using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;

namespace AG.Editor.Core.Data
{
    public class AGModelSummary
    {
        public int Id { get; set; }
        public string Caption { get; set; }

        public int CategoryId { get; set; }
        public AGModelCategory Category { get; set; }
    }
}
