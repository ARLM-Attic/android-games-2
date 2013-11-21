using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;
using System.Xml.Linq;
using AG.Editor.Core.Data;

namespace AG.Editor.Core.V1_0
{
    public class AGTProjectXML1_0
    {
        public static AGTProjectSummary ConvertTProjectSummaryFromXml(XElement xEl)
        {
            string ver = xEl.Attribute("ver").Value;
            string name = xEl.Attribute("n").Value;
            string caption = xEl.Attribute("c").Value;
            AGTProjectSummary summary = new AGTProjectSummary();
            summary.Name = name;
            summary.Caption = caption;
            summary.Version = ver;
            return summary;
        }

        public static AGTProject ConvertTProjectFromXml(XElement xEl)
        {
            string ver = xEl.Attribute("ver").Value;
            string name = xEl.Attribute("n").Value;
            string caption = xEl.Attribute("c").Value;
            AGTProject tProject = new AGTProject();
            tProject.Name = name;
            tProject.Caption = caption;
            tProject.Version = ver;

            List<XElement> xAcList = xEl.Element("ac").Elements("aci").ToList();
            foreach (XElement xAci in xAcList)
            {
                tProject.AudioCateogries.Add(ConvertAudioCategoryFromXml(xAci));
            }

            List<XElement> xMciLst = xEl.Element("mc").Elements("mci").ToList();
            foreach (XElement xMci in xMciLst)
            {
                tProject.ModelCategories.Add(ConvertModelCategoryFromXml(xMci));
            }

            return tProject;
        }

        #region audio category
        private static AGAudioCategory ConvertAudioCategoryFromXml(XElement xAci)
        {
            AGAudioCategory category = new AGAudioCategory();
            category.Id = xAci.XGetAttrIntValue("i", AGECONST.INT_NULL);
            category.Caption = xAci.XGetAttrStringValue("c", AGECONST.STRING_NULL);
            return category;
        }
        #endregion

        #region model category
        private static AGModelCategory ConvertModelCategoryFromXml(XElement xMci)
        {
            string id = xMci.Attribute("i").Value;
            string caption = xMci.Attribute("c").Value;
            int dir = Convert.ToInt32(xMci.Attribute("dir").Value);

            List<XElement> xActs = xMci.Elements("act").ToList();

            AGModelCategory category = new AGModelCategory();
            category.Id = Convert.ToInt32(id);
            category.Caption = caption;
            if(dir == 2)
            {
                category.DirectionMode = AGDirectionMode.Two;
            }
            else if (dir == 4)
            {
                category.DirectionMode = AGDirectionMode.Four;
            }
            else if (dir == 8)
            {
                category.DirectionMode = AGDirectionMode.Eight;
            }
            else if (dir == 16)
            {
                category.DirectionMode = AGDirectionMode.Sixteen;
            }

            foreach (var xAct in xActs)
            {
                int actId = Convert.ToInt32(xAct.Attribute("i").Value);
                string actCaption = xAct.Attribute("c").Value;

                AGAction action = new AGAction(actId);
                action.Caption = actCaption;

                for (int dirIndex = 0; dirIndex < dir; dirIndex++)
			    {
                    AGDirection direction = new AGDirection(dirIndex);
                    direction.Caption = AGDirection.GetCaption(category.DirectionMode, direction.Id);
                    action.Directions.Add(direction);
                }

                category.Actions.Add(action);
            }

            return category;
        }
        #endregion
    }
}
