using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Data;
using System.Xml.Linq;
using System.IO;
using AG.Editor.Core.Metadata;

namespace AG.Editor.Core.Stores
{
    public class AGModelStore
    {
        /// <summary>
        /// 保存模型
        /// </summary>
        /// <param name="project"></param>
        /// <param name="model"></param>
        public void SaveModel(AGEProject project,  AGModel model)
        {
            string modelFolder = project.GetDataModelsFolder();
            string filePath = string.Format("{0}\\{1}.xml", modelFolder, model.UniqueId);
            XDocument xDoc = null;
            if (!File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (!Directory.Exists(fileInfo.DirectoryName))
                {
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }
                xDoc = new XDocument();
            }
            else
            {
                xDoc = XDocument.Load(filePath);
                xDoc.RemoveNodes();
            }

            XElement xM = new XElement("model");
            xM.Add(new XAttribute("unique-id", model.UniqueId));
            xM.Add(new XAttribute("id", model.Id));
            xM.Add(new XAttribute("caption", model.Caption));
            xM.Add(new XAttribute("category-id", model.CategoryId));
            xDoc.Add(xM);

            #region save audio ref
            XElement xARefs = new XElement("audio-ref-list");
            xM.Add(xARefs);
            for (int iAudio = 0; iAudio < model.AudioRefs.Count; iAudio++)
            {
                AGAudioRef audioRef = model.AudioRefs[iAudio];
                XElement xARef = new XElement("audio-ref");
                xARef.Add(new XAttribute("action-id", audioRef.ActionId));
                xARef.Add(new XAttribute("frame-index", audioRef.FrameIndex));
                xARef.Add(new XAttribute("audio-unique-id", audioRef.AudioUniqueId));
                xARefs.Add(xARef);
            }
            #endregion

            XElement xActionList = new XElement("action-list");
            xM.Add(xActionList);
            for (int iact = 0; iact < model.Actions.Count; iact++ )
            {
                #region // 保存action
                AGAction action = model.Actions[iact];
                XElement xAction = new XElement("action");
                xAction.Add(new XAttribute("id", action.Id));
                xAction.Add(new XAttribute("caption", action.Caption));
                xActionList.Add(xAction);

                for (int idir = 0; idir < action.Directions.Count; idir++)
                {
                    #region // 保存direction
                    AGDirection direction = action.Directions[idir];
                    if (direction.RefDirection != null)
                    {
                        // 此方向关联了别的方向
                        XElement xDir = new XElement("direction");
                        xDir.Add(new XAttribute("id", direction.Id));
                        xDir.Add(new XAttribute("ref-id", direction.RefId));
                        xAction.Add(xDir);
                    }
                    else
                    {
                        XElement xDir = new XElement("direction");
                        xDir.Add(new XAttribute("id", direction.Id));
                        xDir.Add(new XAttribute("caption", direction.Caption));
                        xAction.Add(xDir);
                        // 非引用方位，保存帧信息
                        for (int iframe = 0; iframe < direction.Frames.Count; iframe++)
                        {
                            AGFrame frame = direction.Frames[iframe];
                            XElement xFrame = new XElement("frame");
                            xFrame.Add(new XAttribute("unique-id", frame.UniqueId));
                            xFrame.Add(new XAttribute("id", frame.Id));
                            xFrame.Add(new XAttribute("file-name", frame.ImageFileName));
                            xFrame.Add(new XAttribute("width", frame.Width));
                            xFrame.Add(new XAttribute("height", frame.Height));
                            xFrame.Add(new XAttribute("anchor-x", frame.AnchorPointX));
                            xFrame.Add(new XAttribute("anchor-y", frame.AnchorPointY));

                            xDir.Add(xFrame);
                        }
                    }
                    #endregion
                }
                #endregion
            }

            xDoc.Save(filePath);
        }

        /// <summary>
        /// 读取模型
        /// </summary>
        /// <param name="project"></param>
        /// <param name="modelRef"></param>
        /// <returns></returns>
        public AGModel GetModel(AGEProject project, AGModelRef modelRef)
        {
            string modelFolder = project.GetDataModelsFolder();
            string filePath = string.Format("{0}\\{1}.xml", modelFolder, modelRef.ModelUniqueId);
            XDocument xDoc = null;
            if (!File.Exists(filePath))
            {
                return null;
            }
            else
            {
                xDoc = XDocument.Load(filePath);
            }

            XElement xm = xDoc.Element("model");
            int categoryId = xm.XGetAttrInt("category-id");
            AGModelCategory modelCategory = project.TProject.GetModelCategory(categoryId);
            AGModel model = AGModel.ModelWidthCategory(modelCategory,xm.XGetAttrGuid("unique-id"));
            model.Id = xm.XGetAttrInt("id");
            model.Caption = xm.XGetAttrString("caption");
            model.Category = modelCategory;
            model.CategoryId = modelCategory.Id;

            #region load audio ref
            List<XElement> xAudios = xm.XGetElement("audio-ref-list").XGetElements("audio-ref").ToList();
            for (int iAudio = 0; iAudio < xAudios.Count; iAudio++)
            {
                AGAudioRef audioRef = new AGAudioRef();
                audioRef.ActionId = xAudios[iAudio].XGetAttrIntValue("action-id", AGECONST.INT_NULL);
                audioRef.FrameIndex = xAudios[iAudio].XGetAttrIntValue("frame-index", AGECONST.INT_NULL);
                audioRef.AudioUniqueId = xAudios[iAudio].XGetAttrGuid("audio-unique-id");
                model.AudioRefs.Add(audioRef);
            }
            #endregion

            List<XElement> xas = xm.XGetElement("action-list").XGetElements("action").ToList();
            for (int ia = 0; ia < xas.Count; ia++)
            {
                #region 读取action
                XElement xa = xas[ia];
                AGAction action = model.GetAction(xa.XGetAttrInt("id"));
                action.Caption = xa.XGetAttrString("caption");

                List<XElement> xds = xa.XGetElements("direction").ToList();
                for (int iDir = 0; iDir < xds.Count; iDir++)
                {
                    XElement xd = xds[iDir];

                    AGDirection direction = action.GetDirection(xd.XGetAttrInt("id"));
                    int refId = xd.XGetAttrInt("ref-id");
                    if (refId != AGECONST.INT_NULL)
                    {
                        direction.RefId = refId;
                    }
                    else
                    {
                        List<XElement> xfs = xd.XGetElements("frame").ToList();
                        for (int iFrame = 0; iFrame < xfs.Count; iFrame++)
                        {
                            XElement xf = xfs[iFrame];

                            AGFrame frame = new AGFrame(xf.XGetAttrGuid("unique-id"));
                            frame.Id = xf.XGetAttrIntValue("id", 0);
                            frame.ImageFileName = xf.XGetAttrStringValue("file-name", "unknown");
                            frame.Width = xf.XGetAttrIntValue("width", 0);
                            frame.Height = xf.XGetAttrIntValue("height", 0);
                            frame.AnchorPointX = xf.XGetAttrIntValue("anchor-x", 0);
                            frame.AnchorPointY = xf.XGetAttrIntValue("anchor-y", 0);
                            direction.AddFrame(frame);
                        }
                    }
                }
                #endregion
            }

            #region 关联ref信息
            foreach (var action in model.Actions)
            {
                foreach (var dir in action.Directions)
                {
                    if (dir.RefId != null && dir.RefId != AGECONST.INT_NULL)
                    {
                        dir.SetRefDirection(action.GetDirection(dir.RefId.Value));
                    }
                }
            }
            #endregion

            return model;
        }
    }
}
