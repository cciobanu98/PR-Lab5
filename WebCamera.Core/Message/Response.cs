using System;

namespace WebCamera.Core
{
    [Serializable]
    public class Response : Message
    {
        public ResponseType ResponseType { get; set; }
    }
}
