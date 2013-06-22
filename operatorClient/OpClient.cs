using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharedFunctions;
using System.IO;

namespace operatorClient
{
    class OpClient : IOperator
    {
        private static int ID;
        private static int UID;
        private static DateTime lastOnlineTime;
        private static int permissions;

        private static ServerController serverController;

        private static Operator op;

        static void Main(string[] args)
        {
            lastOnlineTime = DateTime.Now;
            UID = sharedFunctions.IdManager.loadUID();
            if (UID == -1)
            {
                UID = sharedFunctions.IdManager.createUID();
                sharedFunctions.IdManager.saveUID(UID);
            }

            Console.WriteLine("remoteAnalyzerMk2 operator client running, id is: " + UID);
            Console.WriteLine("registering operator...");

            op = new Operator(0, UID, DateTime.Now, 0);

            serverController = new ServerController(op);

            if (serverController.loadUri() == "" || serverController.loadUri() == null)
            {
                Console.Write("server uri>");
                serverController.saveUri(Console.ReadLine());
                serverController = new ServerController(op);
            }

            serverController.register();

            String input = "";
            do
            {
                Console.Write("\r\nop>");
                input = Console.ReadLine();
                switch (input)
                {
                    case "test":
                        Console.WriteLine(sharedFunctions.Encoder.stringToHex("\f"));
                        Console.WriteLine(sharedFunctions.Encoder.stringToHex("false"));
                        break;
                    case "getTargets":
                        sharedFunctions.Target[] targets = serverController.getTargets();
                        Console.WriteLine(targets[0].ToString());
                        break;
                    case "cmd":
                        Console.Write("to>");
                        String to = Console.ReadLine();
                        Console.Write("cmd>");
                        String data = Console.ReadLine();
                        Console.WriteLine(serverController.saveCommand(new Command(Convert.ToInt32(to), data)).ToString());
                        break;
                    case "resp":
                        sharedFunctions.Command[] commands = serverController.listCommands();
                        foreach (sharedFunctions.Command command in commands)
                        {
                            Console.WriteLine(command.ToString());
                        }
                        break;
                    case "getCmd":
                        Console.Write("id>");
                        String id = Console.ReadLine();
                        Console.WriteLine(serverController.getCommand(Convert.ToInt32(id)).ToString());
                        break;
                    default:
                        Console.WriteLine("unsupported command");
                        break;
                }
            }
            while (input != "exit");
        }

        public int getPermissions()
        {
            return permissions;
        }

        public int getID()
        {
            return ID;
        }

        public int getUID()
        {
            return UID;
        }

        public DateTime getLastOnlineTime()
        {
            return lastOnlineTime;
        }
    }
}
