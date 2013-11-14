using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AG.Editor.Core.Stores
{
    public static class XDocumentExtension
    {
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
    }
}
