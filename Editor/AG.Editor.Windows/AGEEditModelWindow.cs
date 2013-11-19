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
        public AGModel Model { get; private set; }
        public AGModel SavedModel { get; set; }

        public AGEEditModelWindow()
        {
            InitializeComponent();
        }

        public AGEEditModelWindow(AGModel model)
        {
            Model = model;

            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (Model == null)
            {
                AGECreateModelWindow window = new AGECreateModelWindow();
                if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Model = window.CreatedModel;
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
            this.Text = Model.Caption;

            BindModelTree();
        }

        private void BindModelTree()
        {
            ctlTreeModel.Nodes.Clear();
            foreach (AGAction action in Model.Actions)
            {
                TreeNode tnAction = new TreeNode();
                tnAction.Tag = action;
                tnAction.Text = action.Caption;
                ctlTreeModel.Nodes.Add(tnAction);

                foreach (AGDirection direction in action.Directions)
                {
                    TreeNode tnDirection = new TreeNode();
                    tnDirection.Tag = direction;
                    tnDirection.Text = direction.ToString();
                    tnAction.Nodes.Add(tnDirection);

                    if (direction.RefDirectionId == null)
                    {
                        // 只有非引用方位才显示帧信息
                        foreach (AGFrame frame in direction.Frames)
                        {
                            TreeNode tnFrame = new TreeNode();
                            tnFrame.Tag = frame;
                            tnFrame.Text = frame.ToString();
                            tnDirection.Nodes.Add(tnFrame);
                        }
                    }
                }
            }
            ctlTreeModel.ExpandAll();
        }

        private void ctlBtnAddFrame_Click(object sender, EventArgs e)
        {
            if (ctlTreeModel.SelectedNode != null && ctlTreeModel.SelectedNode.Tag is AGDirection)
            {
                AGAction act = ctlTreeModel.SelectedNode.Parent.Tag as AGAction;
                AGDirection dir = ctlTreeModel.SelectedNode.Tag as AGDirection;

                string modelFolder = AG.Editor.Core.AGEContext.Current.EProject.GetFolder(Model);
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
                        tnFrame.Text = frame.ToString();
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
                AGEFrameEditWrapper wrapper = new AGEFrameEditWrapper(direction.Frames, frame, panel);
                wrapper.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(wrapper);
                panel.Settings(direction.Frames, frame);
            }
        }

        private void ctlBtnSave_Click(object sender, EventArgs e)
        {
            AG.Editor.Core.AGECache.Current.ModelStore.SaveModel(AG.Editor.Core.AGEContext.Current.EProject, Model);
            MessageBox.Show("保存成功!","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SavedModel = Model;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Model.HasChanged)
            {
                if (MessageBox.Show("是否保存?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    AG.Editor.Core.AGECache.Current.ModelStore.SaveModel(AG.Editor.Core.AGEContext.Current.EProject, Model);
                    SavedModel = Model;
                }
            }

            base.OnClosing(e);
        }

        /// <summary>
        /// 拷贝引用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBtnCopyRefClick(object sender, EventArgs e)
        {
            TreeNode selNode = ctlTreeModel.SelectedNode;
            if (selNode.Tag is AGDirection)
            {
                AGDirection dir = selNode.Tag as AGDirection;
                if (dir.RefDirection != null)
                {
                    // 引用别人的direction无法作为拷贝数据源
                    MessageBox.Show(string.Format("{0}不包含实际的帧信息，无法作为拷贝源!", dir.Caption), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("拷贝到其他方位，其他方位的帧数据将会被删除。\r\n是否执行拷贝操作?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    AGAction action = selNode.Parent.Tag as AGAction;
                    action.CopyRefToAll(dir);
                    BindModelTree();
                }
            }
        }

        /// <summary>
        /// 删除帧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlBtnRemoveFrame_Click(object sender, EventArgs e)
        {
            TreeNode selNode = ctlTreeModel.SelectedNode;
            if (selNode.Tag is AGFrame)
            {
                AGAction action = selNode.Parent.Parent.Tag as AGAction;
                AGDirection direction = selNode.Parent.Tag as AGDirection;
                AGFrame frame = selNode.Tag as AGFrame;

                if (MessageBox.Show(string.Format("是否要删除帧[{0}]?", frame.ImageFileName), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    this.panel1.Controls.Clear();
                    Model.RemoveFrame(action.Id, direction.Id, frame.Id);

                    BindModelTree();
                }
            }
            else
            {
                MessageBox.Show("需要选择帧!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
