using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Net.Server
{
    public interface IJClientManager
    {
        List<JNetClientChannel> PlayerList { get; }

        void AddClient(JNetClientChannel player);
    }
}
