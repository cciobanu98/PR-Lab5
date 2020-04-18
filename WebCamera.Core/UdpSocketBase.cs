using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace WebCamera.Core
{
    public abstract class UdpSocketBase
    {
        protected EndPoint remote;
        protected readonly IFormatter formatter;
        protected Socket socket;
        protected readonly ILogger<UdpSocketBase> Logger;

        public UdpSocketBase(ILogger<UdpSocketBase> logger, IFormatter formatter)
        {
            this.formatter = formatter;
            Logger = logger;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public abstract void Initialize(string ip, int port);
        public T Get<T>() where T: Message
        {
            try
            {
                var data = new byte[100000];
                var recv = socket.ReceiveFrom(data, ref remote);
                var ms = new MemoryStream(data);
                return (T)formatter.Deserialize(ms);
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to receive message {e.Message}");
                return null;
            }
        }

        public void Send<T>(T message) where T: Message
        {
            byte[] bytes = null;
            try
            {
                var ms = new MemoryStream();
                formatter.Serialize(ms, message);
                bytes = ms.ToArray();
                socket.SendTo(bytes, remote);
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to send {bytes.Length } bytes {e.Message}");
            }
        }
    }
}
