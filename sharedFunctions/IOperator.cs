using System;

namespace sharedFunctions
{
    public interface IOperator
    {
        int ID { get; }
        int UID { get; }
        DateTime LastOnlineTime { get; }
    }
}
