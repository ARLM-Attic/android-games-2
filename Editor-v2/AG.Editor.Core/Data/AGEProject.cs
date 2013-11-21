using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;
using System.IO;

namespace AG.Editor.Core.Data
{
    public class AGEProject : System.ComponentModel.INotifyPropertyChanged
    {
        public string TPName { get; set; }
        public string TPVersion { get; set; }
        public AGTProject TProject { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }

        /// <summary>
        /// 日期版本,dtver
        /// </summary>
        public string DateVersion { get; set; }

        public List<AGModelRef> Models { get; set; }
        /// <summary>
        /// 音频资源列表
        /// </summary>
        public List<AGAudio> Audios { get; private set; }

        private bool _hasChanged;
        /// <summary>
        /// 标记是否有被修改，如果被修改那么在关闭的时候需要提示用户是否保存
        /// </summary>
        public bool HasChanged
        {
            get
            {
                return _hasChanged;
            }
            private set
            {
                if (_hasChanged != value)
                {
                    _hasChanged = value;
                    RaisePropertyChange("HasChanged");
                }
            }
        }

        public AGEProject()
        {
            Models = new List<AGModelRef>();
            Audios = new List<AGAudio>();
        }

        /// <summary>
        /// 清除修改标记
        /// </summary>
        public void SaveComplete()
        {
            HasChanged = false;
        }

        #region audio
        public void AddAudio(AGAudio audio)
        {
            Audios.Add(audio);
            HasChanged = true;
        }

        public void RemoveAudio(int audioId)
        {
            for (int index = 0; index < Audios.Count; index++)
            {
                if (Audios[index].Id == audioId)
                {
                    Audios.RemoveAt(index);
                    HasChanged = true;
                    return;
                }
            }
        }

        public AGAudio GetAudio(int audioId)
        {
            for (int index = 0; index < Audios.Count; index++)
            {
                if (Audios[index].Id == audioId)
                {
                    return Audios[index];
                }
            }
            return null;
        }

        public List<AGAudio> GetAudios()
        {
            return Audios;
        }
        #endregion

        /// <summary>
        /// 删除模型
        /// </summary>
        /// <param name="modelId"></param>
        public void RemoveModel(Guid modelUniqueId)
        {
            for (int index = 0; index < Models.Count; index++)
            {
                AGModelRef refModel = Models[index];
                if (refModel.ModelUniqueId == modelUniqueId)
                {
                    Models.RemoveAt(index);
                    HasChanged = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 根据actionid获取 action的名称
        /// </summary>
        /// <param name="modelCategoryId"></param>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public string GetActionCapton(int modelCategoryId, int actionId)
        {
            foreach (var mc in TProject.ModelCategories)
            {
                if (mc.Id == modelCategoryId)
                {
                    foreach (var act in mc.Actions)
                    {
                        if (act.Id == actionId)
                        {
                            return act.Caption;
                        }
                    }
                }
            }
            return "unknown";
        }

        /// <summary>
        /// 根据direction id获取direction的名称
        /// </summary>
        /// <param name="modelCategoryId"></param>
        /// <param name="actionId"></param>
        /// <param name="directionId"></param>
        /// <returns></returns>
        public string GetDirectionCapton(int modelCategoryId, int actionId, int directionId)
        {
            foreach (var mc in TProject.ModelCategories)
            {
                if (mc.Id == modelCategoryId)
                {
                    foreach (var act in mc.Actions)
                    {
                        if (act.Id == actionId)
                        {
                            foreach (var dir in act.Directions)
                            {
                                if (dir.Id == directionId)
                                {
                                    return dir.Caption;
                                }
                            }
                        }
                    }
                }
            }
            return "unknown";
        }

        public string GetFolder(AGEProject project)
        {
            FileInfo fileInfo = new FileInfo(Path);
            string folder = fileInfo.Directory.FullName;
            return folder;
        }

        public string GetDataModelsFolder()
        {
            FileInfo fileInfo = new FileInfo(Path);
            string folder = fileInfo.Directory.FullName;
            string modelsFolder = System.IO.Path.Combine(folder, ".\\data\\models\\");
            return modelsFolder;
        }

        /// <summary>
        /// 获取声音资源存放的目录 .\data\audio\
        /// </summary>
        /// <returns></returns>
        public string GetDataAudioFolder()
        {
            FileInfo fileInfo = new FileInfo(Path);
            string folder = fileInfo.Directory.FullName;
            string audioFolder = System.IO.Path.Combine(folder, ".\\data\\audio\\");
            return audioFolder;
        }

        /// <summary>
        /// 获取模型图片存放的路径
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetFolder(AGModel obj)
        {
            FileInfo fileInfo = new FileInfo(Path);
            string folder = fileInfo.Directory.FullName;
            string modelsFolder = System.IO.Path.Combine(folder, ".\\data\\models\\");
            string modelFolder = System.IO.Path.Combine(modelsFolder, string.Format(".\\{0}\\", obj.UniqueId));
            return modelFolder;
        }

        public string GetFrameFilePath(AGFrame frame)
        {
            string modelFolder = GetFolder(frame.Direction.Action.Model);
            return string.Format("{0}\\{1}", modelFolder, frame.ImageFileName);
        }

        #region implement property change interface
        private void RaisePropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
