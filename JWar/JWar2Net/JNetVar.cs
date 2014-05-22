using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Net
{
    public static class JNetVar
    {
        private static Dictionary<byte, byte> s_varDict = new Dictionary<byte, byte>();

        public static byte Get(byte key)
        {
            if (s_varDict.ContainsKey(key))
            {
                return s_varDict[key];
            }
            return 0x00;
        }

        public static void Set(byte key, byte value)
        {
            if (s_varDict.ContainsKey(key))
            {
                s_varDict[key] = value;
            }
            else
            {
                s_varDict.Add(key, value);
            }
        }

        public static void Remove(byte key)
        {
            if (s_varDict.ContainsKey(key))
            {
                s_varDict.Remove(key);
            }
        }
    }
}
