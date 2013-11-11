using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace AGEditor
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class AGEConfigUtil
    {
        private const string s_config_file = @"config.xml";
        private const string s_config_relative_folder = @".\configs\";
        /// <summary>
        /// 配置文件的物理路径
        /// </summary>
        private static string s_config_physical_file;
        /// <summary>
        /// 配置文件目录的物理路径
        /// </summary>
        private static string s_config_physical_folder;
        public static void Init(AppDomain appDomain)
        {
            s_config_physical_folder = Path.Combine(appDomain.BaseDirectory, s_config_relative_folder);
            s_config_physical_file = Path.Combine(s_config_physical_folder, s_config_file);
        }

        /// <summary>
        /// 
        /// 从目录.\configs\config.xml中读取配置信息
        /// </summary>
        /// <returns></returns>
        public static AGEditorConfig GetConfig()
        {
            if (!File.Exists(s_config_physical_file))
            {
                return null;
            }

            AGEditorConfig config = new AGEditorConfig();
            XDocument xDoc = XDocument.Load(s_config_physical_file);

            // 获取当前的工作空间
            XElement xWS = xDoc.Element("editor").Element("workspace");
            config.Workspace = new AGWorkspace(xWS.Attribute("name").Value, xWS.Attribute("path").Value);

            // 获取历史工作空间
            List<XElement> xHWSList = xWS.Element("histories").Elements("workspace").ToList();
            foreach (var xHWS in xHWSList)
            {
                config.HistoryWorkspace.Add(new AGWorkspace(xHWS.Attribute("name").Value, xHWS.Attribute("path").Value));
            }

            return config;
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="config"></param>
        public static void SaveConfig(AGEditorConfig config)
        {
            // 创建or更新,true:create, false:update
            bool isCreateOrUpdate = true;

            // 目录不存在，创建目录
            if (!Directory.Exists(s_config_physical_folder))
            {
                Directory.CreateDirectory(s_config_physical_folder);
                isCreateOrUpdate = true;
            }

            // 检测文件是否存在
            if (!isCreateOrUpdate && !File.Exists(s_config_physical_file))
            {
                isCreateOrUpdate = true;
            }

            if (isCreateOrUpdate)
            {
                // create
                CreateConfigFile(s_config_physical_file, config);
            }
            else
            {
                // update
                UpdateConfigFile(s_config_physical_file, config);
            }
        }

        /// <summary>
        /// create config file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="config"></param>
        private static void CreateConfigFile(string path, AGEditorConfig config)
        {
            XDocument xDoc = new XDocument();
            XElement xRoot = new XElement("editor");
            xDoc.Add(xRoot);
            #region 创建workspace
            XElement xWS = new XElement("workspace");
            xWS.Add(new XAttribute("name", config.Workspace.Name));
            xWS.Add(new XAttribute("path", config.Workspace.Path));
            xRoot.Add(xWS);

            #region save workspace history
            XElement xHWSList = new XElement("histories");
            xWS.Add(xHWSList);
            foreach (var hws in config.HistoryWorkspace)
            {
                XElement xHWS = new XElement("workspace");
                xWS.Add(new XAttribute("name", hws.Name));
                xWS.Add(new XAttribute("path", hws.Path));
                xHWSList.Add(xHWS);
            }
            #endregion

            #endregion
            xDoc.Save(path);
        }

        /// <summary>
        /// update config file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="config"></param>
        private static void UpdateConfigFile(string path, AGEditorConfig config)
        {
            throw new Exception("not implement");
        }
    }
}
