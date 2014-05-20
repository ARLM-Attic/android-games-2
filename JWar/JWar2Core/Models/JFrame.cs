using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace JWar2Core
{
    public class JFrame
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        private Texture2D _texture;

        public override string ToString()
        {
            return string.Format("frame{0}", Id);
        }

        public void SetTexture(Texture2D texture)
        {
            _texture = texture;
        }

        public Texture2D GetTexture()
        {
            return _texture;
        }
    }
}