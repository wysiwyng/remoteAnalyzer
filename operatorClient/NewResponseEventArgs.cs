using System;
using sharedFunctions;

namespace operatorClient
{
    class NewResponseEventArgs : EventArgs
    {
        public Response newResponse { get; set; }
        public DateTime reveiceTime { get; set; }
    }
}
