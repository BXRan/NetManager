using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net.Sockets.Kcp;
using System.Text;

namespace KCPTest
{
   public class KCPHandle : IKcpCallback
    {
        public Action<Memory<byte>> Out;
        public void Output(IMemoryOwner<byte> buffer, int avalidLength)
        {
            using (buffer)
            {
                Out(buffer.Memory.Slice(0, avalidLength));
            }
        }
    }
}
