using System;

namespace sharedFunctions
{
    /// <summary>
    /// a class to manage user ids
    /// </summary>
    public static class IdManager
    {
        /// <summary>
        /// creates a new random user id
        /// </summary>
        /// <returns>the created user id</returns>
        public static int createUID()
        {
            return new Random().Next(100000, 999999);
        }

        /// <summary>
        /// loads the saved user id
        /// </summary>
        /// <returns>the user id</returns>
        public static int loadUID()
        {
            return Properties.Settings.Default.uid;
        }

        /// <summary>
        /// saves a given user id to memory
        /// </summary>
        /// <param name="uid">the user id</param>
        public static void saveUID(int uid)
        {
            Properties.Settings.Default.uid = uid;
            Properties.Settings.Default.Save();
        }
    }
}
