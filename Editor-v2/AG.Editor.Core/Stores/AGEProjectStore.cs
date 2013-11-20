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
            xDoc.Add(xP);

            #region save audios
            XElement xa = new XElement("a");
            xP.Add(xa);
            for (int ia = 0; ia < project.Audios.Count; ia++)
            {
                AGAudio audio = project.Audios[ia];
                XElement xai = new XElement("ai");
                xa.Add(xai);
                xai.Add(new XAttribute("i", audio.Id));
                xai.Add(new XAttribute("c", audio.Caption));
                xai.Add(new XAttribute("fp", audio.FilePath));
                xai.Add(new XAttribute("ci", audio.CategoryId));
            }
            #endregion

            XElement xm = new XElement("m");
            xP.Add(xm);
            for (int im = 0; im < project.Models.Count; im++)
            {
                AGModelRef model = project.Models[im];
                XElement xmi = new XElement("mi");
                xm.Add(xmi);
                xmi.Add(new XAttribute("i", model.Id));
                xmi.Add(new XAttribute("c", model.Caption));
                xmi.Add(new XAttribute("mci", model.CategoryId));
            }

            xDoc.Save(filePath);
            project.DateVersion = dateVersion;
        }

        /// <summary>
        /// 加载EProject对象
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
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

            #region load audios
            List<XElement> xAis = xEl.XGetElement("a").XGetElements("ai").ToList();
            for (int ia = 0; ia < xAis.Count; ia++)
            {
                AGAudio audio = new AGAudio();
                project.AddAudio(audio);

                audio.Id = xAis[ia].XGetAttrIntValue("i", AGECONST.INT_NULL);
                audio.Caption = xAis[ia].XGetAttrStringValue("c", "unknown");
                audio.FilePath = xAis[ia].XGetAttrStringValue("fp", "unknown");
                audio.CategoryId = xAis[ia].XGetAttrIntValue("ci", AGECONST.INT_NULL);
            }
            #endregion

            List<XElement> xMis = xEl.Element("m").Elements("mi").ToList();
            for (int im = 0; im < xMis.Count; im++)
            {
                XElement xmi = xMis[im];
                AGModelRef modelRef = new AGModelRef();
                modelRef.Id = Convert.ToInt32(xmi.Attribute("i").Value);
                modelRef.Caption = xmi.XGetAttrStringValue("c", "unknown");
                modelRef.CategoryId = Convert.ToInt32(xmi.Attribute("mci").Value);
                project.Models.Add(modelRef);
            }
            return project;
        }
    }
}
