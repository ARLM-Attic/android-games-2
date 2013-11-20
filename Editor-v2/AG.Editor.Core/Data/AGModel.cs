using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;

namespace AG.Editor.Core.Data
{
    public class AGModel : System.ComponentModel.INotifyPropertyChanged
    {
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

        public void RemoveAudioRef(int actionId, int frameId)
        {
            for(int index = 0; index< AudioRefs.Count;index++)
            {
                AGAudioRef audio = AudioRefs[index];
                if (audio.ActionId == actionId && audio.FrameId == frameId)
                {
                    AudioRefs.RemoveAt(index);
                    return;
                }
            }
        }
        #endregion

        #region model
        public void AddAction(AGAction action)
        {
            Actions.Add(action);
            action.Model = this;
        }

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
    }
}
