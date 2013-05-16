using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IAttackStrategy
{
    bool Attack(Map2D map, Object2D obj);
}