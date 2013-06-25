using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sharedFunctions;

namespace operatorClient
{
    class CommandController
    {
        private Queue<Command> cmdQueue;
        private ServerController serverController;

        public CommandController(IOperator op)
        {
            cmdQueue = new Queue<Command>();
            serverController = new ServerController(op);

            if (serverController.getUri() == null || serverController.getUri() == "")         //load and check server controller uri, if not set prompt for it
            {
                Console.Write("server uri>");
                serverController.saveUri(Console.ReadLine());
                serverController.setUri(serverController.loadUri());
            }

            serverController.register();
        }

        public void saveCommand(Command command)
        {
            cmdQueue.Enqueue(serverController.saveCommand(command));
        }

    }
}
