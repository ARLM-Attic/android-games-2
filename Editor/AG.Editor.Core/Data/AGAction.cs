using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.Editor.Core.Data
{
    public class AGAction
    {
        public int Id { get; set; }
        public string Caption { get; set; }

        public List<AGDirection> Directions { get; private set; }

        public AGModel Model { get; set; }

        public AGAction()
        {
            Directions = new List<AGDirection>();
        }

        public void AddDirection(AGDirection direction)
        {
            Directions.Add(direction);
            direction.Action = this;
        }
    }
}
