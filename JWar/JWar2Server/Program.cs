using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using JWar2Net.Server;
using JWar2Net;

namespace JWar2Server
{
    class Program
    {
        static void Main(string[] args)
        {
            CacheData.Init();

            ServerConfig.Init(AppDomain.CurrentDomain.BaseDirectory + "configs\\server.xml");

            Log.Print("RPGNET Server v{0}.{1}.{2}", ServerConfig.Instance.Ver.Major, ServerConfig.Instance.Ver.Minor, ServerConfig.Instance.Ver.Build);

            TCPServer server = new TCPServer(CacheData.GetInstance(), new JWar2RequestHandler());
            server.Start(ServerConfig.Instance.IP, ServerConfig.Instance.Port);
         
            string cmd = Console.ReadLine();
            while (cmd != "exit")
            {
                cmd = Console.ReadLine();
            }

            server.Stop();

            Log.Print("press enter exit...");
            Console.Read();
        }
    }
}
