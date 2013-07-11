using System;

namespace sharedObjects
{
    public interface ITarget
    {
        /// <summary>
        /// the database id
        /// </summary>
        int ID { get; }

        /// <summary>
        /// the unique 6 digit id
        /// </summary>
        int UID { get; }

        /// <summary>
        /// the last time the target communicated with the server
        /// </summary>
        DateTime LastOnlineTime { get; }
    }
}
