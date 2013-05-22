using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameResult
{
    public int MapId { get; set; }
    public long GameTime { get; set; }
    public int KilledCount { get; set; }
    public int BuildCount { get; set; }
    public int DeadCount { get; set; }

    public bool IsVictory { get; set; }
}