using System;
using System.Net;
using System.Net.Sockets.Kcp;
namespace PENet
{
    public enum SessionState  //会话状态
    {
        None,
        Connected,
        DisConneted,
    }
    /// <summary>
    /// 网络会话的接受与发送
    /// </summary>
    public class KCPSession  //网络会话
    {
        protected uint m_sid;
        Action<byte[], IPEndPoint> m_udpSenderl;
        private IPEndPoint m_remotePoint;
        protected SessionState m_sessionState=SessionState.None;

        public KCPHandle m_handle;
        public Kcp kcp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conv">识别ID</param>
        /// <param name="itemName"></param>
        public void InitSession(uint sid, Action<byte[],IPEndPoint> udpSender,IPEndPoint remotePoint)
        {
            m_sid = sid;
            m_udpSenderl = udpSender;
            m_remotePoint = remotePoint;
            m_sessionState = SessionState.Connected;   //连接了

            m_handle = new KCPHandle();
            kcp = new Kcp(m_sid, m_handle);
            kcp.NoDelay(1, 10, 2, 1);
            kcp.WndSize(64, 64);
            kcp.SetMtu(512);

            m_handle.Out = ((Memory<byte> buffer) =>
            {
                byte[] bytes = buffer.ToArray();
                udpSender(bytes, m_remotePoint);
            });
        }
    }
}
