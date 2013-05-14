using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

public static class DATUtility
{
    public static List<Model2D> s_models = new List<Model2D>();
    public static List<Unit2D> s_units = new List<Unit2D>();

    public static string GetResPath()
    {
        string datFile = string.Format("{0}\\data\\", AppDomain.CurrentDomain.BaseDirectory);
#if DEBUG
        datFile = string.Format("c:\\ag\\data\\");
#endif
        return datFile;
    }

    #region map
    public static List<int> GetMaps()
    {
        List<int> ids = new List<int>();
        string[] mapFiles =System.IO.Directory.GetFiles(string.Format("{0}maps",GetResPath()), "*.dat");
        foreach (var mapFile in mapFiles)
        {
            if (!mapFile.Contains(".bg"))
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(mapFile);
                ids.Add(Convert.ToInt32(fileInfo.Name.ToLower().Replace(".dat", string.Empty)));
            }
        }
        return ids;
    }
    public static bool SaveMap(Map2D map)
    {
        string datFile = string.Format("{0}maps\\{1}.dat", GetResPath(), map.ID);
        string datBGFile = string.Format("{0}maps\\{1}.bg.dat", GetResPath(), map.ID);
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
        xRoot.Add(new XAttribute("row", map.Row));
        xRoot.Add(new XAttribute("col", map.Col));

        XElement xCells = new XElement("cells");
        StringBuilder cellBuilder = new StringBuilder();
        for (int i = 0; i < map.Cells.Length; i++)
        {
            cellBuilder.AppendFormat("{0},", map.Cells[i].Value);
        }
        xCells.Value = cellBuilder.ToString();
        xRoot.Add(xCells);

        XElement xCamps = new XElement("camps");
        foreach (var camp in map.Camps)
        {
            XElement xCamp = new XElement("camp");
            xCamp.Add(new XAttribute("id", camp.Id));
            xCamp.Add(new XAttribute("caption", camp.Caption));
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
            xWidget.Add(new XAttribute("site-pos", string.Format("{0},{1}", widget.SitePos.Row, widget.SitePos.Col)));
            xWidgets.Add(xWidget);
        }
        xRoot.Add(xWidgets);

        xDoc.Add(xRoot);

        try
        {
            if (map.Background != null)
            {
                File.WriteAllBytes(datBGFile, map.Background);
            }
            xDoc.Save(datFile);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static Map2D GetMap(int mapId)
    {
        string datFile = string.Format("{0}maps\\{1}.dat",GetResPath(), mapId);
        string datBGFile = string.Format("{0}maps\\{1}.bg.dat", GetResPath(), mapId);
        string datBGMFile = string.Format("{0}maps\\{1}.bgm.dat", GetResPath(), mapId);
        if (!System.IO.File.Exists(datFile))
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(datFile);
            if (!System.IO.Directory.Exists(fileInfo.Directory.FullName))
            {
                System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
        }

        XDocument xDoc = XDocument.Load(datFile);
        
        XElement xMap = xDoc.Element("map");
        int row = Convert.ToInt32(xMap.Attribute("row").Value);
        int col = Convert.ToInt32(xMap.Attribute("col").Value);
        Map2D map = new Map2D();
        map.ID = mapId; // Convert.ToInt32(xMap.Attribute("id").Value);
        map.Row = row;
        map.Col = col;
        map.Cells = new MapCell[row * col];

        XElement xCells = xMap.Element("cells");
        string[] cellValues = xCells.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                MapCell cell = new MapCell();
                cell.MapPos = new MapPos(r, c);
                cell.Value = Convert.ToInt32(cellValues[r * col + c]);
                map.Cells[r * col + c] = cell;
            }
        }

        IEnumerable<XElement> xCamps = xMap.Element("camps").Elements("camp");
        foreach (var xCamp in xCamps)
        {
            int id = Convert.ToInt32(xCamp.Attribute("id").Value);
            string caption = xCamp.Attribute("caption").Value;
            string[] sitPosArr = xCamp.Attribute("start-pos").Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int pRow = Convert.ToInt32(sitPosArr[0]);
            int pCol = Convert.ToInt32(sitPosArr[1]);
            Camp camp = new Camp();
            camp.Id = id;
            camp.Caption = caption;
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
            Object2D obj = new Object2D();
            obj.ID = id;
            obj.Camp = map.GetCamp(campId);
            obj.SitePos = new MapPos(pRow, pCol);
            obj.CurrentPoint = obj.SitePos.Center;
            obj.Caption = caption;
            obj.SetUnit(GetUnit(unitId));

            obj.Camp.ObjList.Add(obj);
            obj.Map = map;
            map.Widgets.Add(obj);
        }

        if (File.Exists(datBGFile))
        {
            map.Background = File.ReadAllBytes(datBGFile);
        }

        if(File.Exists(datBGMFile))
        {
            map.BGM = File.ReadAllBytes(datBGMFile);
        }

        return map;
    }
    #endregion

    #region Unit
    public static bool SaveUnit(Unit2D unit)
    {
        string datFile = string.Format("{0}units.dat", GetResPath());
        XDocument xDoc = null;
        XElement xUnit = null;
        if (!System.IO.File.Exists(datFile))
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(datFile);
            if (!System.IO.Directory.Exists(fileInfo.Directory.FullName))
            {
                System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName);
            }

            xDoc = new XDocument();
            XElement xRoot = new XElement("units");
            xUnit = new XElement("unit");
            xRoot.Add(xUnit);
            xDoc.Add(xRoot);
        }
        else
        {
            xDoc = XDocument.Load(datFile);
            xUnit = xDoc.XPathSelectElement(string.Format("//units/unit[@id='{0}']", unit.Id));
            if (xUnit != null)
            {
                xUnit.RemoveAll();
            }
            else
            {
                xUnit = new XElement("unit");
                xDoc.Element("units").Add(xUnit);
            }
        }
        xUnit.Add(new XAttribute("id", unit.Id));
        xUnit.Add(new XAttribute("caption", unit.Caption));
        xUnit.Add(new XAttribute("category-id", unit.Category.Id));
        xUnit.Add(new XAttribute("stirps-id", unit.Stirps.Id));
        xUnit.Add(new XAttribute("model-id", unit.Model.Id));
        xUnit.Add(new XAttribute("icon-id", unit.IconModel.Id));
        xUnit.Add(new XAttribute("scale", unit.Scale));
        xUnit.Add(new XAttribute("max-hp", unit.MaxHP));
        xUnit.Add(new XAttribute("max-mp", unit.MaxMP));
        xUnit.Add(new XAttribute("ad", unit.AD));
        xUnit.Add(new XAttribute("addef", unit.ADDEF));
        xUnit.Add(new XAttribute("cost-m", unit.CostM));
        xUnit.Add(new XAttribute("cost-p", unit.CostP));
        xUnit.Add(new XAttribute("size", unit.Size));
        xUnit.Add(new XAttribute("crit-prob", unit.CritProbability));
        xUnit.Add(new XAttribute("def-prob", unit.DefProbability));
        xUnit.Add(new XAttribute("cd-build", unit.BuildCoolDown));

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

    public static List<int> GetUnits()
    {
        List<int> idList = new List<int>();

        string modelsDat = string.Format("{0}units.dat", GetResPath());
        if (System.IO.File.Exists(modelsDat))
        {
            XDocument xDoc = XDocument.Load(modelsDat);

            IEnumerable<XElement> xModels = xDoc.Element("units").Elements("unit");

            foreach (var xModel in xModels)
            {
                idList.Add(Convert.ToInt32(xModel.Attribute("id").Value));
            }

        }
        return idList;
    }

    public static List<int> GetUnits(int stirpsId, int categoryId)
    {
        List<int> idList = new List<int>();

        string modelsDat = string.Format("{0}units.dat", GetResPath());
        if (System.IO.File.Exists(modelsDat))
        {
            XDocument xDoc = XDocument.Load(modelsDat);

            IEnumerable<XElement> xModels = xDoc.XPathSelectElements(string.Format("//units/unit[@category-id='{0}'][@stirps-id='{1}']", categoryId, stirpsId));
            //= xDoc.Element("units").Elements("unit");

            foreach (var xModel in xModels)
            {
                idList.Add(Convert.ToInt32(xModel.Attribute("id").Value));
            }

        }
        return idList;
    }

    public static Unit2D GetUnit(int unitId)
    {
        foreach (var item in s_units)
        {
            if (item.Id == unitId)
            {
                return item;
            }
        }

        Unit2D unit = null;
        string datFile = string.Format("{0}units.dat", GetResPath());
        XDocument xDoc = XDocument.Load(datFile);

        XElement xUnit = xDoc.XPathSelectElement(string.Format("//units/unit[@id='{0}']", unitId));
        if (xUnit != null)
        {
            unit = new Unit2D();
            unit.Id = Convert.ToInt32(xUnit.Attribute("id").Value);
            unit.Caption = xUnit.Attribute("caption").Value;
            #region category
            if (xUnit.Attribute("category-id") == null)
            {
                unit.Category = UnitCategoryDef.Get(0);
            }
            else
            {
                unit.Category = UnitCategoryDef.Get(Convert.ToInt32(xUnit.Attribute("category-id").Value));
            }
            #endregion
            #region stirps
            if (xUnit.Attribute("stirps-id") == null)
            {
                unit.Stirps = UnitStirps.Get(0);
            }
            else
            {
                unit.Stirps = UnitStirps.Get(Convert.ToInt32(xUnit.Attribute("stirps-id").Value));
            }
            #endregion
            unit.Model = DATUtility.GetModel(Convert.ToInt32(xUnit.Attribute("model-id").Value));
            #region icon model
            if (xUnit.Attribute("icon-id") == null)
            {
                unit.IconModel= DATUtility.GetModel(102);
            }
            else
            {
                unit.IconModel = DATUtility.GetModel(Convert.ToInt32(xUnit.Attribute("icon-id").Value));
            }
            #endregion
            #region scale
            if (xUnit.Attribute("scale") == null)
            {
                unit.Scale = 1.0f;
            }
            else
            {
                unit.Scale = Convert.ToSingle(xUnit.Attribute("scale").Value);
            }
            #endregion
            #region max-hp
            if (xUnit.Attribute("max-hp") == null)
            {
                unit.MaxHP = 100;
            }
            else
            {
                unit.MaxHP = Convert.ToInt32(xUnit.Attribute("max-hp").Value);
            }
            #endregion
            #region max-mp
            if (xUnit.Attribute("max-mp") == null)
            {
                unit.MaxMP = 0;
            }
            else
            {
                unit.MaxMP = Convert.ToInt32(xUnit.Attribute("max-mp").Value);
            }
            #endregion
            #region ad
            if (xUnit.Attribute("ad") == null)
            {
                unit.AD = 10;
            }
            else
            {
                unit.AD = Convert.ToInt32(xUnit.Attribute("ad").Value);
            }
            #endregion
            #region addef
            if (xUnit.Attribute("addef") == null)
            {
                unit.ADDEF = 0;
            }
            else
            {
                unit.ADDEF = Convert.ToInt32(xUnit.Attribute("addef").Value);
            }
            #endregion
            #region cost-m
            if (xUnit.Attribute("cost-m") == null)
            {
                unit.CostM = 10;
            }
            else
            {
                unit.CostM = Convert.ToInt32(xUnit.Attribute("cost-m").Value);
            }
            #endregion
            #region cost-p
            if (xUnit.Attribute("cost-p") == null)
            {
                unit.CostP = 1;
            }
            else
            {
                unit.CostP = Convert.ToInt32(xUnit.Attribute("cost-p").Value);
            }
            #endregion
            #region size
            if (xUnit.Attribute("size") == null)
            {
                unit.Size = 16;
            }
            else
            {
                unit.Size = Convert.ToInt32(xUnit.Attribute("size").Value);
            }
            #endregion
            #region crit-prob
            if (xUnit.Attribute("crit-prob") == null)
            {
                unit.CritProbability = 0;
            }
            else
            {
                unit.CritProbability = Convert.ToInt32(xUnit.Attribute("crit-prob").Value);
            }
            #endregion
            #region def-prob
            if (xUnit.Attribute("def-prob") == null)
            {
                unit.DefProbability = 0;
            }
            else
            {
                unit.DefProbability = Convert.ToInt32(xUnit.Attribute("def-prob").Value);
            }
            #endregion
            #region cd-build
            if (xUnit.Attribute("cd-build") == null)
            {
                unit.BuildCoolDown = 0;
            }
            else
            {
                unit.BuildCoolDown = Convert.ToInt32(xUnit.Attribute("cd-build").Value);
            }
            #endregion

            s_units.Add(unit);
        }

        return unit;
    }
    #endregion

    #region Model

    public static Model2D GetModel(int modelId, List<Model2D> models)
    {
        foreach (var item in models)
        {
            if (item.Id == modelId)
            {
                return item;
            }
        }

        Model2D model = GetModel(modelId);
        models.Add(model);
        return model;
    }

    public static Model2D GetModel(int modelId)
    {
        foreach (var item in s_models)
        {
            if (item.Id == modelId)
            {
                return item;
            }
        }

        string modelFile = string.Format("{0}models.dat", GetResPath());

        XDocument xDoc = XDocument.Load(modelFile);

        XElement xModel = xDoc.XPathSelectElement(string.Format("//models/model[@id='{0}']", modelId));
        if (xModel != null)
        {
            Model2D model = ModelXML.FromXML(xModel);
            foreach (var action in model.Actions)
            {
                foreach (var direction in action.Directions)
                {
                    foreach (var frame in direction.Frames)
                    {
                        frame.Data = ResourceLoader.GetFrameData(model.Id, action.Id, direction.Id, frame.Index);
                    }
                }
            }

            s_models.Add(model);

            return model;
        }
        return null;
    }

    public static List<Model2D> GetModels()
    {
        List<Model2D> list = new List<Model2D>();

        string modelsDat = string.Format("{0}models.dat", GetResPath());
        if (System.IO.File.Exists(modelsDat))
        {
            XDocument xDoc = XDocument.Load(modelsDat);

            IEnumerable<XElement> xModels = xDoc.Element("models").Elements("model");

            foreach (var xModel in xModels)
            {
                list.Add(ModelXML.FromXML(xModel));
            }

        }
        return list;
    }

    public static List<Model2D> GetModels(int categoryId)
    {
        List<Model2D> list = new List<Model2D>();

        string modelsDat = string.Format("{0}models.dat", GetResPath());
        if (System.IO.File.Exists(modelsDat))
        {
            XDocument xDoc = XDocument.Load(modelsDat);

            IEnumerable<XElement> xModels = xDoc.XPathSelectElements(string.Format("//model[@category-id='{0}']", categoryId));//xDoc.Element("models").Elements("model");

            foreach (var xModel in xModels)
            {
                list.Add(ModelXML.FromXML(xModel));
            }

        }
        return list;
    }

    public static bool SaveModel(Model2D model)
    {
        string modelsDat = string.Format("{0}models.dat", GetResPath());
        if (!System.IO.File.Exists(modelsDat))
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(modelsDat);
            if (!System.IO.Directory.Exists(fileInfo.Directory.FullName))
            {
                System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName);
            }

            XDocument xDoc = new XDocument();

            XElement xRoot = new XElement("models");

            XElement xModel = new XElement("model");
            xModel.Add(new XAttribute("id", model.Id));
            xModel.Add(new XAttribute("caption", model.Caption));
            xModel.Add(new XAttribute("category-id", model.Category.Id));
            #region add actions
            foreach (var action in model.Actions)
            {
                XElement xAction = new XElement("action");
                xAction.Add(new XAttribute("id", action.Id));

                foreach (var direction in action.Directions)
                {
                    XElement xDirection = new XElement("direction");
                    xDirection.Add(new XAttribute("id", direction.Id));

                    foreach (var frame in direction.Frames)
                    {
                        XElement xFrame = new XElement("frame");
                        xFrame.Add(new XAttribute("index", frame.Index));
                        xFrame.Add(new XAttribute("width", frame.Width));
                        xFrame.Add(new XAttribute("height", frame.Height));
                        xFrame.Add(new XAttribute("offset-x", frame.OffsetX));
                        xFrame.Add(new XAttribute("offset-y", frame.offsetY));

                        xDirection.Add(xFrame);
                    }

                    xAction.Add(xDirection);
                }

                xModel.Add(xAction);
            }
            #endregion
            xRoot.Add(xModel);

            xDoc.Add(xRoot);

            try
            {
                xDoc.Save(modelsDat);
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            XDocument xDoc = XDocument.Load(modelsDat);
            XElement xRoot = xDoc.Element("models");

            XElement xModel = xDoc.XPathSelectElement(string.Format("//models/model[@id='{0}']", model.Id));
            if (xModel != null)
            {
                xModel.RemoveAll();
            }
            else
            {
                xModel = new XElement("model");
                xRoot.Add(xModel);
            }

            xModel.Add(new XAttribute("id", model.Id));
            xModel.Add(new XAttribute("caption", model.Caption));
            xModel.Add(new XAttribute("category-id", model.Category.Id));
            #region add actions
            foreach (var action in model.Actions)
            {
                XElement xAction = new XElement("action");
                xAction.Add(new XAttribute("id", action.Id));

                foreach (var direction in action.Directions)
                {
                    XElement xDirection = new XElement("direction");
                    xDirection.Add(new XAttribute("id", direction.Id));

                    foreach (var frame in direction.Frames)
                    {
                        XElement xFrame = new XElement("frame");
                        xFrame.Add(new XAttribute("index", frame.Index));
                        xFrame.Add(new XAttribute("width", frame.Width));
                        xFrame.Add(new XAttribute("height", frame.Height));
                        xFrame.Add(new XAttribute("offset-x", frame.OffsetX));
                        xFrame.Add(new XAttribute("offset-y", frame.offsetY));

                        xDirection.Add(xFrame);
                    }

                    xAction.Add(xDirection);
                }

                xModel.Add(xAction);
            }
            #endregion

            try
            {
                xDoc.Save(modelsDat);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    #endregion
}