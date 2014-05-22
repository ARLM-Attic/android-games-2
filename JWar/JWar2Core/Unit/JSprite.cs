using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JWar2Core
{
    public class JSprite : JObject
    {
        JModel _model;

        public JMapPos Pos { get; set; }
        private Vector2 _positionInMap;
        public Vector2 PositionInMap
        {
            get
            {
                return _positionInMap;
            }
            set
            {
                _positionInMap.X = value.X;
                _positionInMap.Y = value.Y;
            }
        }

        public int ActionId { get; set; }
        public int DirectionId { get; set; }
        public int FrameId { get; set; }

        public JSprite(JModel model)
        {
            _model = model;

            ActionId = 1;
            DirectionId = 1;
            FrameId = 1;

            Pos = new JMapPos(1, 1);
        }

        private Vector2 _position;
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
            }
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            JFrame frame = _model.GetFrame(ActionId, DirectionId, FrameId);

            Rectangle rect = new Rectangle((int)this.Position.X - frame.OffsetX,
                (int)this.Position.Y - frame.OffsetY,
                frame.Width,
                frame.Height);
            spriteBatch.Draw(frame.GetTexture(), rect, Color.White);

            base.OnDraw(spriteBatch, gameTime);
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void Move(float offsetX, float offsetY)
        {
            _positionInMap.X += offsetX;
            _positionInMap.Y += offsetY;
        }
    }
}
