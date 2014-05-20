using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace JWar2Core
{
    public class JDirection
    {
        public int Id { get; set; }

        public List<JFrame> Frames { get; set; }

        public JDirection()
        {
        }

        /// <summary>
        /// 逆时针旋转
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int Left(int dir, int count)
        {
            return 0;
        }

        /// <summary>
        /// 顺时针调整
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int Right(int dir, int count)
        {
            int remainder = dir + count % 8;

            return remainder == 0 ? 8 : remainder;
        }

        public bool HasFrames()
        {
            if (Frames.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}