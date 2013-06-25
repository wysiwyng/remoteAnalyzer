using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using sharedFunctions;

namespace operatorClient
{
    /// <summary>
    /// a commandController which controls commands sent to the server and fetches their responses
    /// </summary>
    class CommandController
    {
        private Queue<Command> cmdQueue;
        private ServerController serverController;
        private Timer timer;

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
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += timer_Elapsed;

            if (serverController.getUri() == null || serverController.getUri() == "")         //load and check server controller uri, if not set prompt for it
            {
                Console.Write("server uri>");
                serverController.saveUri(Console.ReadLine());
                serverController.setUri(serverController.loadUri());
            }

            serverController.register();

            timer.Start();
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
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (cmdQueue.Count == 0) return;
            Command tempCmd = cmdQueue.Peek();
            Response tempResp = serverController.getResponseByCmd(tempCmd.getID());
            if (tempResp != null)
            {
                cmdQueue.Dequeue();
                NewResponseEventArgs args = new NewResponseEventArgs();
                args.newResponse = tempResp;
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
