using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace JWar2Net.Server
{
    public class TCPServer
    {
        private Socket _serverSocket;
        public Socket ServerSocket
        {
            get
            {
                return _serverSocket;
            }
        }

        private ListenThread _listenThread;
        private ReceiveThread _receiveThread;

        private IJClientManager _clientManager;
        private IJNetRequestHandler _requestHandler;

        public TCPServer(IJClientManager clientManager, IJNetRequestHandler requestHandler)
        {
            _clientManager = clientManager;
            _requestHandler = requestHandler;
        }

        public void Start(string ip, int port)
        {
            Log.Debug("tcp-server", "server start with IP:{0} Port:{1}",
                ip,
                port);
            //定义IP地址
            IPAddress local = IPAddress.Parse(ip);
            IPEndPoint iep = new IPEndPoint(local, port);
            //创建服务器的socket对象
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(iep);


            this._receiveThread = new ReceiveThread(this, this._clientManager, this._requestHandler);
            this._receiveThread.Start();

            this._listenThread = new ListenThread(this, this._clientManager);
            this._listenThread.Start();
        }

        public void Stop()
        {
            this._listenThread.Stop();
            this._receiveThread.Stop();

            Log.Debug("tcp-server", "server stop");
        }
    }
}
