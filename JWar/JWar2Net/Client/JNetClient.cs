using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace JWar2Net.Client
{
    public class JNetClient
    {

        public Socket Socket;
        private ReceiveThread _recvThread;

        private IJNetResponseHandler _responseHandler;

        private static JNetClient s_instance;
        public static JNetClient Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new JNetClient();
                }
                return s_instance;
            }
        }

        public void SetResponseHandler(IJNetResponseHandler responseHandler)
        {
            _responseHandler = responseHandler;
        }

        /// <summary>
        /// 1成功，2失败
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public int Connect(string ip, int port)
        {
            _recvThread = new ReceiveThread(this, _responseHandler);

            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(address, port);
            try
            {
                Socket.Connect(endPoint);
                _recvThread.Start();
                return 1;
            }
            catch
            {
                return 2;
            }
        }

        public int DisConnect()
        {
            _recvThread.Stop();
            Socket.Disconnect(false);
            return 0;
        }

        public int Send(byte[] buffer, int length)
        {
            //_clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(OnSendCallback), null);
            try
            {
                //_clientSocket.Send(buffer,);
                Socket.BeginSend(buffer, 0, length, SocketFlags.None, new AsyncCallback(OnSendCallback), null);
                return 1;
            }
            catch
            {
                return 2;
            }
        }


        void OnSendCallback(IAsyncResult ar)
        {
            Socket.EndSend(ar);
        }
    }
}
