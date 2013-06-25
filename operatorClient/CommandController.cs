using System;
using System.Timers;
using System.Collections.Generic;
using sharedFunctions;

namespace operatorClient
{
    class CommandController
    {
        private Queue<Command> cmdQueue;
        private ServerController serverController;
        private Timer timer;

        public event EventHandler<NewResponseEventArgs> newResponseReceived;

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

        protected virtual void onNewResponseReceived(NewResponseEventArgs e)
        {
            EventHandler<NewResponseEventArgs> handler = newResponseReceived;
            if (handler != null) handler(this, e);
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (cmdQueue.Count == 0) return;
            Command tempCmd = cmdQueue.Peek();
            Response tempResp = serverController.getResponseByCmd(tempCmd.getID());
            if (tempResp != null)
            {
                cmdQueue.Dequeue();
                Console.WriteLine("response received");
                NewResponseEventArgs args = new NewResponseEventArgs();
                args.newResponse = tempResp;
                args.reveiceTime = DateTime.Now;
            }
        }

        public void saveCommand(Command command)
        {
            cmdQueue.Enqueue(serverController.saveCommand(command));
        }

    }
}
