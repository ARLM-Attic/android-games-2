using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGWeb
{
    public class ServerData
    {
        private static object s_lock = new object();
        private static ServerData s_instance;

        public static ServerData Instance
        {
            get
            {
                if (s_instance == null)
                {
                    lock (s_lock)
                    {
                        if (s_instance == null)
                        {
                            s_instance = new ServerData();
                        }
                    }
                }
                return s_instance;
            }
        }

        public Map Map { get; set; }

        public ServerData()
        {
            Map = new Map();
            Map.Row = 1000;
            Map.Col = 1000;

            Map.Cells = new int[Map.Row * Map.Col];

            for (int r = 0; r < Map.Row; r++)
            {
                for (int c = 0; c < Map.Col; c++)
                {
                    if (r % 3 == 0)
                    {
                        Map.Cells[r * Map.Col + c] = 1;
                    }
                    else
                    {
                        Map.Cells[r * Map.Col + c] = 0;
                    }
                }
            }
        }
    }
}
