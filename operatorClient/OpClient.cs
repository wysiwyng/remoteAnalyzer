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

        private static CommandController commandController;

        private static Operator op;

        static void Main(string[] args)
        {
            lastOnlineTime = DateTime.Now;

            UID = IdManager.loadUID();      //read the uid from app settings
            if (UID == -1)                                  //check it for validity, if invalid create a new uid and save
            {
                UID = IdManager.createUID();
                IdManager.saveUID(UID);
            }

            Console.WriteLine("remoteAnalyzerMk2 operator client running, id is: " + UID);
            Console.WriteLine("registering operator...");

            op = new Operator(0, UID, DateTime.Now, 0);     //create new operator object for the serverController to work with

            serverController = new ServerController(op);

            if (serverController.getUri() == null || serverController.getUri() == "")         //load and check server controller uri, if not set prompt for it
            {
                Console.Write("server uri>");
                serverController.saveUri(Console.ReadLine());
                serverController.setUri(serverController.loadUri());
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
                        Console.WriteLine(Encoder.stringToHex("\f"));
                        Console.WriteLine(Encoder.stringToHex("none"));
                        break;
                    case "getTargets":
                        Target[] targets = serverController.getTargets();
                        foreach (Target target in targets)
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
                        Command[] commands = serverController.listCommands();
                        foreach (Command command in commands)
                        {
                            Console.WriteLine(command.ToString());
                        }
                        break;
                    case "getCmd":
                        Console.Write("id>");
                        String id = Console.ReadLine();
						Command cmd = serverController.getCommand(Convert.ToInt32 (id));
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
						Response resp = serverController.getResponseById(Convert.ToInt32(respID));
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
