using System;

namespace sharedFunctions
{
    public interface ITarget
    {
        int ID { get; }
        int UID { get; }
        DateTime LastOnlineTime { get; }
    }
}
