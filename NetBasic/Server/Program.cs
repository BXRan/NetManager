using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        private static int port = 17333;

        static void Main(string[] args)
        {
            //CreatTCPServer();
            CreatUDPServer();

            //Console.ReadKey();
        }

        private static void CreatUDPServer()
        {
            UdpClient listener = new UdpClient(port);
            IPEndPoint remoteIP = new IPEndPoint(IPAddress.Any, port);

            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for message ...");
                    byte[] bytes = listener.Receive(ref remoteIP);
                    Console.WriteLine($"Received msg from {remoteIP}");
                    Console.WriteLine($" {Encoding.ASCII.GetString(bytes,0,bytes.Length)}");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }

        }

        private static void CreatTCPServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port); //服务器监听
            listener.Start();//开始监听

            Console.WriteLine("Waiting for connection ...");  //等待客户端连接

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();  //接受客户端
                Console.WriteLine("Connection accepted.");
                NetworkStream ns = client.GetStream();//获取 流
                byte[] data = Encoding.ASCII.GetBytes("www.毕萧然.com");//把要发送的消息转换成字节
                try
                {
                    ns.Write(data, 0, data.Length);//发送
                    ns.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());  //输出错误信息
                }

            }
        
        }
    }
}
