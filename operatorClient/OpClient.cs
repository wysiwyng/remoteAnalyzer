using System;
using System.Collections.Generic;
using sharedFunctions;

namespace operatorClient
{
    class OpClient : IOperator
    {
        private static int id;                      //the database id
        public int ID { get { return id; } }

        private static int uid;                     //the unique uid
        public int UID { get { return uid; } }

        public DateTime LastOnlineTime { get { return DateTime.Now; } }

        private static CommandController commandController;

        private static Operator op;

        static void Main(string[] args)
        {
            uid = IdManager.loadUID();                      //read the uid from app settings
            if (uid == -1)                                  //check it for validity, if invalid create a new uid and save
            {
                uid = IdManager.createUID();
                IdManager.saveUID(uid);
            }

            Console.WriteLine("remoteAnalyzerMk2 operator client running, id is: " + uid);
            Console.WriteLine("registering operator...");

            op = new Operator(0, uid, DateTime.Now);     //create new operator object for the serverController to work with

            commandController = new CommandController(op);

            commandController.newResponseReceived += commandController_newResponseReceived;

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

        static void commandController_newResponseReceived(object sender, NewResponseEventArgs e)
        {
            Console.WriteLine("received response for queued command");
            Console.WriteLine(e.Response.ToString());
            Console.Write("\r\nop>");
        }
    }
}
