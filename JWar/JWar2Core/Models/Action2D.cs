using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Core
{
    public class Action2D
    {
        public int Id { get; set; }
        public string Caption { get; set; }

        public List<Direction2D> Directions { get; set; }

        public Action2D()
        {
            Directions = new List<Direction2D>();
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