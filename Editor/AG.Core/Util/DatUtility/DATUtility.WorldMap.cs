using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

public static partial class DATUtility
{
    public static List<int> GetWorldMaps()
    {
        List<int> ids = new List<int>();
        string[] mapFiles = System.IO.Directory.GetFiles(string.Format("{0}world-maps", GetResPath()), "*.xml");
        foreach (var mapFile in mapFiles)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(mapFile);
            ids.Add(Convert.ToInt32(fileInfo.Name.ToLower().Replace(".xml", string.Empty)));
        }
        return ids;
    }

    public static bool SaveWorldMap(WorldMap map)
    {
        string datFile = string.Format("{0}world-maps\\{1}.xml", GetResPath(), map.Id);
        if (!System.IO.File.Exists(datFile))
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(datFile);
            if (!System.IO.Directory.Exists(fileInfo.Directory.FullName))
            {
                System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
        }

        XDocument xDoc = new XDocument();

        XElement xRoot = new XElement("world-map");

        xRoot.Add(new XAttribute("id", map.Id));
        xRoot.Add(new XAttribute("caption", map.Caption));
        xRoot.Add(new XAttribute("model-id", map.Model.Id));

        XElement xStages = new XElement("stages");
        foreach (var stage in map.StagesPosList)
        {
            XElement xStage = new XElement("stage");
            xStage.Add(new XAttribute("map-id", stage.MapId));
            xStage.Add(new XAttribute("pos", string.Format("{0},{1}", stage.Pos.X, stage.Pos.Y)));
            xStages.Add(xStage);
        }
        xRoot.Add(xStages);

        xDoc.Add(xRoot);

        try
        {
            xDoc.Save(datFile);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static WorldMap GetWorldMap(int id)
    {
        string datFile = string.Format("{0}world-maps\\{1}.xml", GetResPath(), id);
        if (!System.IO.File.Exists(datFile))
        {
            return null;
        }

        XDocument xDoc = XDocument.Load(datFile);

        XElement xMap = xDoc.Element("world-map");
        int worldMapId = Convert.ToInt32(xMap.Attribute("id").Value);
        string caption = xMap.Attribute("caption").Value;
        int modelId = Convert.ToInt32(xMap.Attribute("model-id").Value);

        WorldMap map = new WorldMap();
        map.Id = worldMapId;
        map.Caption = caption;
        map.Model = DATUtility.GetModel(modelId);

        IEnumerable<XElement> xStages = xMap.Element("stages").Elements("stage");
        foreach (var xStage in xStages)
        {
            string[] posValues = xStage.Attribute("pos").Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int mapId = Convert.ToInt32(xStage.Attribute("map-id").Value);
            int posX = Convert.ToInt32(posValues[0]);
            int posY = Convert.ToInt32(posValues[1]);

            StagesPos stagePos = new StagesPos();
            stagePos.MapId = mapId;
            stagePos.Pos = new Point2D(posX, posY);
            map.StagesPosList.Add(stagePos);
        }
        return map;
    }
}
