using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;

namespace AG.Editor.Core.Data
{
    public class AGAudio
    {
        public Guid UniqueId { get; set; }

        public int Id { get; set; }
        public string Caption { get; set; }
        public string FilePath { get; set; }
        public int CategoryId { get; set; }
        public AGAudioCategory Category { get; set; }

        public AGAudio(Guid uniqueId)
        {
            UniqueId = uniqueId;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", Caption, Id);
        }
    }
}
