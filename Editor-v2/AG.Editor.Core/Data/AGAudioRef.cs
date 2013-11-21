using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.Editor.Core.Data
{
    public class AGAudioRef
    {
        public int ActionId { get; set; }
        public int FrameIndex { get; set; }
        public Guid AudioUniqueId { get; set; }

        public AGAudioRef()
        {
        }

        public AGAudioRef(int actionId, int frameIndex, Guid audioUniqueId)
        {
            ActionId = actionId;
            FrameIndex = frameIndex;
            AudioUniqueId = audioUniqueId;
        }
    }
}
