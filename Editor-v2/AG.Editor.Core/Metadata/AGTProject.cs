using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.Editor.Core.Metadata
{
    public class AGTProject
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Version { get; set; }

        /// <summary>
        /// 模型类别列表
        /// </summary>
        public List<AGModelCategory> ModelCategories { get; set; }
        public List<AGAudioCategory> AudioCateogries { get; set; }

        public AGTProject()
        {
            ModelCategories = new List<AGModelCategory>();
            AudioCateogries = new List<AGAudioCategory>();
        }

        /// <summary>
        /// 获取模型定义
        /// </summary>
        /// <param name="modelCategoryId"></param>
        /// <returns></returns>
        public AGModelCategory GetModelCategory(int modelCategoryId)
        {
            foreach (var item in ModelCategories)
            {
                if (item.Id == modelCategoryId)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
