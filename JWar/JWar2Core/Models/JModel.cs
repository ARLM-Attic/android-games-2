using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWar2Core
{
    public class JModel
    {
        public int Id { get; set; }
        public List<JAction> Actions { get; set; }

        public JModel()
        {
        }

        public JAction GetAction(int actionId)
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

        public JFrame GetFrame(int actionId, int directionId, int index)
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
                                if (frame.Id == index)
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

        public List<JFrame> GetFrames(int actionId, int directionId)
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
}