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

            XElement xP = new XElement("project");
            xP.Add(new XAttribute("nanme", project.Name));
            xP.Add(new XAttribute("date-version", dateVersion));
            xP.Add(new XAttribute("template-name", project.TPName));
            xP.Add(new XAttribute("template-version", project.TPVersion));
            xDoc.Add(xP);

            #region save audios
            XElement xa = new XElement("audio-list");
            xP.Add(xa);
            for (int ia = 0; ia < project.Audios.Count; ia++)
            {
                AGAudio audio = project.Audios[ia];
                XElement xai = new XElement("audio");
                xa.Add(xai);
                xai.Add(new XAttribute("unique-id", audio.UniqueId));
                xai.Add(new XAttribute("id", audio.Id));
                xai.Add(new XAttribute("caption", audio.Caption));
                xai.Add(new XAttribute("file-name", audio.FilePath));
                xai.Add(new XAttribute("category-id", audio.CategoryId));
            }
            #endregion

            XElement xm = new XElement("model-list");
            xP.Add(xm);
            for (int im = 0; im < project.Models.Count; im++)
            {
                AGModelRef model = project.Models[im];
                XElement xmi = new XElement("model");
                xm.Add(xmi);
                xmi.Add(new XAttribute("model-unique-id", model.ModelUniqueId));
                xmi.Add(new XAttribute("caption", model.Caption));
                xmi.Add(new XAttribute("category-id", model.CategoryId));
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

            XElement xEl = xDoc.Element("project");
            AGEProject project = new AGEProject();
            project.Path = filePath;
            project.Name = xEl.XGetAttrString("name");
            project.DateVersion = xEl.XGetAttrStringValue("date-version", DateTime.MinValue.ToString(_dateVersionFormat));
            project.TPName = xEl.XGetAttrString("template-name");
            project.TPVersion = xEl.XGetAttrString("template-version");

            // 加载TProject
            project.TProject = AGECache.Current.TProjectStore.GetTProject(project.TPName);

            #region load audios
            List<XElement> xAis = xEl.XGetElement("audio-list").XGetElements("audio").ToList();
            for (int ia = 0; ia < xAis.Count; ia++)
            {
                XElement xAudio = xAis[ia];
                AGAudio audio = new AGAudio(xAudio.XGetAttrGuid("unique-id"));
                project.AddAudio(audio);

                audio.Id = xAudio.XGetAttrInt("id");
                audio.Caption = xAudio.XGetAttrString("caption");
                audio.FilePath = xAudio.XGetAttrString("file-name");
                audio.CategoryId = xAudio.XGetAttrInt("category-id");
            }
            #endregion

            List<XElement> xMis = xEl.Element("model-list").Elements("model").ToList();
            for (int im = 0; im < xMis.Count; im++)
            {
                XElement xmi = xMis[im];
                AGModelRef modelRef = new AGModelRef();
                modelRef.ModelUniqueId = xmi.XGetAttrGuid("model-unique-id");
                modelRef.Caption = xmi.XGetAttrString("caption");
                modelRef.CategoryId = xmi.XGetAttrInt("category-id");
                project.Models.Add(modelRef);
            }
            return project;
        }
    }
}
