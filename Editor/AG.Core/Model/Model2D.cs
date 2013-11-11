using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Model2D
{
    public int Id { get; set; }
    public string Caption { get; set; }
    public ModelCategory Category { get; set; }

    public List<Action2D> Actions { get; set; }

    public Model2D()
    {
        Actions = new List<Action2D>();
    }

    public Action2D GetAction(int actionId)
    {
        foreach (var item in Actions)
        {
            if (item.Id == actionId)
            {
                return item;
            }
        }
        return null;
    }

    public Frame2D GetFrame(int actionId, int directionId, int index)
    {
        foreach (var action in Actions)
        {
            if (action.Id == actionId)
            {
                foreach (var direction in action.Directions)
                {
                    if (direction.Id == directionId)
                    {
                        foreach (var frame in direction.Frames)
                        {
                            if (frame.Index == index)
                            {
                                return frame;
                            }
                        }
                    }
                }
            }
        }
        return Actions[0].Directions[0].Frames[0];
    }

    public List<Frame2D> GetFrames(int actionId, int directionId)
    {
        foreach (var action in Actions)
        {
            if (action.Id == actionId)
            {
                foreach (var direction in action.Directions)
                {
                    if (direction.Id == directionId)
                    {
                        return direction.Frames;
                    }
                }
            }
        }
        return null;
    }
}