using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

public class ModelXML
{
    public static Model2D FromXML(XElement xModel)
    {
        List<Action2DDef> actionDefs = Action2DDef.GetDefs();
        List<Direction2DDef> directionDefs = Direction2DDef.GetDefs();
        Model2D model = new Model2D();

        model = new Model2D();
        model.Id = Convert.ToInt32(xModel.Attribute("id").Value);
        model.Caption = xModel.Attribute("caption").Value;
        #region category
        if (string.IsNullOrEmpty(xModel.Attribute("category-id").Value))
        {
            model.Category = ModelCategory.Get(1);
        }
        else
        {
            model.Category = ModelCategory.Get(Convert.ToInt32(xModel.Attribute("category-id").Value));
        }
        #endregion

        IEnumerable<XElement> xActions = xModel.Elements("action");
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
}

