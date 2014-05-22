using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Net.Server
{
    public interface IJNetRequestHandler
    {
        void OnHandle(JNetClientChannel player, byte[] buffer, int length);
    }
}
