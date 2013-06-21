using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharedFunctions
{
    public class Operator:IOperator
    {
        private int ID;
        private int UID;
        private DateTime lastOnlineTime;
        private int permissions;

        public Operator(int _id, int _uid, DateTime _lastOnlineTime, int _permissions)
        {
            ID = _id;
            UID = _uid;
            lastOnlineTime = _lastOnlineTime;
            permissions = _permissions;
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

        public int getPermissions()
        {
            return permissions;
        }
    }
}
