using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class ResourceLoader
{
    public static Stream GetResources(int modelId, int actionId, int directionId, int frameIndex)
    {
        Stream outputStream = new MemoryStream();

        string modelFile = string.Format("c:\\ag\\data\\models\\{0:d4}.m", modelId);
        string frameFile = string.Format("/{0:d4}-{1:d4}-{2:d4}-{3:d4}", modelId, actionId, directionId, frameIndex);
        using (Package zip = System.IO.Packaging.Package.Open(modelFile, FileMode.Open))
        {
            foreach (PackagePart part in zip.GetParts())
            {
                if (part.Uri == new Uri(frameFile, UriKind.Relative))
                {
                    using (Stream inFileStream = part.GetStream())
                    {
                        long bufferSize = inFileStream.Length < 4096 ? inFileStream.Length : 4096;
                        byte[] buffer = new byte[bufferSize];
                        int bytesRead = 0;
                        long bytesWritten = 0;
                        while ((bytesRead = inFileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            outputStream.Write(buffer, 0, bytesRead);
                            bytesWritten += bufferSize;
                        }
                        break;
                    }
                }
            }
        }
        return outputStream;
    }

    private static Model2D LoadModelFromMX(Stream stream)
    {
        List<Action2DDef> actionDefs = Action2DDef.GetDefs();
        List<Direction2DDef> directionDefs = Direction2DDef.GetDefs();

        Model2D model = new Model2D();
        XDocument xDoc = XDocument.Load(stream);

        XElement xRoot = xDoc.Element("model");
        model.Id = Convert.ToInt32(xRoot.Attribute("id").Value);
        model.Caption = xRoot.Attribute("caption").Value;

        IEnumerable<XElement> xActions = xRoot.Elements("action");
        foreach (var xAction in xActions)
        {
            Action2D action = new Action2D();

            action.Id = Convert.ToInt32(xAction.Attribute("id").Value);

            foreach (var actionDef in actionDefs)
            {
                if (actionDef.Id == action.Id)
                {
                    action.Caption = actionDef.Caption;
                    break;
                }
            }

            IEnumerable<XElement> xDirections = xAction.Elements("direction");
            foreach (var xDirection in xDirections)
            {
                Direction2D direction = new Direction2D();
                direction.Id = Convert.ToInt32(xDirection.Attribute("id").Value);


                foreach (var directionDef in directionDefs)
                {
                    if (directionDef.Id == direction.Id)
                    {
                        direction.Caption = directionDef.Caption;
                        break;
                    }
                }

                IEnumerable<XElement> xFrames = xDirection.Elements("frame");
                foreach (var xFrame in xFrames)
                {
                    Frame2D frame = new Frame2D();
                    frame.Index = Convert.ToInt32(xFrame.Attribute("index").Value);
                    frame.Width = Convert.ToInt32(xFrame.Attribute("width").Value);
                    frame.Height = Convert.ToInt32(xFrame.Attribute("height").Value);
                    frame.OffsetX = Convert.ToInt32(xFrame.Attribute("offset-x").Value);
                    frame.offsetY = Convert.ToInt32(xFrame.Attribute("offset-y").Value);
                    
                    direction.Frames.Add(frame);
                }

                action.Directions.Add(direction);
            }

            model.Actions.Add(action);
        }

        return model;
    }

    public static byte[] GetFrameData(int modelId, int actionId, int directionId, int frameIndex)
    {
        byte[] data = null;

        string modelFile = string.Format("{0}models\\{1:d4}.m", DATUtility.GetResPath(), modelId);
        string frameFile = string.Format("/{0:d4}-{1:d4}-{2:d4}-{3:d4}", modelId, actionId, directionId, frameIndex);
        using (Package zip = System.IO.Packaging.Package.Open(modelFile, FileMode.Open))
        {
            foreach (PackagePart part in zip.GetParts())
            {
                if (part.Uri == new Uri(frameFile, UriKind.Relative))
                {
                    using (Stream inFileStream = part.GetStream())
                    {
                        data = new byte[inFileStream.Length];
                        inFileStream.Read(data, 0, data.Length);
                        break;
                    }
                }
            }
        }
        return data;
    }
}