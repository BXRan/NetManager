using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        private static int port = 17333;
        static void Main(string[] args)
        {
            //CreatTCPClient();
            CreatUDPClient();

            Console.ReadKey();
        }

        private static void CreatUDPClient()
        {
            UdpClient client = new UdpClient();
            Byte[] data = Encoding.ASCII.GetBytes("www.毕萧然.com");
            IPEndPoint remoteIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            client.Send(data, data.Length,remoteIP);
            Console.WriteLine("Message send to remote address"); ;
        }

        private static void CreatTCPClient()
        {
            try
            {
                var client = new TcpClient("127.0.0.1", port);  //连接服务器
                NetworkStream ns = client.GetStream();
                byte[] data = new byte[1024];
                int len = ns.Read(data, 0, data.Length);  //接受消息.
                Console.WriteLine(Encoding.ASCII.GetString(data,0,len));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
