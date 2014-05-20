using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Core
{
    public class Direction2DDef
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Caption { get; set; }

        private Direction2DDef(int id, string code, string caption)
        {
            Id = id;
            Code = code;
            Caption = caption;
        }

        public override string ToString()
        {
            return Caption;
        }

        public static readonly Direction2DDef South = new Direction2DDef(0x01, "S", "下");
        public static readonly Direction2DDef SouthWest = new Direction2DDef(0x02, "SW", "左下");
        public static readonly Direction2DDef West = new Direction2DDef(0x03, "W", "左");
        public static readonly Direction2DDef NorthWest = new Direction2DDef(0x04, "NW", "左上");
        public static readonly Direction2DDef North = new Direction2DDef(0x05, "N", "上");
        public static readonly Direction2DDef NorthEast = new Direction2DDef(0x06, "NE", "右上");
        public static readonly Direction2DDef East = new Direction2DDef(0x07, "E", "右");
        public static readonly Direction2DDef SouthEast = new Direction2DDef(0x08, "SE", "右下");

        private static List<Direction2DDef> _defs = new List<Direction2DDef>();

        public static List<Direction2DDef> GetDefs()
        {
            if (_defs.Count == 0)
            {
                _defs.Add(South);
                _defs.Add(SouthWest);
                _defs.Add(West);
                _defs.Add(NorthWest);
                _defs.Add(North);
                _defs.Add(NorthEast);
                _defs.Add(East);
                _defs.Add(SouthEast);
            }

            return _defs;
        }
    }
}