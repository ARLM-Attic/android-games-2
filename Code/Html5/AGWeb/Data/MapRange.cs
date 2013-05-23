using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGWeb
{
    public class MapRange
    {
        public int MapId { get; set; }
        public int StartRow { get; set; }
        public int StartCol { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int[] Cells { get; set; }
        public List<Object2D> Objs { get; set; }

        public MapRange()
        {
            Objs = new List<Object2D>();
        }
    }
}
