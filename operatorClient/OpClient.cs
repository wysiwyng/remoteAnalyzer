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

            commandController = new CommandController(op);

            String input = "";

            do
            {
                Console.Write("\r\nop>");                   //displays a prompt
                input = Console.ReadLine();                 //and read it
                switch (input)                              //and checks what you entered
                {
                    case "cmd":
                        Console.Write("to>");
                        int toUID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("cmd>");
                        String cmdData = Console.ReadLine();
                        commandController.saveCommand(new Command(toUID, cmdData));
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
