using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Data;
using System.Xml.Linq;
using System.IO;

namespace AG.Editor.Core.Stores
{
    public class AGModelStore
    {
        public void SaveModel(AGEProject project,  AGModel model)
        {
            string modelFolder = project.GetDataModelsFolder();
            string filePath = string.Format("{0}\\{1:d8}.xml", modelFolder, model.Id);
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

            XElement xM = new XElement("m");
            xM.Add(new XAttribute("i", model.Id));
            xM.Add(new XAttribute("c", model.Caption));
            xM.Add(new XAttribute("ci", model.CategoryId));
            xDoc.Add(xM);

            #region save audio ref
            XElement xARefs = new XElement("audio");
            xM.Add(xARefs);
            for (int iAudio = 0; iAudio < model.AudioRefs.Count; iAudio++)
            {
                AGAudioRef audioRef = model.AudioRefs[iAudio];
                XElement xARef = new XElement("i");
                xARef.Add(new XAttribute("a", audioRef.ActionId));
                xARef.Add(new XAttribute("f", audioRef.FrameId));
                xARef.Add(new XAttribute("ref", audioRef.AudioId));
                xARefs.Add(xARef);
            }
            #endregion

            for (int iact = 0; iact < model.Actions.Count; iact++ )
            {
                // 保存action
                AGAction action = model.Actions[iact];
                XElement xAct = new XElement("a");
                xAct.Add(new XAttribute("i", action.Id));
                xM.Add(xAct);

                for (int idir = 0; idir < action.Directions.Count; idir++)
                {
                    #region // 保存direction
                    AGDirection direction = action.Directions[idir];
                    if (direction.RefDirectionId != null)
                    {
                        XElement xDir = new XElement("d");
                        xDir.Add(new XAttribute("i", direction.Id));
                        xDir.Add(new XAttribute("ref", direction.RefDirectionId));
                        xAct.Add(xDir);
                    }
                    else
                    {
                        XElement xDir = new XElement("d");
                        xDir.Add(new XAttribute("i", direction.Id));
                        xAct.Add(xDir);
                        // 非引用方位，保存帧信息
                        for (int iframe = 0; iframe < direction.Frames.Count; iframe++)
                        {
                            AGFrame frame = direction.Frames[iframe];
                            XElement xFrame = new XElement("f");
                            xFrame.Add(new XAttribute("i", frame.Id));
                            xFrame.Add(new XAttribute("fn", frame.ImageFileName));
                            xFrame.Add(new XAttribute("w", frame.Width));
                            xFrame.Add(new XAttribute("h", frame.Height));
                            xFrame.Add(new XAttribute("ax", frame.AnchorPointX));
                            xFrame.Add(new XAttribute("ay", frame.AnchorPointY));

                            xDir.Add(xFrame);
                        }
                    }
                    #endregion
                }
            }

            xDoc.Save(filePath);
        }

        public AGModel GetModel(AGEProject project, AGModelRef modelRef)
        {
            string modelFolder = project.GetDataModelsFolder();
            string filePath = string.Format("{0}\\{1:d8}.xml", modelFolder, modelRef.Id);
            XDocument xDoc = null;
            if (!File.Exists(filePath))
            {
                return null;
            }
            else
            {
                xDoc = XDocument.Load(filePath);
            }

            AGModel model = new AGModel();
            XElement xm = xDoc.Element("m");
            model.Id = xm.XGetAttrIntValue("i", 0);
            model.Caption = xm.XGetAttrStringValue("c", "unknown");
            model.CategoryId = xm.XGetAttrIntValue("ci", 0);

            #region load audio ref
            List<XElement> xAudios = xm.XGetElement("audio").XGetElements("i").ToList();
            for (int iAudio = 0; iAudio < xAudios.Count; iAudio++)
            {
                AGAudioRef audioRef = new AGAudioRef();
                audioRef.ActionId = xAudios[iAudio].XGetAttrIntValue("a", AGECONST.INT_NULL);
                audioRef.FrameId = xAudios[iAudio].XGetAttrIntValue("f", AGECONST.INT_NULL);
                audioRef.AudioId = xAudios[iAudio].XGetAttrIntValue("ref", AGECONST.INT_NULL);
                model.AudioRefs.Add(audioRef);
            }
            #endregion

            List<XElement> xas = xm.Elements("a").ToList();
            for (int ia = 0; ia < xas.Count; ia++)
            {
                XElement xa = xas[ia];
                AGAction action = new AGAction();
                action.Id = xa.XGetAttrIntValue("i", 0);
                action.Caption = project.GetActionCapton(model.CategoryId, action.Id);
                model.AddAction(action);

                List<XElement> xds = xa.Elements("d").ToList();
                for (int iDir = 0; iDir < xds.Count; iDir++)
                {
                    XElement xd = xds[iDir];

                    int refDirId = xd.XGetAttrIntValue("ref", AGECONST.INT_NULL);
                    if (refDirId != AGECONST.INT_NULL)
                    {
                        int dirId = xd.XGetAttrIntValue("i", 0);
                        AGDirection direction = new AGDirection(dirId, refDirId);
                        direction.Caption = project.GetDirectionCapton(model.CategoryId, action.Id, direction.Id);
                        action.AddDirection(direction);
                    }
                    else
                    {
                        AGDirection direction = new AGDirection();
                        direction.Id = xd.XGetAttrIntValue("i", 0);
                        direction.Caption = project.GetDirectionCapton(model.CategoryId, action.Id, direction.Id);
                        action.AddDirection(direction);
                        List<XElement> xfs = xd.Elements("f").ToList();
                        for (int iFrame = 0; iFrame < xfs.Count; iFrame++)
                        {
                            XElement xf = xfs[iFrame];

                            AGFrame frame = new AGFrame();
                            frame.Id = xf.XGetAttrIntValue("i", 0);
                            frame.ImageFileName = xf.XGetAttrStringValue("fn", "unknown");
                            frame.Width = xf.XGetAttrIntValue("w", 0);
                            frame.Height = xf.XGetAttrIntValue("h", 0);
                            frame.AnchorPointX = xf.XGetAttrIntValue("ax", 0);
                            frame.AnchorPointY = xf.XGetAttrIntValue("ay", 0);
                            direction.AddFrame(frame);
                        }
                    }
                }
            }

            #region 关联ref信息
            foreach (var action in model.Actions)
            {
                foreach (var dir in action.Directions)
                {
                    if (dir.RefDirectionId != null)
                    {
                        dir.SetRefDirection(action.GetDirection(dir.RefDirectionId.Value));
                    }
                }
            }
            #endregion

            return model;
        }
    }
}
