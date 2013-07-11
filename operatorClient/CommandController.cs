using System;
using System.Windows.Forms;
using System.Collections.Generic;
using sharedObjects;
using ioLibrary;

namespace operatorClient
{
    /// <summary>
    /// a commandController which controls commands sent to the server and fetches their responses
    /// </summary>
    class CommandController
    {
        private Queue<Command> cmdQueue;
        private ServerController serverController;

        /// <summary>
        /// gets the underlying serverController
        /// </summary>
        public ServerController ServerController { get { return serverController; } }
        private Timer timer;

        /// <summary>
        /// gets or sets the interval of the internal timer
        /// </summary>
        public int TimerInterval
        {
            get { return timer.Interval; }
            set { timer.Interval = value; }
        }

        /// <summary>
        /// the event fired when a new response to a queued command is received
        /// </summary>
        public event EventHandler<NewResponseEventArgs> newResponseReceived;

        /// <summary>
        /// constructs a new CommandController with the given IOperator interface
        /// </summary>
        /// <param name="op">the IOperator</param>
        public CommandController(IOperator op)
        {
            cmdQueue = new Queue<Command>();
            serverController = new ServerController(op);
            timer = new Timer();
            
            timer.Interval = 5000;
            
            timer.Tick += timer_Elapsed;

            //if (serverController.URI == null || serverController.URI == "")         //load and check server controller uri, if not set prompt for it
            //{
                Console.Write("server uri>");
                serverController.saveUri(Console.ReadLine());
                serverController.URI = serverController.loadUri();
            //}

            while (!register()) { }

            timer.Start();
        }

        private bool register()
        {
            try
            {
                serverController.register();
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("some io error occured, maybe url is wrong...");
                Console.WriteLine("error was: ");
                Console.WriteLine(e.Message);
                Console.Write("server uri>");
                serverController.saveUri(Console.ReadLine());
                serverController.URI = serverController.loadUri();
                return false;
            }
            return true;
        }

        /// <summary>
        /// fires the newResponseReceived event
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void onNewResponseReceived(NewResponseEventArgs e)
        {
            EventHandler<NewResponseEventArgs> handler = newResponseReceived;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// the event listener for the main timer
        /// </summary>
        /// <param name="sender">the object which fired this event</param>
        /// <param name="e">the eventArgs</param>
        private void timer_Elapsed(object sender, EventArgs e)
        {
            if (cmdQueue.Count == 0) return;
            Command tempCmd = cmdQueue.Peek();
            Response tempResp = serverController.getResponseByCmd(tempCmd.ID);
            if (tempResp != null)
            {
                cmdQueue.Dequeue();
                NewResponseEventArgs args = new NewResponseEventArgs();
                args.Response = tempResp;
                args.reveiceTime = DateTime.Now;
                onNewResponseReceived(args);
            }
        }

        /// <summary>
        /// saves a command to the server and to the queue
        /// </summary>
        /// <param name="command">the command to save</param>
        public void saveCommand(Command command)
        {
            cmdQueue.Enqueue(serverController.saveCommand(command));
        }
    }
}
