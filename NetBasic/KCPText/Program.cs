using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KCPTest
{
    class KCPTextStart
    {
        static void Main(string[] args)
        {
            Text1();
            Console.ReadKey();
        }

        public static void Text1()
        {
            const uint conv = 123;

            KCPItem kcpServer = new KCPItem(123, "Server");
            KCPItem kcpClient = new KCPItem(123, "Client");

            Random rd = new Random();

            byte[] data = Encoding.ASCII.GetBytes("WWW.CASIO.COM");
            kcpClient.SendMsg(data);

            kcpServer.SetOutCallback((Memory<byte> buffer) =>
            {
                kcpClient.InputData(buffer.Span);
            });
            kcpClient.SetOutCallback((Memory<byte> buffer) =>   //服务器发送成功回调
            {
                int net = rd.Next(100);//模拟丢包
                if (net >= 95)
                {
                    Console.WriteLine($"Send Pkg Succ:{GetByteString(buffer.ToArray())}");
                    kcpServer.InputData(buffer.Span);//模拟接收到客户端消息
                }
                else
                {
                    Console.WriteLine("Send Pkg Miss");
                }

            });

            while (true)
            {
                kcpServer.Update();
                kcpClient.Update();

                Thread.Sleep(20);
            }
        }  //KCP

        static string GetByteString(byte[] bytes)
        {
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("\n   [{0}:{1}]", i, bytes[i]);
            }
            return str;
        }

       
    }

   
}
