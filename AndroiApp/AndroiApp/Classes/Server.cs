using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace AndroiApp.Classes
{
    internal class Server
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient udpClient = new UdpClient();

        IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse("192.168.0.101"), 6666);


        public async void SendFunction(string function)
        {
            byte[] fun_bytes = Encoding.UTF8.GetBytes(function);


            await udpClient.SendAsync(fun_bytes, fun_bytes.Length, remotePoint);
        }

    }
}
