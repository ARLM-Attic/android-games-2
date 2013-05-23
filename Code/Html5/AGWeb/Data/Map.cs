using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGWeb
{
    public class Map
    {
        public int Id { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }

        public int[] Cells { get; set; }

        public static MapRange GetRange(Map2D map, int row, int col, int radius)
        {
            if (radius * 2 > map.Row)
            {
                radius = map.Row / 2;
            }

            if (radius * 2 > map.Col)
            {
                radius = map.Col / 2;
            }

            int startRow = row - radius;
            int startCol = col - radius;
            if (startRow < 0)
            {
                startRow = 0;
            }
            else if (startRow + radius * 2 >= map.Row)
            {
                startRow = map.Row - radius * 2 - 1;
            }

            if (startCol < 0)
            {
                startCol = 0;
            }
            else if (startCol + radius * 2 >= map.Col)
            {
                startCol = map.Col - radius * 2 - 1;
            }

            MapRange range = new MapRange();
            range.StartRow = startRow;
            range.StartCol = startCol;
            range.Row = radius * 2;
            range.Col = radius * 2;
            range.Cells = new int[range.Row * range.Col];
            for (int r = 0; r < range.Row; r++)
            {
                for (int c = 0; c < range.Col; c++)
                {
                    range.Cells[r * range.Col + c] = map.Cells[(startRow + r) * map.Col + (startCol + c)].Type;
                }
            }

            #region 获取区域内的对象
            for (int index = 0; index < map.Widgets.Count; index++)
            {
                Object2D obj = map.Widgets[index];

                if (obj.SitePos.Row >= range.StartRow && obj.SitePos.Col >= range.StartCol
                    && obj.SitePos.Row < range.StartRow + range.Row
                    && obj.SitePos.Col < range.StartCol + range.Col)
                {
                    range.Objs.Add(obj);
                }
            }
            #endregion

            return range;
        }
    }
}
