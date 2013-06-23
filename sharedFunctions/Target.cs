using System;

namespace sharedFunctions
{
    public class Target : ITarget
    {
        private int UID;
        private int ID;
        private DateTime lastOnlineTime;

        public Target(int _UID, DateTime _lastOnlineTime)
        {
            UID = _UID;
            lastOnlineTime = _lastOnlineTime;
        }

        public Target(String inputString)
        {
            createFromString(inputString);
        }
        
        public void createFromString(String inputString)
        {
            String[] inputSplit = inputString.Split(new Char[] { '\f' }, StringSplitOptions.RemoveEmptyEntries);
            ID = Convert.ToInt32(inputSplit[0]);
            UID = Convert.ToInt32(inputSplit[1]);
            lastOnlineTime = DateTime.Parse(inputSplit[2]);
        }

        public int getID()
        {
            return ID;
        }

        public int getUID()
        {
            return UID;
        }

        public DateTime getLastOnlineTime()
        {
            return lastOnlineTime;
        }

        public override String ToString()
        {
            return "\ntarget id: " + ID.ToString() + "\n UID: " + UID.ToString() + "\n lastOnlineTime: " + lastOnlineTime.ToString();
        }
    }
}
