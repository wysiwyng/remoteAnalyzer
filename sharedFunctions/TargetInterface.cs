using System;

namespace sharedFunctions
{
    public interface ITarget
    {
        int getID();
        int getUID();
        DateTime getLastOnlineTime();
    }
}
