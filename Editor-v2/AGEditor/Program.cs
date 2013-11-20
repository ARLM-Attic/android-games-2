using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AGEditor.Windows.Workspace;
using AG.Editor.Core;
using AG.Editor.Windows;

namespace AGEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AGECache.Init(AppDomain.CurrentDomain);
            AG.Editor.Core.AGEContext.Current.Settings = AGECache.Current.SettingsStore.GetSettings();
            if (AG.Editor.Core.AGEContext.Current.Settings == null)
            {
                AG.Editor.Core.AGEContext.Current.Settings = new AG.Editor.Core.Settings.AGESettings();
            }

            AGELaunchWindow launchWindow = new AGELaunchWindow(AG.Editor.Core.AGEContext.Current.Settings);
            if (launchWindow.ShowDialog() == DialogResult.OK)
            {
                AG.Editor.Core.AGEContext.Current.Settings.LatestEProjectPath = launchWindow.SelectedEProject.Path;
                AG.Editor.Core.AGEContext.Current.EProject = launchWindow.SelectedEProject;
            }
            else
            {
                return;
            }

            Application.Run(new MainWindow3());

            /*
            AGEConfigUtil.Init(AppDomain.CurrentDomain);
            #region init workspace
            AGEditorConfig config = AGEConfigUtil.GetConfig();
            if (config == null)
            {
                // first time for use, create worksapce
                config = new AGEditorConfig();
                CreateWorkspaceWindow window = new CreateWorkspaceWindow();
                if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    config.Workspace = window.Workspace;
                    AGEConfigUtil.SaveConfig(config);
                    AGEContext.Current.Config = config;
                    // 设置资源保存路径
                    DATUtility.SetAppPath(AGEContext.Current.Config.Workspace.Path);
                }
                else
                {
                    //exit
                    return;
                }
            }
            else
            {
                // confirm workspace
                ConfirmWorkspaceWindow window = new ConfirmWorkspaceWindow(config);
                if (window.ShowDialog() == DialogResult.OK)
                {
                    config.Workspace = window.Workspace;
                    AGEContext.Current.Config = config;
                    // 设置资源保存路径
                    DATUtility.SetAppPath(AGEContext.Current.Config.Workspace.Path);
                }
                else
                {
                    // exit
                    return;
                }
            }
            #endregion
            Application.Run(new MainWindow2());

            AGEConfigUtil.SaveConfig(AGEContext.Current.Config);
            */
        }
    }
}
