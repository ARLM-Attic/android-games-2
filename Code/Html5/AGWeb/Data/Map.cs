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

        public MapRange GetRange(int row, int col, int radius)
        {
            int startRow = row - radius;
            int startCol = col - radius;
            if (startRow < 0)
            {
                startRow = 0;
            }
            else if (startRow + radius * 2 >= Row)
            {
                startRow = Row - radius * 2 - 1;
            }

            if (startCol < 0)
            {
                startCol = 0;
            }
            else if (startCol + radius * 2 >= Col)
            {
                startCol = Col - radius * 2 - 1;
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
                    range.Cells[r * range.Col + c] = this.Cells[(startRow + r) * Col + (startCol + c)];
                }
            }
            return range;
        }
    }
}
