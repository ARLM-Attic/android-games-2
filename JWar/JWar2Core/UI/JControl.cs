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
    public class JControl : JObject
    {
        private Vector2 _position;
        private Vector2 _size;
        private Rectangle _rect;

        /// <summary>
        /// 文本内容
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position.X = value.X;
                _position.Y = value.Y;
                _rect.X = (int)_position.X;
                _rect.Y = (int)_position.Y;
            }
        }
        /// <summary>
        /// 字体
        /// </summary>
        public SpriteFont Font { set; get; }
        /// <summary>
        /// 前景色
        /// </summary>
        public Color Foreground { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color Background { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size.X = value.X;
                _size.Y = value.Y;
                _rect.Width = (int)_size.X;
                _rect.Height = (int)_size.Y;
            }
        }

        public Rectangle Rect
        {
            get
            {
                return _rect;
            }
        }

        public JControl()
        {
            Foreground = Color.Black;
            Background = Color.White;
            Font = JResource.Load<SpriteFont>("SpriteFont1");
        }

        /// <summary>
        /// 判断点是否在范围内
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsHitTest(int x, int y)
        {
            if (Rect.Contains(x, y))
            {
                return true;
            }
            return false;
        }
    }
}
