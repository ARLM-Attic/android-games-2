using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Skill
{
    public int ColdDown { get; set; }

    public bool IsColdDown { get; private set; }

    public Skill()
    {
        //ColdDown = 4 * 30;
    }

    public abstract bool Check(IEngine engine, Object2D obj);

    public void Cast(Object2D obj)
    {
        OnCast(obj);
        ColdDown = 1 * 30;
    }

    public abstract void OnCast(Object2D obj);

    public void Loop()
    {
        ColdDown--;
        if (ColdDown <= 0)
        {
            ColdDown = 0;
            
            IsColdDown = true;
        }
        else
        {
            IsColdDown = false;
        }
    }

}
