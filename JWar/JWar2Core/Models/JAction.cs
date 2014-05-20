using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace JWar2Core
{
    public class JAction
    {
        public int Id { get; set; }

        public List<JDirection> Directions { get; set; }

        public JAction()
        {
        }

        public bool HasFrames()
        {
            for (int i = 0; i < Directions.Count; i++)
            {
                if (Directions[i].HasFrames())
                {
                    return true;
                }
            }
            return false;
        }
    }
}