using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using sharedFunctions;

namespace targetClient
{
    class TgClient
    {
        private static ServerController serverController;

        private static Target target;

        private static Timer timer;

        private static Queue<Command> cmdQueue;

        static void Main(string[] args)
        {
            cmdQueue = new Queue<Command>();

            timer = new Timer();

            timer.Interval = 1000;
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

            Application.Run();
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine("tick");
        }

    }
}
