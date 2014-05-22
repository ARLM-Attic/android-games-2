using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Net
{
    public class BufferUtil
    {
        public static void SetByte(byte[] buffer, byte value, ref int offset)
        {
            buffer[offset] = value;
            offset++;
        }

        public static byte GetByte(byte[] buffer, ref int offset)
        {
            byte returnValue = buffer[offset];
            offset++;
            return returnValue;
        }

        /// <summary>
        /// 把字符串填充到buffer中，填充区域是[index, index+length)
        /// </summary>
        public static void SetString(byte[] buffer, string value, int length, ref int index)
        {
            byte[] valueBuffer = Encoding.UTF8.GetBytes(value);
            for (int copyIndex = 0; copyIndex < length; copyIndex++)
            {
                if (copyIndex < valueBuffer.Length)
                {
                    buffer[index + copyIndex] = valueBuffer[copyIndex];
                }
                else
                {
                    buffer[index + copyIndex] = 0;
                }
            }
            index = index + length;
        }

        /// <summary>
        /// 将buffer中的[index, index+length)区域的数据转换为字符串
        /// </summary>
        public static string GetString(byte[] buffer, int length, ref int index)
        {
            string returnString = Encoding.UTF8.GetString(buffer, index, length).TrimEnd('\0');
            index += length;
            return returnString;
        }
    }
}
