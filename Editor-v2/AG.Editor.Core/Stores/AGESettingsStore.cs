using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Data;
using System.IO;
using AG.Editor.Core.Settings;
using System.Xml.Linq;

namespace AG.Editor.Core.Stores
{
    public class AGESettingsStore
    {

        private const string s_config_file = @".\configs\settings.xml";
        private const string s_config_relative_folder = @".\configs\";
        private string _settingsFile;
        private string _settingsFolder;
        public AGESettingsStore(string basePath)
        {
            _settingsFile = Path.Combine(basePath, s_config_file);
            _settingsFolder = Path.Combine(basePath, s_config_relative_folder);
        }

        public void SaveProject(AGEProject project)
        {
        }

        /// <summary>
        /// 
        /// 从目录.\configs\config.xml中读取配置信息
        /// </summary>
        /// <returns></returns>
        public AGESettings GetSettings()
        {
            if (!File.Exists(_settingsFile))
            {
                return null;
            }

            AGESettings settings = new AGESettings();
            XDocument xDoc = XDocument.Load(_settingsFile);

            // 获取当前的工作空间
            XElement xWS = xDoc.Element("settings").Element("p");
            settings.LatestEProjectPath = xWS.Attribute("path").Value;

            // 获取历史工作空间
            //List<XElement> xHWSList = xWS.Element("histories").Elements("workspace").ToList();
            //foreach (var xHWS in xHWSList)
            //{
            //    config.HistoryWorkspace.Add(new AGWorkspace(xHWS.Attribute("name").Value, xHWS.Attribute("path").Value));
            //}

            return settings;
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="config"></param>
        public void SaveSettings(AGESettings config)
        {
            // 创建or更新,true:create, false:update
            bool isCreateOrUpdate = true;

            // 目录不存在，创建目录
            if (!Directory.Exists(_settingsFolder))
            {
                Directory.CreateDirectory(_settingsFolder);
                isCreateOrUpdate = true;
            }

            // 检测文件是否存在
            if (!isCreateOrUpdate && !File.Exists(_settingsFile))
            {
                isCreateOrUpdate = true;
            }

            if (isCreateOrUpdate)
            {
                // create
                CreateConfigFile(_settingsFile, config);
            }
            else
            {
                // update
                UpdateConfigFile(_settingsFile, config);
            }
        }

        /// <summary>
        /// create config file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="config"></param>
        private void CreateConfigFile(string path, AGESettings config)
        {
            XDocument xDoc = new XDocument();
            XElement xRoot = new XElement("settings");
            xDoc.Add(xRoot);
            #region save latest project path
            XElement xWS = new XElement("p");
            xWS.Add(new XAttribute("path", config.LatestEProjectPath));
            xRoot.Add(xWS);

            #region save workspace history
            //XElement xHWSList = new XElement("histories");
            //xWS.Add(xHWSList);
            //foreach (var hws in config.HistoryWorkspace)
            //{
            //    XElement xHWS = new XElement("workspace");
            //    xWS.Add(new XAttribute("name", hws.Name));
            //    xWS.Add(new XAttribute("path", hws.Path));
            //    xHWSList.Add(xHWS);
            //}
            #endregion

            #endregion
            xDoc.Save(path);
        }

        /// <summary>
        /// update config file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="config"></param>
        private void UpdateConfigFile(string path, AGESettings config)
        {
            throw new Exception("not implement");
        }
    }
}
