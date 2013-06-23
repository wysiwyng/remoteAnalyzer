using System;

namespace sharedFunctions
{
    public interface IOperator
    {
        int getID();
        int getUID();
        DateTime getLastOnlineTime();
        int getPermissions();
    }
}
