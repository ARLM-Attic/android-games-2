using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.Editor.Core.Data
{
    public class AGFrame
    {
        public int Id { get; set; }
        public string ImageFileName { get; set; }
        public AGDirection Direction { get; set; }
        public int AnchorPointX { get; set; }
        public int AnchorPointY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
