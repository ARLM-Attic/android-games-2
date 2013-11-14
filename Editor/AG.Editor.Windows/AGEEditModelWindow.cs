using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.Editor.Core.Data;
using System.IO;
using AG.Editor.Windows.Controls;

namespace AG.Editor.Windows
{
    public partial class AGEEditModelWindow : Form
    {
        private AGModel _model;

        public AGEEditModelWindow()
        {
            InitializeComponent();
        }

        public AGEEditModelWindow(AGModel model)
        {
            _model = model;

            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_model == null)
            {
                AGECreateModelWindow window = new AGECreateModelWindow();
                if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _model = window.CreatedModel;
                    // 添加到项目中
                    AG.Editor.Core.AGEContext.Current.EProject.Models.Add(new AGModelRef(_model));
                }
                else
                {
                    Close();
                }
            }
            InitUI();
        }

        private void InitUI()
        {
            this.Text = _model.Caption;

            BindModelTree();
        }

        private void BindModelTree()
        {
            foreach (AGAction action in _model.Actions)
            {
                TreeNode tnAction = new TreeNode();
                tnAction.Tag = action;
                tnAction.Text = action.Caption;
                ctlTreeModel.Nodes.Add(tnAction);

                foreach (AGDirection direction in action.Directions)
                {
                    TreeNode tnDirection = new TreeNode();
                    tnDirection.Tag = direction;
                    tnDirection.Text = direction.Caption;
                    tnAction.Nodes.Add(tnDirection);

                    foreach (AGFrame frame in direction.Frames)
                    {
                        TreeNode tnFrame = new TreeNode();
                        tnFrame.Tag = frame;
                        tnFrame.Text = frame.Id.ToString();
                        tnDirection.Nodes.Add(tnFrame);
                    }
                }
            }
        }

        private void ctlBtnAddFrame_Click(object sender, EventArgs e)
        {
            if (ctlTreeModel.SelectedNode != null && ctlTreeModel.SelectedNode.Tag is AGDirection)
            {
                AGAction act = ctlTreeModel.SelectedNode.Parent.Tag as AGAction;
                AGDirection dir = ctlTreeModel.SelectedNode.Tag as AGDirection;

                string modelFolder = AG.Editor.Core.AGEContext.Current.EProject.GetFolder(_model);
                if (!Directory.Exists(modelFolder))
                {
                    Directory.CreateDirectory(modelFolder);
                }

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = true;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var fileNmae in dlg.FileNames)
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileNmae);
                        AGFrame frame = new AGFrame();
                        // 计算Index
                        if (dir.Frames.Count > 0)
                        {
                            frame.Id = dir.Frames.Count;
                        }
                        else
                        {
                            frame.Id = 0;
                        }

                        Bitmap bmp = new Bitmap(fileInfo.FullName);
                        frame.Width = bmp.Width;
                        frame.Height = bmp.Height;

                        dir.AddFrame(frame);
                        //string frameImageName = string.Format("{0:d8}-{1:d2}-{2:d2}-{3:d2}{4}", _model.Id, act.Id, dir.Id, frame.Id, fileInfo.Extension);
                        string frameImageName = fileInfo.Name;
                        frame.ImageFileName = frameImageName;


                        File.Copy(fileInfo.FullName, string.Format("{0}\\{1}", modelFolder, frameImageName), true);
                        // 拷贝资源到项目目录
                        /*
                        Bitmap bmp = new Bitmap(fileInfo.FullName);
                        frame.Width = bmp.Width;
                        frame.Height = bmp.Height;

                        MemoryStream stream = new MemoryStream();
                        Bitmap destImage = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        for (int wIndex = 0; wIndex < bmp.Width; wIndex++)
                        {
                            for (int hIndex = 0; hIndex < bmp.Height; hIndex++)
                            {
                                Color color = bmp.GetPixel(wIndex, hIndex);

                                if (color.R == 0x00 && color.G == 0x00 && color.B == 0x00)
                                {
                                    destImage.SetPixel(wIndex, hIndex, Color.FromArgb(0, 255, 255, 255));
                                }
                                else
                                {
                                    destImage.SetPixel(wIndex, hIndex, color);
                                }
                            }
                        }
                        destImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        frame.Data = new byte[stream.Length];
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.Read(frame.Data, 0, (int)stream.Length);
                        
                        */

                        TreeNode tnFrame = new TreeNode();
                        tnFrame.Tag = frame;
                        tnFrame.Text = frame.ImageFileName;
                        ctlTreeModel.SelectedNode.Nodes.Add(tnFrame);
                        ctlTreeModel.SelectedNode.Expand();
                    }
                }
            }
        }

        /// <summary>
        /// 选择项目改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlTreeModel_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = ctlTreeModel.SelectedNode;
            if (selNode.Tag is AGFrame)
            {
                this.panel1.Controls.Clear();
                AGDirection direction = selNode.Parent.Tag as AGDirection;
                AGFrame frame = selNode.Tag as AGFrame;
                //AGEPreviewFramePanel previewPanel = new AGEPreviewFramePanel(frame);
                AGEFrameEditPanel panel = new AGEFrameEditPanel();
                AGEFrameEditWrapper wrapper = new AGEFrameEditWrapper(panel);
                wrapper.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(wrapper);
                panel.Settings(direction.Frames, frame);
            }
        }

        private void ctlBtnSave_Click(object sender, EventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("是否要保存?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                AG.Editor.Core.AGECache.Current.ModelStore.SaveModel(AG.Editor.Core.AGEContext.Current.EProject, _model);
                AG.Editor.Core.AGECache.Current.EProjectStore.SaveEProject(AG.Editor.Core.AGEContext.Current.EProject);
            }
            else
            {
                // 必须保存。哈哈
                return;
            }

            base.OnClosing(e);
        }
    }
}
