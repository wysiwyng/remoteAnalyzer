using System;

namespace sharedFunctions
{
    public class Target : ITarget
    {
        private int uid;
        public int UID { get { return uid; } }

        private int id;
        public int ID { get { return id; } }

        private DateTime lastOnlineTime;
        public DateTime LastOnlineTime { get { return lastOnlineTime; } }

        public Target(int _UID, DateTime _lastOnlineTime)
        {
            uid = _UID;
            lastOnlineTime = _lastOnlineTime;
        }

        public Target(String inputString)
        {
            createFromString(inputString);
        }
        
        public void createFromString(String inputString)
        {
            String[] inputSplit = inputString.Split(new Char[] { '\f' }, StringSplitOptions.RemoveEmptyEntries);
            id = Convert.ToInt32(inputSplit[0]);
            uid = Convert.ToInt32(inputSplit[1]);
            lastOnlineTime = DateTime.FromBinary(Convert.ToInt64(inputSplit[2]));
        }

        public override String ToString()
        {
            return "\ntarget id: " + id.ToString() + "\n UID: " + uid.ToString() + "\n lastOnlineTime: " + lastOnlineTime.ToString();
        }
    }
}
