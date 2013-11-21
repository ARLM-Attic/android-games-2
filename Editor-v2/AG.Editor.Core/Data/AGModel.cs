using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;

namespace AG.Editor.Core.Data
{
    public class AGModel : System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 唯一标识,不会发布
        /// </summary>
        public Guid UniqueId { get; private set; }
        /// <summary>
        /// 模型的真实编号
        /// </summary>
        public int Id { get; set; }
        public string Caption { get; set; }
        public int CategoryId { get; set; }
        public AGModelCategory Category { get; set; }
        public List<AGAction> Actions { get; private set; }
        public List<AGAudioRef> AudioRefs { get; private set; }

        private bool _hasChanged;
        public bool HasChanged
        {
            get
            {
                return _hasChanged;
            }
            set
            {
                if (_hasChanged != value)
                {
                    _hasChanged = value;
                    RaisePropertyChanged("HasChanged");
                }
            }
        }

        public AGModel()
        {
            UniqueId = Guid.NewGuid();
            _hasChanged = false;
            Actions = new List<AGAction>();
            AudioRefs = new List<AGAudioRef>();
        }

        public AGModel(Guid uniqueId)
        {
            UniqueId = uniqueId;
            _hasChanged = false;
            Actions = new List<AGAction>();
            AudioRefs = new List<AGAudioRef>();
        }

        /// <summary>
        /// 重置修改状态
        /// </summary>
        public void Reset()
        {
            _hasChanged = false;
        }

        #region audio
        public List<AGAudioRef> GetAudioRefs()
        {
            return AudioRefs;
        }

        public void AddAudioRef(AGAudioRef audio)
        {
            AudioRefs.Add(audio);
        }

        public void RemoveAudioRef(AGAudioRef audio)
        {
            RemoveAudioRef(audio.ActionId, audio.FrameIndex);
        }

        public void RemoveAudioRef(int actionId, int frameIndex)
        {
            for(int index = 0; index< AudioRefs.Count;index++)
            {
                AGAudioRef audio = AudioRefs[index];
                if (audio.ActionId == actionId && audio.FrameIndex == frameIndex)
                {
                    AudioRefs.RemoveAt(index);
                    return;
                }
            }
        }
        #endregion

        #region model
        //public void AddAction(AGAction action)
        //{
        //    Actions.Add(action);
        //    action.Model = this;
        //}

        /// <summary>
        /// 删除1帧
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="directionId"></param>
        /// <param name="frameId"></param>
        public void RemoveFrame(int actionId, int directionId, int frameId)
        {
            AGAction action = GetAction(actionId);
            AGDirection direction = action.GetDirection(directionId);
            List<AGFrame> frames = direction.GetFrames();

            for (int index = 0; index < frames.Count; index++)
            {
                if(frames[index].Id == frameId)
                {
                    direction.RemoveFrame(frameId);
                }
            }
        }

        /// <summary>
        /// 获取动作对象
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public AGAction GetAction(int actionId)
        {
            for (int index = 0; index < Actions.Count; index++)
            {
                if (Actions[index].Id == actionId)
                {
                    return Actions[index];
                }
            }
            return null;
        }

        public AGFrame GetFrame(int actionId, int directionId, int frameId)
        {
            AGAction action = GetAction(actionId);
            AGDirection direction = action.GetDirection(directionId);
            return direction.GetFrame(frameId);
        }

        public AGFrame GetFrame(int actionId, int frameIndex)
        {
            AGAction action = GetAction(actionId);
            AGDirection direction = action.GetDirection(0);
            List<AGFrame> frames = direction.GetFrames();
            return frames[frameIndex];
            //return null;
        }
        #endregion

        #region validate
        /// <summary>
        /// 检查各个动作，各个方向是否含有帧，并且帧数是否一致
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            foreach (var action in Actions)
            {
                foreach (var direction in action.Directions)
                {
                    if (direction.RefDirection == null && direction.Frames.Count == 0)
                    {
                        return false;
                    }
                }
            }

            foreach (var action in Actions)
            {
                for (int index = 1; index < action.Directions.Count; index++)
                {
                    AGDirection direction1 = action.Directions[index - 1];
                    AGDirection direction2 = action.Directions[index];

                    if ((direction1.RefDirection != null && direction2.RefDirection != null) && direction1.Frames.Count != direction2.Frames.Count)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        public override string ToString()
        {
            return string.Format("{0}[{1}]", Caption, Id);
        }

        #region implement interface property changed

        private void RaisePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        public static AGModel ModelWidthCategory(AGModelCategory category, Guid uniqueId)
        {
            AGModel model = new AGModel(uniqueId);
            model.CategoryId = category.Id;
            model.Category = category;

            for (int iFrame = 0; iFrame < category.Actions.Count; iFrame++)
            {
                AGAction cAction = category.Actions[iFrame];
                AGAction action = new AGAction(cAction.Id);
                action.Caption = cAction.Caption;
                model.Actions.Add(action);
                action.Model = model;

                for (int iDirection = 0; iDirection < cAction.Directions.Count; iDirection++)
                {
                    AGDirection cDirection = cAction.Directions[iDirection];
                    AGDirection direction = new AGDirection(cDirection.Id);
                    direction.Caption = cDirection.Caption;
                    action.Directions.Add(direction);
                    direction.Action = action;
                }
            }
           
            return model;
        }
    }
}
