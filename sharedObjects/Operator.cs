using System;

namespace sharedObjects
{
    public class Operator:IOperator
    {
        private int id;
        public int ID { get { return id; } }
       
        private int uid;      
        public int UID { get { return uid; } }
      
        private DateTime lastOnlineTime;
        public DateTime LastOnlineTime { get { return lastOnlineTime; } }

        /// <summary>
        /// constructs a new operator with the given values
        /// </summary>
        /// <param name="_id">the database id</param>
        /// <param name="_uid">the 6 digit unique id</param>
        /// <param name="_lastOnlineTime">the time this operator last communicated with the server</param>
        public Operator(int _id, int _uid, DateTime _lastOnlineTime)
        {
            id = _id;
            uid = _uid;
            lastOnlineTime = _lastOnlineTime;
        }
    }
}
