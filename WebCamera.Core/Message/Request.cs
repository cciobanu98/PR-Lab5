using System;

namespace WebCamera.Core
{
    [Serializable]
    public class Request : Message
    {
        public RequestType RequestType { get; set; }
    }
}
