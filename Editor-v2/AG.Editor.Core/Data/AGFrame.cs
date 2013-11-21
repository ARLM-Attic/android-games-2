using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.Editor.Core.Data
{
    public class AGFrame
    {
        public Guid UniqueId { get; private set; }
        public int Id { get; set; }
        public string ImageFileName { get; set; }
        public int AnchorPointX { get; set; }
        public int AnchorPointY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// 引用的frameId
        /// </summary>
        public Guid RefFrameUniqueId { get; private set; }
        public AGFrame RefFrame { get; private set; }
        public AGDirection Direction { get; internal set; }

        public AGFrame()
        {
            UniqueId = Guid.NewGuid();
        }

        public AGFrame(Guid uniqueId)
        {
            UniqueId = uniqueId;
        }

        public AGFrame(Guid uniqueId, Guid refFrameUniqueId)
        {
            UniqueId = uniqueId;
            RefFrameUniqueId = refFrameUniqueId;
        }

        /// <summary>
        /// 设置引用的frame
        /// </summary>
        /// <param name="frame"></param>
        public void SetRefFrame(AGFrame frame)
        {
            RefFrameUniqueId = frame.UniqueId;
            RefFrame = frame;
        }

        public override string ToString()
        {
            if (RefFrameUniqueId != Guid.Empty)
            {
                return string.Format("ref:{0}", RefFrame.ImageFileName); 
            }
            else
            {
                return ImageFileName;
            }
        }
    }
}
