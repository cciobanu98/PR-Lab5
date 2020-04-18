using System;

namespace WebCamera.Core
{
    [Serializable]
    public abstract class Message
    {
        public byte[] Data { get; set; }
    }
}
