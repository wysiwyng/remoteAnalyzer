﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using ioLibrary;
using sharedObjects;

namespace targetClient
{
    class TgClient
    {
        private static ServerController serverController;

        private static Target target;

        private static System.Windows.Forms.Timer timer;

        private static volatile Queue<Command> cmdQueue;

        private static AutoResetEvent newData;
        
        private static volatile bool completed = true;

        static void Main(string[] args)
        {
            newData = new AutoResetEvent(false);

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

            //serverController.URI = "<insert url here>";

            //serverController.saveUri(serverController.URI);

            serverController.register();

            timer.Start();

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
                completed = true;
            }
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            if (!completed) return;
            cmdQueue = new Queue<Command>(serverController.listCommands());
            if (cmdQueue.Count > 0)
            {
                Debug.WriteLine("enqueued " + cmdQueue.Count.ToString() + " new commands");
                completed = false;
                newData.Set();
            }
            Debug.WriteLine("tick");
        }
    }
}
