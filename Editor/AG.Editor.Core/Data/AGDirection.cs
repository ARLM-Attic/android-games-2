using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;

namespace AG.Editor.Core.Data
{
    public class AGDirection
    {
        public int Id { get; set; }
        public string Caption { get; set; }

        public List<AGFrame> Frames { get; private set; }
        public AGAction Action { get; set; }

        public int? RefDirectionId { get; private set; }
        public AGDirection RefDirection { get; private set; }

        public AGDirection()
        {
            Frames = new List<AGFrame>();
        }

        public AGDirection(int id, int refDirectionId)
        {
            Id = id;
            RefDirectionId = refDirectionId;
        }

        /// <summary>
        /// 获取此方向上的帧，如果是引用其他方向则获取其他方向的帧
        /// </summary>
        /// <returns></returns>
        public List<AGFrame> GetFrames()
        {
            if (RefDirection != null)
            {
                return RefDirection.GetFrames();
            }
            return Frames;
        }

        /// <summary>
        /// 设置引用模式，设置之后，此direction所有的frame信息都将被删除，因为这些信息将会从引用的direction中取得
        /// </summary>
        /// <param name="direction"></param>
        public void SetRefDirection(AGDirection direction)
        {
            RefDirection = direction;
            RefDirectionId = RefDirection.Id;
        }

        public void AddFrame(AGFrame frame)
        {
            Frames.Add(frame);
            frame.Direction = this;
        }

        public override string ToString()
        {
            if (RefDirectionId != null)
            {
                return string.Format("{0}ref:{1}", Caption, RefDirection.Caption);
            }
            else
            {
                return Caption;
            }
        }

        public static string GetCaption(AGDirectionMode mode, int dirId)
        {
            if (mode == AGDirectionMode.Two)
            {
                string[] strs = { "右", "左" };
                return strs[dirId];
            }
            else if (mode == AGDirectionMode.Four)
            {
                string[] strs = { "右", "下", "左", "上" };
                return strs[dirId];
            }
            else if (mode == AGDirectionMode.Eight)
            {
                string[] strs = { "右", "右下", "下", "左下", "左", "左上", "上", "右上" };
                return strs[dirId];
            }
            else if (mode == AGDirectionMode.Sixteen)
            {
                string[] strs = { "右", "右下1", "右下2", "右3", 
                                    "下", "左下1", "左下2", "左下3",
                                    "左", "左上1","左上2","左上3", 
                                    "上", "右上1", "右上2", "右上3" };
                return strs[dirId];
            }
            return string.Empty;
        }
    }
}
