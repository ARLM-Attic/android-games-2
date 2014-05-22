using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Net.Client
{
    public interface IJNetResponseHandler
    {
        void OnHandle(byte[] data, int length);
    }
}
