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

            for (int iact = 0; iact < model.Actions.Count; iact++ )
            {
                AGAction action = model.Actions[iact];
                XElement xAct = new XElement("a");
                xAct.Add(new XAttribute("i", action.Id));
                xM.Add(xAct);

                for (int idir = 0; idir < action.Directions.Count; idir++)
                {
                    AGDirection direction = action.Directions[idir];
                    XElement xDir = new XElement("d");
                    xDir.Add(new XAttribute("i", direction.Id));
                    xAct.Add(xDir);

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
            }

            xDoc.Save(filePath);
        }
    }
}
