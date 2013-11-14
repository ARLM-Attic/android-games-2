using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.Editor.Core.Data
{
    /// <summary>
    /// 对模型的引用，含有较少的信息
    /// </summary>
    public class AGModelRef
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public int CategoryId { get; set; }

        public AGModelRef()
        {
        }

        public AGModelRef(AGModel model)
        {
            Id = model.Id;
            Caption = model.Caption;
            this.CategoryId = model.CategoryId;
        }
    }
}
