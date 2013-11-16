using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;

namespace AG.Editor.Core.Data
{
    public class AGModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }

        public int CategoryId { get; set; }
        public AGModelCategory Category { get; set; }

        public List<AGAction> Actions { get; private set; }

        public AGModel()
        {
            Actions = new List<AGAction>();
        }

        public void AddAction(AGAction action)
        {
            Actions.Add(action);
            action.Model = this;
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", Caption, Id);
        }
    }
}
