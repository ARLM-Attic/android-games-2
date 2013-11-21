using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AG.Editor.Core
{
    public class AGECONST
    {
        public const int INT_NULL = -1;
        public const string STRING_NULL = "unknown";
    }

    public static class XDocumentExtension
    {
        public static XElement XGetElement(this XElement xEl, string name)
        {
            if (xEl == null)
            {
                return null;
            }
            return xEl.Element(name);
        }

        public static IEnumerable<XElement> XGetElements(this XElement xEl, string name)
        {
            if (xEl == null)
            {
                return new List<XElement>();
            }
            return xEl.Elements(name);
        }

        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <param name="xAttr"></param>
        /// <param name="defaultString"></param>
        /// <returns></returns>
        public static string XGetValueString(this XAttribute xAttr, string defaultString)
        {
            if (xAttr == null)
            {
                return defaultString;
            }
            return xAttr.Value;
        }

        public static int XGetAttrInt(this XElement xEl, string attrName)
        {
            return XGetAttrIntValue(xEl, attrName, AGECONST.INT_NULL);
        }

        public static int XGetAttrIntValue(this XElement xEl, string attrName, int defaultValue)
        {
            if (xEl == null)
            {
                return defaultValue;
            }

            XAttribute xAttr = xEl.Attribute(attrName);
            if (xAttr == null)
            {
                return defaultValue;
            }

            int value = 0;
            if (int.TryParse(xAttr.Value, out value))
            {
                return value;
            }
            return defaultValue;
        }

        public static string XGetAttrString(this XElement xEl, string attrName)
        {
            return XGetAttrStringValue(xEl, attrName, AGECONST.STRING_NULL);
        }

        public static string XGetAttrStringValue(this XElement xEl, string attrName, string defaultValue)
        {
            if (xEl == null)
            {
                return defaultValue;
            }

            XAttribute xAttr = xEl.Attribute(attrName);
            if (xAttr == null)
            {
                return defaultValue;
            }
            return xAttr.Value;
        }

        public static Guid XGetAttrGuid(this XElement xEl, string attrName)
        {
            return XGetAttrGuid(xEl, attrName, Guid.Empty);
        }

        public static Guid XGetAttrGuid(this XElement xEl, string attrName, Guid defaultValue)
        {
            if (xEl == null)
            {
                return defaultValue;
            }

            try
            {
                XAttribute xAttr = xEl.Attribute(attrName);
                if (xAttr != null)
                {
                    Guid guid = new Guid(xEl.Attribute(attrName).Value);
                    return guid;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }
    }
}
