using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace JWar2Net.Server
{
    public class AGNetClient
    {
        /// <summary>
        /// 通过此socket和接收客户端的数据，发送数据到客户端
        /// </summary>
        public Socket Socket { get; private set; }

        public AGNetClient(Socket socket)
        {
            Socket = socket;
        }

        /// <summary>
        /// 发送数据到客户端
        /// </summary>
        /// <param name="buffer"></param>
        public void SendData(byte[] buffer)
        {
            Socket.Send(buffer);
        }

        /// <summary>
        /// 发送数据到客户端
        /// </summary>
        /// <param name="buffer"></param>
        public void SendData(byte[] buffer, int length)
        {
            Socket.Send(buffer, length, SocketFlags.None);
        }


        public string GetNetAddress()
        {
            return string.Format("{0}:{1}", ((IPEndPoint)Socket.RemoteEndPoint).Address, ((IPEndPoint)Socket.RemoteEndPoint).Port);
        }
    }
}
