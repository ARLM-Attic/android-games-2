using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace JWar2Net.Server
{
    public class ReceiveThread
    {
        private Thread _thread;
        private bool _isRunning = false;
        private EventWaitHandle _eventHandle;

        TCPServer _server;

        //private RequestResolver _requestResolver;

        private IJNetRequestHandler _actionDispacher;
        private IJClientManager _clientManager;

        public ReceiveThread(TCPServer server,IJClientManager clientManager, IJNetRequestHandler requestHandler)
        {
            this._server = server;
            //_requestResolver = new RequestResolver();

            _actionDispacher = requestHandler;
            _clientManager = clientManager;
        }

        public void Start()
        {
            Console.WriteLine("server start receive");

            this._eventHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            this._thread = new Thread(Running);
            this._isRunning = true;
            Console.WriteLine("receive start");
            this._thread.Start();
        }

        public void Stop()
        {
            this._isRunning = false;
            this._eventHandle.WaitOne();
            Console.WriteLine("server stop receive");
        }

        private void Running()
        {
            Console.WriteLine("receive thread start");
            while (_isRunning)
            {
                for (int index = 0; index < _clientManager.PlayerList.Count; )
                {
                    JNetClientChannel player = _clientManager.PlayerList[index];

                    if (player != null)
                    {
                        if (player.ErrorCount > 0)
                        {
                            // 三次失败之后将玩家设置为离线
                            // 删除此玩家
                            //CacheData.GetInstance().RemvoeClient(player, index);
                            Log.Debug("receive-thread", "删除玩家({0})编号({1}),原因网络出错!", player.Client.GetNetAddress(), player.Id);

                            // 通知该地图上的玩家，此人离线
                            //RPGServer.Contracts.Messages.MessageROFM.Send(player);
                        }
                        else
                        {
                            if (!player.IsReceiving)
                            {
                                try
                                {
                                    player.IsReceiving = true;
                                    player.Client.Socket.BeginReceive(player.Buffer, 0, 2048, SocketFlags.None, new AsyncCallback(OnReceiveCallback), player);//.Receive(_buffer, SocketFlags.None);
                                    player.ErrorCount = 0;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("玩家({0})编号({1})接收数据出错{2}!({3})", player.Client.GetNetAddress(), player.Id, player.ErrorCount, ex);
                                    player.ErrorCount++;
                                }

                            }
                            index++;
                        }
                    }
                }
            }

            this._eventHandle.Set();
            Console.WriteLine("receive thread stop");
        }

        private void OnReceiveCallback(IAsyncResult ar)
        {
            JNetClientChannel player = ar.AsyncState as JNetClientChannel;
            player.ReceiveLength = player.Client.Socket.EndReceive(ar);

            player.ErrorCount = 0;
            if (player.ReceiveLength > 0)
            {
                Log.Debug("recv", "收到玩家({0})编号({1})数据长度({2})", player.Client.GetNetAddress(), player.Id, player.ReceiveLength);
                _actionDispacher.OnHandle(player, player.Buffer, player.ReceiveLength);
            }
            else
            {
                Log.Error("recv", "收到玩家({0})编号({1})数据长度({2})，视为掉线", player.Client.GetNetAddress(), player.Id, player.ReceiveLength);
                // 收到长度为0的数据视为掉线
                player.ErrorCount =1;
            }
            player.IsReceiving = false;
        }
    }
}
