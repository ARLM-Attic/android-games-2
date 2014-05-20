using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace JWar2Core
{
    public class Frame2D
    {
        public int Index { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int OffsetX { get; set; }
        public int offsetY { get; set; }

        public byte[] Data { get; set; }

        public Texture2D Texture { get; set; }

        public override string ToString()
        {
            return string.Format("frame{0}", Index);
        }
    }
}