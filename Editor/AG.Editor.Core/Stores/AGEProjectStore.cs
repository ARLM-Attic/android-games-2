using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AG.Editor.Core.Data;
using System.Xml.Linq;

namespace AG.Editor.Core.Stores
{
    public class AGEProjectStore
    {
        private string _dateVersionFormat = "yy.MM.dd.hhmmss";
        public AGEProjectStore(string basePath)
        {
        }

        /// <summary>
        /// 保存项目信息。
        /// 日期版本信息会修改
        /// </summary>
        /// <param name="project"></param>
        public void SaveEProject(AGEProject project)
        {
            string filePath = project.Path;
            XDocument xDoc = null;

            string dateVersion = DateTime.Now.ToString(_dateVersionFormat);

            if (!File.Exists(project.Path))
            {
                FileInfo fileInfo = new FileInfo(project.Path);
                if (!Directory.Exists(fileInfo.DirectoryName))
                {
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }
                xDoc = new XDocument();
            }
            else
            {
                xDoc = XDocument.Load(project.Path);
                xDoc.RemoveNodes();
            }

            XElement xP = new XElement("p");
            xP.Add(new XAttribute("n", project.Name));
            xP.Add(new XAttribute("dtver", dateVersion));
            xP.Add(new XAttribute("tpn", project.TPName));
            xP.Add(new XAttribute("tpver", project.TPVersion));

            XElement xm = new XElement("m");
            xP.Add(xm);
            for (int im = 0; im < project.Models.Count; im++)
            {
                AGModelRef model = project.Models[im];
                XElement xmi = new XElement("mi");
                xmi.Add(new XAttribute("i", model.Id));
                xmi.Add(new XAttribute("n", model.Caption));
                xmi.Add(new XAttribute("mci", model.CategoryId));

                xm.Add(xmi);
            }

            xDoc.Add(xP);

            xDoc.Save(filePath);
            project.DateVersion = dateVersion;
        }

        public AGEProject GetEProject(string filePath)
        {
            XDocument xDoc = XDocument.Load(filePath);

            XElement xEl = xDoc.Element("p");
            AGEProject project = new AGEProject();
            project.Path = filePath;
            project.Name = xEl.Attribute("n").Value;
            project.DateVersion = xEl.Attribute("dtver").XGetValueString(DateTime.MinValue.ToString(_dateVersionFormat));
            project.TPName = xEl.Attribute("tpn").Value;
            project.TPVersion = xEl.Attribute("tpver").Value;

            // 加载TProject
            project.TProject = AGECache.Current.TProjectStore.GetTProject(project.TPName);

            return project;
        }
    }
}
