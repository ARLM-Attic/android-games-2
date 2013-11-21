using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.Editor.Core.Data
{
    /// <summary>
    /// action信息是由metadata定义，id无法修改
    /// </summary>
    public class AGAction
    {
        public int Id { get; private set; }
        public string Caption { get; set; }

        public List<AGDirection> Directions { get; private set; }

        public AGModel Model { get; set; }

        public AGAction(int id)
        {
            Id = id;
            Directions = new List<AGDirection>();
        }

        //public void AddDirection(AGDirection direction)
        //{
        //    Directions.Add(direction);
        //    direction.Action = this;
        //}

        public override string ToString()
        {
            return Caption;
        }

        /// <summary>
        /// 将sourceDir的引用到其他所有方位
        /// </summary>
        /// <param name="sourceDir"></param>
        public void CopyRefToAll(AGDirection sourceDir)
        {
            for (int iDir = 0; iDir < Directions.Count; iDir++)
            {
                AGDirection direction = Directions[iDir];
                if (direction.Id != sourceDir.Id)
                {
                    direction.SetRefDirection(sourceDir);
                }
            }
        }

        public AGDirection GetDirection(int dirId)
        {
            for (int iDir = 0; iDir < Directions.Count; iDir++)
            {
                AGDirection direction = Directions[iDir];
                if (direction.Id == dirId)
                {
                    return direction;
                }
            }
            return null;
        }
    }
}
