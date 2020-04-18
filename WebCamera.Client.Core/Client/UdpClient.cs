using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using WebCamera.Core;

namespace WebCamera.Client.Core.Client
{
    public class UdpClient : UdpSocketBase, IUdpClient
    {
        public UdpClient(ILogger<UdpClient> logger, IFormatter formatter) : base(logger, formatter)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //remote = new IPEndPoint(IPAddress.Any, 0);
        }
        public override void Initialize(string ip, int port)
        {
            IPAddress broadcast = IPAddress.Parse(ip);
            remote = new IPEndPoint(broadcast, port);
        }
        public Response Get()
        {
            return Get<Response>();
        }

        public void Send(Request request)
        {
            base.Send(request);
        }
    }
}
