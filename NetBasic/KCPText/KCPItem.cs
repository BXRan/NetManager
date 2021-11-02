using System;
using System.Collections.Generic;
using System.Net.Sockets.Kcp;
using System.Text;

namespace KCPTest
{
    public class KCPItem
    {
        public string itemName;
        public KCPHandle handle;
        public Kcp kcp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conv">识别ID</param>
        /// <param name="itemName"></param>
        public KCPItem(uint conv,string itemName)
        {
            handle = new KCPHandle();
            kcp = new Kcp(conv,handle);
            kcp.NoDelay(1, 10, 2, 1);
            kcp.WndSize(64, 64);
            kcp.SetMtu(512);

            this.itemName = itemName;
        }

        public void InputData(Span<byte> data)
        {
            kcp.Input(data);
        }

        public void SetOutCallback(Action<Memory<byte>> itemSender)
        {
            handle.Out = itemSender;
        }

        public void SendMsg(byte[] data)
        {
            Console.WriteLine($"{itemName} 输入的数据{GetByteString(data)}");
            kcp.Send(data.AsSpan());
        }

        public void Update()
        {
            kcp.Update(DateTime.UtcNow);
            int len;
            while ((len=kcp.PeekSize())>0)
            {
                var buffer = new byte[len];
                if (kcp.Recv(buffer)>=0)
                {
                    Console.WriteLine($"{itemName}  收到数据：{GetByteString(buffer)}");
                }
            }
        }
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
