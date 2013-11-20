using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGEditor
{
    public class AGEContext
    {
        private static AGEContext _context;
        
        /// <summary>
        /// get current context information
        /// <para>this is a singleton instance</para>
        /// </summary>
        public static AGEContext Current
        {
            get
            {
                if (_context == null)
                {
                    _context = new AGEContext();
                }
                return _context;
            }
        }

        /// <summary>
        /// get curent config info
        /// </summary>
        public AGEditorConfig Config { get; set; }

        /// <summary>
        /// 获取模型的总数量
        /// </summary>
        /// <returns></returns>
        public int GetModelCount()
        {
            int count = 0;
            List<ModelCategory> categories = ModelCategory.GetDefs();
            foreach (var category in categories)
            {
                List<Model2D> models = DATUtility.GetModels(category.Id);
                count += models.Count;
            }
            return count;
        }
    }
}
