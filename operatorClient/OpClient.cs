using System;
using System.Collections.Generic;
using sharedFunctions;

namespace operatorClient
{
    class OpClient
    {
        private static CommandController commandController;

        private static Operator op;

        static void Main(string[] args)
        {
            int uid = IdManager.loadUID();                      //read the uid from app settings
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

            Console.WriteLine("registered and running");

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
                    case "getTargets":
                        Target[] targets = commandController.ServerController.getTargets();
                        foreach (Target target in targets)
                        {
                            Console.WriteLine(target.ToString());
                        }
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
