using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace JWar2Net.Client
{
    public class ReceiveThread
    {
        private Thread _thread;
        private bool _isRunning = false;
        private bool _isReceiving = false;

        private EventWaitHandle _eventHandle;

        byte[] _buffer;
        int _receiveLength;
        JNetClient _client;


        private IJNetResponseHandler _response;

        public ReceiveThread(JNetClient client, IJNetResponseHandler responseHandler)
        {
            _buffer = new byte[4096];
            this._client = client;
            //_requestResolver = new RequestResolver();

            //_actionDispacher = new RPGHandle();
            _response = responseHandler;
        }

        public void Start()
        {
            //Console.WriteLine("server start receive");

            this._eventHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            this._thread = new Thread(Running);
            this._isRunning = true;
            //Console.WriteLine("receive start");
            this._thread.Start();
        }

        public void Stop()
        {
            this._isRunning = false;
            this._eventHandle.WaitOne();
            //Console.WriteLine("server stop receive");
        }

        private void Running()
        {
            //Console.WriteLine("receive thread start");
            while (_isRunning)
            {
                if(!_isReceiving)
                {
                    _isReceiving = true;
                    _client.Socket.BeginReceive(_buffer, 0, 4096, SocketFlags.None, new AsyncCallback(OnReceiveCallback), null);
                    
                }
            }

            this._eventHandle.Set();
            //Console.WriteLine("receive thread stop");
        }

        private void OnReceiveCallback(IAsyncResult ar)
        {
            _receiveLength = _client.Socket.EndReceive(ar);
            if (_receiveLength > 0)
            {
                //Log.Debug("recv", "收到玩家({0})编号({1})数据长度({2})", player.Client.GetNetAddress(), player.Id, player.ReceiveLength);
                //_actionDispacher.Process(player, player.Buffer, player.ReceiveLength);
                _response.OnHandle(_buffer, _receiveLength);
            }
            else
            {
            }
            _isReceiving = false;
        }
    }
}
