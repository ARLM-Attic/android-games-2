using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JWar2Net
{
    public class SerializeUtility
    {
        public static byte[] SerializeToBinary(object obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(ms, obj);

            byte[] resbytes = new byte[ms.Length];

            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(resbytes, 0, (int)ms.Length);

            return resbytes;
        }

        public static object DeserializeFromBinary(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            ms.Seek(0, SeekOrigin.Begin);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return binaryFormatter.Deserialize(ms);
        }
    }
}
