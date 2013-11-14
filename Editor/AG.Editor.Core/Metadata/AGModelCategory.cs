using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Data;

namespace AG.Editor.Core.Metadata
{
    public class AGModelCategory
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public AGDirectionMode DirectionMode { get; set; }

        public List<AGAction> Actions { get; set; }

        public AGModelCategory()
        {
            Actions = new List<AGAction>();
        }
    }
}
