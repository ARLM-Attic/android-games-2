using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IEngine
{
    IADI ADI { get; }
    IIDI IDI { get; }
    Map2D CurrentMap { get; set; }
    void SwitchSence(Sence sence);
    void LoadMap(int mapId);
}