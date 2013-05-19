using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

    public void Loop(IEngine engine, MouseMessage mouse)
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

        OnUpdate(engine, mouse);
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

    public virtual void RenderOnIcon(IGDI gdi, Rectangle rect)
    {
    }

    protected virtual void OnUpdate(IEngine engine, MouseMessage mouse)
    {
    }
}
