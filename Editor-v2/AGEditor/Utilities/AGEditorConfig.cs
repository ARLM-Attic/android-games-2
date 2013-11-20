using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGEditor
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class AGEditorConfig
    {
        /// <summary>
        /// 工作路径
        /// </summary>
        public AGWorkspace Workspace { get; set; }
        /// <summary>
        /// 历史工作空间列表
        /// </summary>
        public List<AGWorkspace> HistoryWorkspace { get; set; }

        public AGEditorConfig()
        {
            HistoryWorkspace = new List<AGWorkspace>();
        }
    }

    public class AGWorkspace
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public AGWorkspace(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
