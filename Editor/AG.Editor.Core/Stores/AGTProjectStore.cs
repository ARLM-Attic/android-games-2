using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;
using System.IO;
using System.Xml.Linq;

namespace AG.Editor.Core.Stores
{
    public class AGTProjectStore
    {
        private string _relativeFolder = ".\\metadata\\projects\\";
        private string _folder;
        public AGTProjectStore(string basePath)
        {
            _folder = Path.Combine(basePath, _relativeFolder);
        }

        public List<AGTProjectSummary> GetTProjects()
        {
            List<AGTProjectSummary> list = new List<AGTProjectSummary>();
            string[] fileNames = Directory.GetFiles(_folder);
            foreach (var fileName in fileNames)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Extension == ".xml")
                {
                    list.Add(GetTProjectSummary(fileInfo.FullName));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取详细的项目模板信息
        /// </summary>
        /// <param name="summary"></param>
        /// <returns></returns>
        public AGTProject GetTProject(string name)
        {
            string filePath = string.Format("{0}{1}.xml", _folder, name);
            XDocument xDoc = XDocument.Load(filePath);
            XElement xRoot = xDoc.Element("pt");
            string frameworkVer = xRoot.Attribute("fver").Value;

            if (frameworkVer == "1.0")
            {
                return V1_0.AGTProjectXML1_0.ConvertTProjectFromXml(xRoot);
            }
            return null;
        }

        private AGTProjectSummary GetTProjectSummary(string filePath)
        {
            XDocument xDoc = XDocument.Load(filePath);
            XElement xRoot = xDoc.Element("pt");
            string frameworkVer = xRoot.Attribute("fver").Value;

            if (frameworkVer == "1.0")
            {
                return V1_0.AGTProjectXML1_0.ConvertTProjectSummaryFromXml(xRoot);
            }
            return null;
        }


    }
}
