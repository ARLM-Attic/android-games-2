using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace JWar2Net.Server
{
    public class ListenThread
    {
        private Thread _thread;
        private bool _isRunning = false;
        private EventWaitHandle _eventHandle;

        TCPServer _server;
        private IJClientManager _clientManager;

        public ListenThread(TCPServer server, IJClientManager clientManager)
        {
            this._clientManager = clientManager;
            this._server = server;
        }

        public void Start()
        {
            this._server.ServerSocket.Listen(20);
            Console.WriteLine(string.Format("server socket start listen {0}", 20));

            this._eventHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            this._thread = new Thread(Running);
            this._isRunning = true;
            Console.WriteLine("listen start");
            this._thread.Start();
        }

        public void Stop()
        {
            this._isRunning = false;
            if (!this._eventHandle.WaitOne(100))
            {
                MockConnect();
                this._eventHandle.WaitOne(100);
            }
            Console.WriteLine("listen stop");
        }

        private void Running()
        {
            Console.WriteLine("listen thread start");
            while (_isRunning)
            {
                Socket clientSocket = this._server.ServerSocket.Accept();
                if (_isRunning)
                {
                    AGNetClient client = new AGNetClient(clientSocket);
                    JNetClientChannel player = new JNetClientChannel();
                    player.Client = client;
                    _clientManager.AddClient(player);
                    Console.WriteLine("client {0} connect", client.GetNetAddress());
                }
            }

            this._eventHandle.Set();
            Console.WriteLine("listen thread stop");
        }

        private void MockConnect()
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse(ServerConfig.Instance.IP);
            IPEndPoint endPoint = new IPEndPoint(address, ServerConfig.Instance.Port);
            try
            {
                clientSocket.Connect(endPoint);//.BeginConnect(iep, new AsyncCallback(Connect),socket);

                Console.WriteLine("mock client connect");
            }
            catch
            {
                Console.WriteLine("mock client connect failure");
            }
        }
    }
}
