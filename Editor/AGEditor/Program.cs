using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AGEditor.Windows.Workspace;

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
        }
    }
}
