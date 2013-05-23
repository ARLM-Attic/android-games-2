//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Xml.Linq;

//namespace AGEditer
//{
//    public static class ModelLoader
//    {
//        public static Model2D Load(string fileName)
//        {
//            // 创建临时目录
//            string tempFolder = string.Format("{0}\\temp\\{1}",AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid());
//            if (!System.IO.Directory.Exists(tempFolder))
//                {
//                    System.IO.Directory.CreateDirectory(tempFolder);
//            }

//            // 解压到临时目录
//            PackagingUtility.DecompressFile(fileName, tempFolder);

//            string mxFile = new System.IO.DirectoryInfo(tempFolder).GetFiles("*.mx")[0].FullName;

//            return LoadFromMX(mxFile);
//        }

//        private static Model2D LoadFromMX(string mxFile)
//        {
//            System.IO.FileInfo fileInfo = new System.IO.FileInfo(mxFile);

//            List<Action2DDef> actionDefs = Action2DDef.GetDefs();
//            List<Direction2DDef> directionDefs = Direction2DDef.GetDefs();

//            Model2D model = new Model2D();
//            XDocument xDoc = XDocument.Load(mxFile);

//            XElement xRoot = xDoc.Element("model");
//            model.Id = Convert.ToInt32(xRoot.Attribute("id").Type);
//            model.Caption = xRoot.Attribute("caption").Type;

//            IEnumerable<XElement> xActions = xRoot.Elements("action");
//            foreach (var xAction in xActions)
//            {
//                Action2D action = new Action2D();

//                action.Id = Convert.ToInt32(xAction.Attribute("id").Type);

//                foreach (var actionDef in actionDefs)
//                {
//                    if (actionDef.Id == action.Id)
//                    {
//                        action.Caption = actionDef.Caption;
//                        break;
//                    }
//                }

//                IEnumerable<XElement> xDirections = xAction.Elements("direction");
//                foreach (var xDirection in xDirections)
//                {
//                    Direction2D direction = new Direction2D();
//                    direction.Id = Convert.ToInt32(xDirection.Attribute("id").Type);


//                    foreach (var directionDef in directionDefs)
//                    {
//                        if (directionDef.Id == direction.Id)
//                        {
//                            direction.Caption = directionDef.Caption;
//                            break;
//                        }
//                    }

//                    IEnumerable<XElement> xFrames = xDirection.Elements("frame");
//                    foreach (var xFrame in xFrames)
//                    {
//                        Frame2D frame = new Frame2D();
//                        frame.Index = Convert.ToInt32(xFrame.Attribute("index").Type);
//                        frame.Width = Convert.ToInt32(xFrame.Attribute("width").Type);
//                        frame.Height = Convert.ToInt32(xFrame.Attribute("height").Type);
//                        frame.OffsetX = Convert.ToInt32(xFrame.Attribute("offset-x").Type);
//                        frame.offsetY = Convert.ToInt32(xFrame.Attribute("offset-y").Type);
//                        frame.FileName = string.Format("{0:d4}-{1:d4}-{2:d4}-{3:d4}", model.Id, action.Id, direction.Id, frame.Index);
//                        frame.OrginalFile = string.Format("{0}\\{1}", fileInfo.Directory.FullName, frame.FileName);

//                        direction.Frames.Add(frame);
//                    }

//                    action.Directions.Add(direction);
//                }

//                model.Actions.Add(action);
//            }

//            return model;
//        }
//    }
//}
