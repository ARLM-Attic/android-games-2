using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.Editor.Core.Metadata
{
    public class AGAudioCategory
    {
        public int Id { get; set; }
        public string Caption { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", Caption, Id);
        }
    }
}
