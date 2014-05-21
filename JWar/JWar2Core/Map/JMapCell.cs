using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Core
{
    public class JMapCell
    {

        #region 属性
        public int Type { get; set; }
        /// <summary>
        /// 地形编号
        /// </summary>
        public int TerrainId { get; set; }
        public int TerrainDir { get; set; }
        /// <summary>
        /// 该地形中具体的切片编号
        /// </summary>
        public int TerrainIndex { get; set; }
        #endregion

        public JMapPos Pos { get; set; }

        public JMapCell(JMapPos pos, int terrainId)
        {
            Pos = pos;
        }

    }
}