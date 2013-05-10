using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Object2D
{
    public int ID { get; set; }

    public string Caption { get; set; }

    public Camp Camp { get; set; }
    public Map2D Map { get; internal set; }

    public Unit2D Unit { get; private set; }

    public MapPos SitePos { get; set; }
    public MapPos TargetPos { get; set; }
    public Point2D CurrentPoint { get; set; }

    public int FrameIndex { get; internal set; }
    public int ActionId { get; internal set; }
    public int DirectionId { get; internal set; }

    private int _counter;
    private int _updateCounter = 3;

    #region 属性
    public long HP { get; set; }
    public int AD { get; set; }
    public int ADDEF { get; set; }
    #endregion

    public int DefProbability { get; set; }

    public List<Skill> Skills { get; set; }

    public int DeadTime { get; set; }
    private bool _isDead = false;

    public int DefenceCount { get; set; }
    public int DefenceCounter { get; set; }

    /// <summary>
    /// 目标单位对象
    /// </summary>
    public Object2D TargetObj { get; set; }
    public ObjState State { get; set; }

    private IMoveStrategy MoveStrategy { get; set; }

    public Object2D()
    {
        ActionId = Action2DDef.Stand.Id;
        DirectionId = Direction2DDef.South.Id;
        FrameIndex = 0x01;
    }

    public Object2D(int actionId, int directionId)
    {
        ActionId = actionId;
        DirectionId = directionId;
        FrameIndex = 0x01;
    }

    public void SetUnit(Unit2D unit)
    {
        Unit = unit;
        HP = Unit.MaxHP;
        AD = Unit.AD;
        ADDEF = Unit.ADDEF;

        DefenceCount = 4;
        DefenceCounter = 0;

        Skills = new List<Skill>();
        Skills.Add(new DefSkill());
        Skills.Add(new CritAttackSkill());
        Skills.Add(new AttackSkill());

        MoveStrategy = new NCDMoveStrategy();
    }

    public void SetAction(ObjState state)
    {
        if (State != state)
        {
            if (state == ObjState.Attack)
            {
                State = state;
                ActionId = Action2DDef.Attack.Id;
                FrameIndex = 0x01;
            }
            else if (state == ObjState.Die)
            {
                //if (FrameIndex >= Unit.Model.GetFrames(ActionId, DirectionId).Count)
                //{
                    State = state;
                    ActionId = Action2DDef.Die.Id;
                    FrameIndex = 0x01;
                //}
            }
            else if (state == ObjState.Move)
            {
                if (FrameIndex >= Unit.Model.GetFrames(ActionId, DirectionId).Count)
                {
                    State = state;
                    ActionId = Action2DDef.Move.Id;
                    FrameIndex = 0x01;
                }
            }
            else if (state == ObjState.Stand)
            {
                if (FrameIndex >= Unit.Model.GetFrames(ActionId, DirectionId).Count)
                {
                    State = state;
                    ActionId = Action2DDef.Stand.Id;
                    FrameIndex = 0x01;
                }
            }
            else if (state == ObjState.Def)
            {
                //if (FrameIndex >= Unit.Model.GetFrames(ActionId, DirectionId).Count)
                //{
                    State = state;
                    ActionId = Action2DDef.Defense.Id;
                    FrameIndex = 0x01;
                //}
            }
        }
    }

    /// <summary>
    /// 1. check skill
    /// 2. check attack
    /// 3. check move
    /// </summary>
    public void Update(IEngine engine)
    {
        _counter++;
        if (_counter < _updateCounter)
        {
            return;
        }

        if (!this.IsDead())
        {
            for (int iSkill = 0; iSkill < Skills.Count; iSkill++)
            {
                Skills[iSkill].Loop();
            }

            bool isValidate = false;
            for (int iSkill = 0; iSkill < Skills.Count; iSkill++)
            {
                if (Skills[iSkill].Check(engine, this))
                {
                    isValidate = true;
                    break;
                }
            }

            if (!isValidate)
            {
                MoveStrategy.Move(Map, this);
            }
        }
        else
        {
            SetAction(ObjState.Die);
        }

        _counter = 0;
        List<Frame2D> frames = Unit.Model.GetFrames(ActionId, DirectionId);

        FrameIndex++;
        if (FrameIndex > frames.Count)
        {
            if (this.State == ObjState.Die)
            {
                DeadTime++;
                FrameIndex = frames.Count;
            }
            else if (this.State == ObjState.Def)
            {
                DefenceCounter++;
                FrameIndex = frames.Count;
                if (DefenceCounter > DefenceCount)
                {
                    DefenceCounter = 0;
                    SetAction(ObjState.Stand);
                }
            }
            else if (this.State == ObjState.Attack)
            {
                SetAction(ObjState.Stand);
            }
            else
            {
                FrameIndex = 0x01;
            }
        }
    }

    /// <summary>
    /// 判断是否死亡
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        if (!_isDead)
        {
            if (HP <= 0)
            {
                _isDead = true;
                HP = 0;
                #region 从单元格的单位列表中删除
                MapCell cell = this.Map.GetCell(this.SitePos);
                for (int objIndex = 0; objIndex < cell.ObjList.Count; objIndex++)
                {
                    if (cell.ObjList[objIndex].ID == this.ID)
                    {
                        cell.ObjList.RemoveAt(objIndex);
                        break;
                    }
                }
                #endregion

                SetAction(ObjState.Die);
            }
        }
        return _isDead;
    }
}