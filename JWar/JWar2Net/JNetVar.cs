using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Net
{
    public static class JNetVar
    {
        private static Dictionary<byte, byte> s_varDict = new Dictionary<byte, byte>();
        private static Dictionary<byte, object> s_varObjDict = new Dictionary<byte, object>();

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

        public static object GetObj(byte key)
        {
            if (s_varObjDict.ContainsKey(key))
            {
                return s_varObjDict[key];
            }
            return null;
        }

        public static void SetObj(byte key, object value)
        {
            if (s_varObjDict.ContainsKey(key))
            {
                s_varObjDict[key] = value;
            }
            else
            {
                s_varObjDict.Add(key, value);
            }
        }

        public static void RemoveObj(byte key)
        {
            if (s_varObjDict.ContainsKey(key))
            {
                s_varObjDict.Remove(key);
            }
        }
    }
}
