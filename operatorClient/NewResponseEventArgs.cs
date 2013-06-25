using System;
using sharedFunctions;

namespace operatorClient
{
    class NewResponseEventArgs : EventArgs
    {
        /// <summary>
        /// the received response
        /// </summary>
        public Response Response { get; set; }

        /// <summary>
        /// a datetime object containing the date when the response was received
        /// </summary>
        public DateTime reveiceTime { get; set; }
    }
}
