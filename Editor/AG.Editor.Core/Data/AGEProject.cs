using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.Editor.Core.Metadata;
using System.IO;

namespace AG.Editor.Core.Data
{
    public class AGEProject
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

        public AGEProject()
        {
            Models = new List<AGModelRef>();
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
        /// 获取模型图片存放的路径
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetFolder(AGModel obj)
        {
            FileInfo fileInfo = new FileInfo(Path);
            string folder = fileInfo.Directory.FullName;
            string modelsFolder = System.IO.Path.Combine(folder, ".\\data\\models\\");
            string modelFolder = System.IO.Path.Combine(modelsFolder, string.Format(".\\{0:d8}\\", obj.Id));
            return modelFolder;
        }

        public string GetFrameFilePath(AGFrame frame)
        {
            string modelFolder = GetFolder(frame.Direction.Action.Model);
            return string.Format("{0}\\{1}", modelFolder, frame.ImageFileName);
        }
    }
}
