using System;
using System.Collections.Generic;
using sharedFunctions;

namespace operatorClient
{
    class OpClient : IOperator
    {
        private static int ID;                      //the database id
        private static int UID;                     //the unique uid
        private static DateTime lastOnlineTime;     //the last online time
        private static int permissions;             //the permissions of this operator, not used

        private static ServerController serverController;   //a serverController

        private static Operator op;

		private static Queue<sharedFunctions.Command> cmdQueue;

        static void Main(string[] args)
        {
            lastOnlineTime = DateTime.Now;

			cmdQueue = new Queue<sharedFunctions.Command>();

            UID = sharedFunctions.IdManager.loadUID();      //read the uid from app settings
            if (UID == -1)                                  //check it for validity, if invalid create a new uid and save
            {
                UID = sharedFunctions.IdManager.createUID();
                sharedFunctions.IdManager.saveUID(UID);
            }

            Console.WriteLine("remoteAnalyzerMk2 operator client running, id is: " + UID);
            Console.WriteLine("registering operator...");

            op = new Operator(0, UID, DateTime.Now, 0);     //create new operator object for the serverController to work with

            serverController = new ServerController(op);

            if (serverController.loadUri() == null || serverController.loadUri () == "")         //load and check server controller uri, if not set prompt for it
            {
                Console.Write("server uri>");
                serverController.saveUri(Console.ReadLine());
                serverController = new ServerController(op);
            }

            serverController.register();                    //register the uid

            String input = "";

            do
            {
                Console.Write("\r\nop>");                   //displays a prompt
                input = Console.ReadLine();                 //and read it
                switch (input)                              //and checks what you entered
                {
                    case "test":
                        Console.WriteLine(sharedFunctions.Encoder.stringToHex("\f"));
                        Console.WriteLine(sharedFunctions.Encoder.stringToHex("none"));
                        break;
                    case "getTargets":
                        sharedFunctions.Target[] targets = serverController.getTargets();
                        foreach (sharedFunctions.Target target in targets)
                        {
                            Console.WriteLine(target.ToString());
                        }
                        break;
                    case "cmd":
                        Console.Write("to>");
                        String to = Console.ReadLine();
                        Console.Write("cmd>");
                        String data = Console.ReadLine();
                        Console.WriteLine(serverController.saveCommand(new Command(Convert.ToInt32(to), data)).ToString()); //creates a new command, saves it on the server and displays it again in the prompt
                        break;
                    case "listCmds":
                        sharedFunctions.Command[] commands = serverController.listCommands();
                        foreach (sharedFunctions.Command command in commands)
                        {
                            Console.WriteLine(command.ToString());
                        }
                        break;
                    case "getCmd":
                        Console.Write("id>");
                        String id = Console.ReadLine();
						sharedFunctions.Command cmd = serverController.getCommand(Convert.ToInt32 (id));
						Console.WriteLine(cmd != null ? cmd.ToString() : "command not foud");
                        break;
                    case "resp":
                        Console.Write("cmd ID>");
                        String cmdID = Console.ReadLine();
                        Console.Write("response>");
                        String response = Console.ReadLine();
                        Console.WriteLine(serverController.saveResponse(new Response(Convert.ToInt32(cmdID), Convert.ToInt32(UID), response)).ToString());
                        break;
					case "getResp":
						Console.Write("resp ID>");
						String respID = Console.ReadLine ();
						sharedFunctions.Response resp = serverController.getResponseById(Convert.ToInt32(respID));
						Console.WriteLine(resp != null ? resp.ToString() : "response not foud");
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
