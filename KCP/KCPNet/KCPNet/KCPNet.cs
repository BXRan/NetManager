using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PENet
{
    /// <summary>
    /// 基于KCP封装，实现可靠UDP
    /// </summary>
    public class KCPNet
    {
        UdpClient udp;
        IPEndPoint remotePoint;
#region
        public void StartASClient(string ip,int port)
        {
            udp = new UdpClient(0);
            remotePoint = new IPEndPoint(IPAddress.Parse(ip),port);

            //明天编写输出工具类
        }
#endregion

    }


   
}
