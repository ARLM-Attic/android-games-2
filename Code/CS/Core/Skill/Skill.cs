﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Skill
{
    public int CoolDownTime { get; set; }
    public int CoolDownConter { get; set; }

    public bool IsCoolDown { get; private set; }

    public Model2D IconModel { get; private set; }

    public bool IsRepare { get; set; }

    public Skill(Model2D iconModel, int cdTime)
    {
        IconModel = iconModel;
        CoolDownTime = cdTime;// 1 * 30;
    }

    public abstract bool Check(IEngine engine, Object2D obj);

    public void Cast(Object2D obj)
    {
        OnCast(obj);
        CoolDownConter = CoolDownTime;
    }

    /// <summary>
    /// 让技能进入cd状态
    /// </summary>
    public void CoolDowning()
    {
        CoolDownConter = CoolDownTime;
    }

    public abstract void OnCast(Object2D obj);

    public void Loop(IEngine engine)
    {
        CoolDownConter--;
        if (CoolDownConter <= 0)
        {
            CoolDownConter = 0;
            
            IsCoolDown = true;
        }
        else
        {
            IsCoolDown = false;
        }

        OnUpdate(engine);
    }

    public void Begin()
    {
        if (IsRepare)
        {
            IsRepare = false;
        }
        else
        {
            IsRepare = true;
        }
    }

    public virtual void Render(IEngine engine)
    {
    }

    protected virtual void OnUpdate(IEngine engine)
    {
    }
}
