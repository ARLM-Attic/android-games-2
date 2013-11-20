using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.Editor.Core.Data
{
    public class AGAudioRef
    {
        public int ActionId { get; set; }
        public int FrameId { get; set; }
        public int AudioId { get; set; }

        public AGAudioRef()
        {
        }

        public AGAudioRef(int actionId, int frameId, int audioId)
        {
            ActionId = actionId;
            FrameId = frameId;
            AudioId = audioId;
        }
    }
}
