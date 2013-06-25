using System;

namespace sharedFunctions
{
    public class Operator:IOperator
    {
        private int id;
        public int ID { get { return id; } }
        private int uid;
        public int UID { get { return uid; } }
        private DateTime lastOnlineTime;
        public DateTime LastOnlineTime { get { return lastOnlineTime; } }

        public Operator(int _id, int _uid, DateTime _lastOnlineTime)
        {
            id = _id;
            uid = _uid;
            lastOnlineTime = _lastOnlineTime;
        }

        public int getID()
        {
            return id;
        }

        public int getUID()
        {
            return uid;
        }

        public DateTime getLastOnlineTime()
        {
            return lastOnlineTime;
        }
    }
}
