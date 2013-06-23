using System;

namespace sharedFunctions
{
    public class IdManager
    {
        public static int createUID()
        {
            return new Random().Next(100000, 999999);
        }

        public static int loadUID()
        {
            return Properties.Settings.Default.uid;
        }

        public static void saveUID(int uid)
        {
            Properties.Settings.Default.uid = uid;
            Properties.Settings.Default.Save();
        }
    }
}
