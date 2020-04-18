using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.Serialization;
using WebCamera.Core;

namespace WebCamera.Server
{
    public class Server : UdpSocketBase
    {
        public Server(ILogger<Server> logger, IFormatter formatter):base(logger, formatter)
        {

        }
        public override void Initialize(string ip, int port)
        {
            
            var ipAdress = new IPEndPoint(IPAddress.Any, port);

            remote = new IPEndPoint(IPAddress.Any, 0);
            socket.Bind(ipAdress);
            Logger.LogInformation("Server Start with succes");
        }
    }
}
