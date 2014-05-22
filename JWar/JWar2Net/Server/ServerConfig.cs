using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JWar2Net.Server
{
    public class ServerConfig
    {
        public string IP { get; set; }
        public int Port { get; set; }

        public Version Ver { get; set; }

        private static ServerConfig s_instance;
        public static ServerConfig Instance
        {
            get
            {
                return s_instance;
            }
        }
        public static void Init(string xmlFile)
        {
            s_instance = new ServerConfig();
            XDocument xDoc = XDocument.Load(xmlFile);
            XElement xIP = xDoc.Root.Element("ip");
            s_instance.IP = xIP.Value;

            XElement xPort = xDoc.Root.Element("port");
            s_instance.Port = Convert.ToInt32(xPort.Value);


            s_instance.Ver = new Version(Convert.ToInt32(xDoc.Root.Element("ver-major").Value),
                Convert.ToInt32(xDoc.Root.Element("ver-minor").Value),
                Convert.ToInt32(xDoc.Root.Element("ver-build").Value));
        }

        private ServerConfig()
        {
        }
    }
}
