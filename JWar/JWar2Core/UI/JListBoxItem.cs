using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JWar2Core.UI
{
    public class JListBoxItem
    {
        public string Text { get; set; }
        public object Tag { get; set; }

        public JListBoxItem(string text)
        {
            Text = text;
        }
    }
}
