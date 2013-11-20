using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

public static partial class DATUtility
{
    #region map
    public static List<MapInfo> GetMaps()
    {
        List<MapInfo> ids = new List<MapInfo>();
        string[] mapFiles = System.IO.Directory.GetFiles(string.Format("{0}maps", GetResPath()), "*.xml");
        foreach (var mapFile in mapFiles)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(mapFile);

            XDocument xDoc = XDocument.Load(fileInfo.FullName);

            XElement xMap = xDoc.Element("map");
            int id = Convert.ToInt32(xMap.Attribute("id").Value);
            string mapCaption = xMap.Attribute("caption").Value;
            //int row = Convert.ToInt32(xMap.Attribute("row").Type);
            //int col = Convert.ToInt32(xMap.Attribute("col").Type);
            //int bgModelId = Convert.ToInt32(xMap.Attribute("bg-model-id").Type);

            MapInfo mapInfo = new MapInfo();
            mapInfo.Id = id;
            mapInfo.Caption = mapCaption;
            ids.Add(mapInfo);
        }
        return ids;
    }

    public static MapInfo GetMapInfo(int mapId)
    {
        string datFile = string.Format("{0}maps\\{1}.xml", GetResPath(), mapId);
        if (!System.IO.File.Exists(datFile))
        {
            return null;
        }

        XDocument xDoc = XDocument.Load(datFile);

        XElement xMap = xDoc.Element("map");
        string mapCaption = xMap.Attribute("caption").Value;
        int id = Convert.ToInt32(xMap.Attribute("id").Value);

        MapInfo info = new MapInfo();
        info.Id = id;
        info.Caption = mapCaption;
        return info;
    }

    public static bool SaveMap(Map2D map)
    {
        string datFile = string.Format("{0}maps\\{1}.xml", GetResPath(), map.ID);
        if (!System.IO.File.Exists(datFile))
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(datFile);
            if (!System.IO.Directory.Exists(fileInfo.Directory.FullName))
            {
                System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
        }

        XDocument xDoc = new XDocument();

        XElement xRoot = new XElement("map");

        xRoot.Add(new XAttribute("id", map.ID));
        xRoot.Add(new XAttribute("caption", map.Caption));
        xRoot.Add(new XAttribute("row", map.Row));
        xRoot.Add(new XAttribute("col", map.Col));
        #region background
        if (map.Background != null)
        {
            xRoot.Add(new XAttribute("bg-model-id", map.Background.Id));
        }
        else
        {
            xRoot.Add(new XAttribute("bg-model-id", 0));
        }
        #endregion

        XElement xCells = new XElement("cells");
        StringBuilder cellBuilder = new StringBuilder();
        for (int i = 0; i < map.Cells.Length; i++)
        {
            cellBuilder.AppendFormat("{0}|{1}|{2},",
                map.Cells[i].Type,
                map.Cells[i].TerrainId,
                map.Cells[i].TerrainIndex);
        }
        xCells.Value = cellBuilder.ToString();
        xRoot.Add(xCells);

        XElement xCamps = new XElement("camps");
        foreach (var camp in map.Camps)
        {
            XElement xCamp = new XElement("camp");
            xCamp.Add(new XAttribute("id", camp.Id));
            xCamp.Add(new XAttribute("caption", camp.Caption));
            xCamp.Add(new XAttribute("type-id", camp.Type.Id));
            xCamp.Add(new XAttribute("start-pos", string.Format("{0},{1}", camp.StartPos.Row, camp.StartPos.Col)));
            xCamps.Add(xCamp);
        }
        xRoot.Add(xCamps);

        XElement xWidgets = new XElement("objs");
        foreach (var widget in map.Widgets)
        {
            XElement xWidget = new XElement("obj");
            xWidget.Add(new XAttribute("id", widget.ID));
            xWidget.Add(new XAttribute("camp-id", widget.Camp.Id));
            xWidget.Add(new XAttribute("caption", widget.Caption));
            xWidget.Add(new XAttribute("unit-id", widget.Unit.Id));
            xWidget.Add(new XAttribute("site-pt", string.Format("{0},{1}", widget.CurrentPoint.X, widget.CurrentPoint.Y)));
            xWidget.Add(new XAttribute("site-pos", string.Format("{0},{1}", widget.SitePos.Row, widget.SitePos.Col)));
            xWidgets.Add(xWidget);
        }
        xRoot.Add(xWidgets);

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

    private static List<Map2D> s_maps = new List<Map2D>();

    public static Map2D GetMap(int mapId)
    {
        for (int index = 0; index < s_maps.Count; index++)
        {
            if (s_maps[index].ID == mapId)
            {
                return s_maps[index];
            }
        }

        string datFile = string.Format("{0}maps\\{1}.xml", GetResPath(), mapId);
        if (!System.IO.File.Exists(datFile))
        {
            return null;
        }

        XDocument xDoc = XDocument.Load(datFile);

        XElement xMap = xDoc.Element("map");
        string mapCaption = xMap.Attribute("caption").Value;
        int row = Convert.ToInt32(xMap.Attribute("row").Value);
        int col = Convert.ToInt32(xMap.Attribute("col").Value);
        int bgModelId = Convert.ToInt32(xMap.Attribute("bg-model-id").Value);

        Map2D map = new Map2D();
        map.ID = mapId; // Convert.ToInt32(xMap.Attribute("id").Type);
        map.Caption = mapCaption;
        map.Row = row;
        map.Col = col;
        if (bgModelId != 0)
        {
            map.Background = DATUtility.GetModel(bgModelId);
        }
        map.Cells = new MapCell[row * col];

        XElement xCells = xMap.Element("cells");
        string[] cellValues = xCells.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                MapCell cell = new MapCell();
                cell.MapPos = new MapPos(r, c);
                #region 解析单元的属性
                string[] cellValueArr = cellValues[r * col + c].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (cellValueArr.Length == 3)
                {
                    cell.Type = Convert.ToInt32(cellValueArr[0]);
                    cell.TerrainId = Convert.ToInt32(cellValueArr[1]);
                    cell.TerrainIndex = Convert.ToInt32(cellValueArr[2]);
                }
                else
                {
                    cell.Type = Convert.ToInt32(cellValues[r * col + c]);
                    cell.TerrainId = DATUtility.GetTerrain(1).Id;
                    cell.TerrainIndex = 1;
                }
                #endregion
                map.Cells[r * col + c] = cell;
            }
        }

        IEnumerable<XElement> xCamps = xMap.Element("camps").Elements("camp");
        foreach (var xCamp in xCamps)
        {
            int id = Convert.ToInt32(xCamp.Attribute("id").Value);
            string caption = xCamp.Attribute("caption").Value;
            int typeId = Convert.ToInt32(xCamp.Attribute("type-id").Value);
            string[] sitPosArr = xCamp.Attribute("start-pos").Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int pRow = Convert.ToInt32(sitPosArr[0]);
            int pCol = Convert.ToInt32(sitPosArr[1]);
            Camp camp = new Camp();
            camp.Id = id;
            camp.Caption = caption;
            camp.Type = CampType.Get(typeId);
            camp.StartPos = new MapPos(pRow, pCol);

            map.Camps.Add(camp);
        }

        IEnumerable<XElement> xObjects = xMap.Element("objs").Elements("obj");
        foreach (var xObj in xObjects)
        {
            int id = Convert.ToInt32(xObj.Attribute("id").Value);
            int campId = Convert.ToInt32(xObj.Attribute("camp-id").Value);
            string caption = xObj.Attribute("caption").Value;
            int unitId = Convert.ToInt32(xObj.Attribute("unit-id").Value);
            string[] sitPosArr = xObj.Attribute("site-pos").Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int pRow = Convert.ToInt32(sitPosArr[0]);
            int pCol = Convert.ToInt32(sitPosArr[1]);

            string[] sitePtArr = xObj.Attribute("site-pt").Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            float pX = Convert.ToSingle(sitePtArr[0]);
            float pY = Convert.ToSingle(sitePtArr[1]);

            Object2D obj = new Object2D();
            obj.ID = id;
            obj.Camp = map.GetCamp(campId);
            obj.SitePos = new MapPos(pRow, pCol);
            obj.CurrentPoint = new Point2D(pX, pY);
            obj.Caption = caption;
            obj.SetUnit(GetUnit(unitId));

            obj.Camp.ObjList.Add(obj);
            obj.Map = map;
            map.Widgets.Add(obj);
        }

        s_maps.Add(map);
        return map;
    }
    #endregion
}
