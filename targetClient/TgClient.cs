using System;
using System.Diagnostics;
using System.Timers;
using sharedFunctions;

namespace targetClient
{
    class TgClient
    {
        private static ServerController serverController;

        private static Target target;

        private static Timer timer;

        static void Main(string[] args)
        {
            int uid = IdManager.loadUID();
            if (uid == -1)
            {
                uid = IdManager.createUID();
                IdManager.saveUID(uid);
            }

            target = new Target(uid, DateTime.Now);

            serverController = new ServerController(target);

            serverController.register();

            Debug.WriteLine("blub");
        }
    }
}
