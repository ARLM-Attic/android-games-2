using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Net.Server
{
    public class JNetClientChannel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// 单位编号
        /// </summary>
        public uint RoleId { get; set; }
        public ushort RoleModelId { get; set; }

        /// <summary>
        /// 当前的地图编号
        /// </summary>
        public ushort CurMapId;
        /// <summary>
        /// 当前的位置行
        /// </summary>
        public ushort CurPosRow;
        /// <summary>
        /// 当前的位置列
        /// </summary>
        public ushort CurPosCol;

        public AGNetClient Client { get; set; }
        
        public byte[] Buffer;
        public int ReceiveLength;
        private int MAXBUFFERSIZE = 4096;
        public bool IsReceiving;

        public int ErrorCount { get; set; }

        public JNetClientChannel()
        {
            IsReceiving = false;
            Buffer = new byte[MAXBUFFERSIZE];
        }
    }
}
