using System;
using System.Collections.Generic;
using System.Text;
using WebCamera.Core;

namespace WebCamera.Client.Core.Client
{
    public interface IUdpClient
    {
        void Initialize(string ip, int port);

        void Send(Request request);

        Response Get();
    }
}
