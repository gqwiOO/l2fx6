using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerForAndroid
{
    internal class ComputerServer
    {
        private static string IP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
        public UdpClient udpClient = new UdpClient("192.168.1.4", 6666);

        public async void Start()
        {
            Console.WriteLine("Server started");
            IPEndPoint phoneIP = new IPEndPoint(IPAddress.Any, 6666);

            while (true) 
            {
            
                var response = await udpClient.ReceiveAsync();
                if (response != null)
                {
                   var str = Encoding.UTF8.GetString(response.Buffer);
                   Console.WriteLine("received : " + str);
            
                }
                
            }


        }
    }
}
