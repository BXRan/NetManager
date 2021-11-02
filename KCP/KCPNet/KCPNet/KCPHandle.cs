using System;
using System.Buffers;
using System.Net.Sockets.Kcp;

namespace PENet
{
    /// <summary>
    /// KCP数据处理器
    /// </summary>
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
