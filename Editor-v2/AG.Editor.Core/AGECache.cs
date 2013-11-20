using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Stores;

namespace AG.Editor.Core
{
    public class AGECache
    {
        private static AGECache s_instance;
        public static AGECache Current
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new AGECache();
                }
                return s_instance;
            }
        }

        public static void Init(AppDomain appDomain)
        {
            Current.MetadataStore = new AGMetadataStore(appDomain.BaseDirectory);
            Current.SettingsStore = new AGESettingsStore(appDomain.BaseDirectory);
            Current.TProjectStore = new AGTProjectStore(appDomain.BaseDirectory);
            Current.EProjectStore = new AGEProjectStore("");
            Current.ModelStore = new AGModelStore();
        }

        public AGMetadataStore MetadataStore { get; set; }
        /// <summary>
        /// 项目模板的存储接口
        /// </summary>
        public AGTProjectStore TProjectStore { get; set; }
        public AGESettingsStore SettingsStore { get; set; }
        public AGEProjectStore EProjectStore { get; set; }
        public AGModelStore ModelStore { get; set; }

        private AGECache()
        {
        }
    }
}
