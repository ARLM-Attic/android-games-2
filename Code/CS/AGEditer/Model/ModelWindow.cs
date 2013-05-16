using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AGEditer
{
    public partial class ModelWindow : Form
    {
        private Model2D _model;

        public ModelWindow()
        {
            InitializeComponent();
        }

        private void createModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateModelWindow dlg = new CreateModelWindow();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _model = new Model2D();
                _model.Id = dlg.ModelId;
                _model.Caption = dlg.ModelCaption;
                _model.Category = dlg.ModelCategory;

                List<Action2DDef> defs = Action2DDef.GetDefs();
                List<Direction2DDef> dirDefs = Direction2DDef.GetDefs();
                foreach (var def in defs)
                {
                    Action2D action = new Action2D();
                    action.Id = def.Id;
                    action.Caption = def.Caption;

                    foreach (var dirDef in dirDefs)
                    {
                        Direction2D dir = new Direction2D();
                        dir.Id = dirDef.Id;
                        dir.Caption = dirDef.Caption;

                        action.Directions.Add(dir);
                    }

                    _model.Actions.Add(action);
                }

                BindToTree(treeView1, _model);
            }
        }

        private void BindToTree(TreeView tree, Model2D model)
        {
            tree.Nodes.Clear();

            TreeNode tnModel = new TreeNode();
            tnModel.Text = string.Format("[{0}]{1}({2})", model.Category.Caption, model.Caption, model.Id);
            tnModel.Tag = model;

            foreach (var action in model.Actions)
            {
                TreeNode tnAction = new TreeNode();
                tnAction.Text = action.Caption;
                tnAction.Tag = action;

                foreach (var dir in action.Directions)
                {
                    TreeNode tnDir = new TreeNode();
                    tnDir.Text = dir.Caption;
                    tnDir.Tag = dir;

                    BindToTree(tnDir, dir);


                    tnAction.Nodes.Add(tnDir);

                }

                tnModel.Nodes.Add(tnAction);
            }

            tree.Nodes.Add(tnModel);
            tree.ExpandAll();
        }

        private void BindToTree(TreeNode node, Direction2D dir)
        {
            node.Nodes.Clear();
            foreach (var frame in dir.Frames)
            {
                TreeNode tnFrame = new TreeNode();
                BindToTree(tnFrame, frame);

                node.Nodes.Add(tnFrame);
            }
            node.ExpandAll();
        }

        private void BindToTree(TreeNode frameNode, Frame2D frame)
        {
            frameNode.Text = string.Format("frame({0})-({1},{2})-({3},{4})",
                frame.Index,
                frame.Width,
                frame.Height,
                frame.OffsetX,
                frame.offsetY);
            //frameNode.ToolTipText = frame.FileName + "\r\n" + frame.OrginalFile;
            frameNode.Tag = frame;
        }

        private void addFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            if (selNode == null || selNode.Tag == null)
            {
                return;
            }

            if (selNode.Tag.GetType() == typeof(Direction2D))
            {
                Direction2D dir = selNode.Tag as Direction2D;

                FrameWindow dlg = new FrameWindow();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 计算Index
                    if (dir.Frames.Count > 0)
                    {
                        dlg.Frame.Index = dir.Frames.Last().Index + 1;
                    }
                    else
                    {
                        dlg.Frame.Index = 1;
                    }

                    dir.Frames.Add(dlg.Frame);

                    BindToTree(selNode, dir);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = e.Node;
            if (selNode.Tag.GetType() == typeof(Model2D))
            {
                flowLayoutPanel1.Controls.Clear();

                Model2D model = selNode.Tag as Model2D;
                foreach (var action in model.Actions)
                {
                    foreach (var direction in action.Directions)
                    {
                        foreach (var item in direction.Frames)
                        {
                            PictureBox pic = new PictureBox();
                            pic.SizeMode = PictureBoxSizeMode.AutoSize;
                            pic.Image = new Bitmap(new System.IO.MemoryStream(item.Data));

                            flowLayoutPanel1.Controls.Add(pic);
                        }
                    }
                }
            }
            else if (selNode.Tag.GetType() == typeof(Action2D))
            {
                flowLayoutPanel1.Controls.Clear();
                Action2D action = selNode.Tag as Action2D;

                foreach (var direction in action.Directions)
                {
                    foreach (var item in direction.Frames)
                    {
                        PictureBox pic = new PictureBox();
                        pic.SizeMode = PictureBoxSizeMode.AutoSize;
                        pic.Image = new Bitmap(new System.IO.MemoryStream(item.Data));

                        flowLayoutPanel1.Controls.Add(pic);
                    }
                }
            }
            else if (selNode.Tag.GetType() == typeof(Direction2D))
            {
                flowLayoutPanel1.Controls.Clear();
                Direction2D direction = selNode.Tag as Direction2D;
                foreach (var item in direction.Frames)
                {
                    PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.AutoSize;
                    pic.Image = new Bitmap(new System.IO.MemoryStream(item.Data));

                    flowLayoutPanel1.Controls.Add(pic);
                }
            }
        }

        private void addFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            if (selNode == null || selNode.Tag == null)
            {
                return;
            }

            if (selNode.Tag.GetType() == typeof(Direction2D))
            {
                Direction2D dir = selNode.Tag as Direction2D;

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = true;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var fileNmae in dlg.FileNames)
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileNmae);
                        Frame2D frame = new Frame2D();
                        frame.Data = System.IO.File.ReadAllBytes(fileNmae);
                        Bitmap bmp = new Bitmap(fileInfo.FullName);
                        frame.Width = bmp.Width;
                        frame.Height = bmp.Height;

                        // 计算Index
                        if (dir.Frames.Count > 0)
                        {
                            frame.Index = dir.Frames.Last().Index + 1;
                        }
                        else
                        {
                            frame.Index = 1;
                        }

                        dir.Frames.Add(frame);
                    }

                    BindToTree(selNode, dir);
                }
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            if (selNode == null)
            {
                return;
            }
            if (selNode.Tag.GetType() == typeof(Model2D))
            {
                Model2D model = selNode.Tag as Model2D;
                PreviewWindow dlg = new PreviewWindow(model);
                dlg.ShowDialog();
            }
            else if (selNode.Tag.GetType() == typeof(Action2D))
            {
                Model2D model = selNode.Parent.Tag as Model2D;
                Action2D action = selNode.Tag as Action2D;
                PreviewWindow dlg = new PreviewWindow(model, action);
                dlg.ShowDialog();
            }
            else if (selNode.Tag.GetType() == typeof(Direction2D))
            {
                Model2D model = selNode.Parent.Parent.Tag as Model2D;
                Action2D action = selNode.Parent.Tag as Action2D;
                Direction2D dir = selNode.Tag as Direction2D;
                PreviewWindow dlg = new PreviewWindow(model, action, dir);
                dlg.ShowDialog();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectModelWindow dlg = new SelectModelWindow();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _model = dlg.SelectedModel;
                foreach (var action in _model.Actions)
                {
                    foreach (var direction in action.Directions)
                    {
                        foreach (var frame in direction.Frames)
                        {
                            frame.Data = ResourceLoader.GetFrameData(_model.Id, action.Id, direction.Id, frame.Index);
                        }
                    }
                }
                BindToTree(treeView1, _model);
            }
        }

        private void editFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            if (selNode == null)
            {
                return;
            }
            if (selNode.Tag.GetType() == typeof(Frame2D))
            {
                Frame2D frame = selNode.Tag as Frame2D;
                FrameWindow dlg = new FrameWindow(frame);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                }
                BindToTree(selNode, frame);
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode selNode = treeView1.HitTest(e.Location).Node;
            if (selNode != null)
            {
            }

            if (selNode.Tag.GetType() == typeof(Frame2D))
            {
                Frame2D frame = selNode.Tag as Frame2D;
                FrameWindow dlg = new FrameWindow(frame);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                }
                BindToTree(selNode, frame);
            }
        }

        private void _ctlBtnPublish_Click(object sender, EventArgs e)
        {
            string publishPath = string.Format("{1}models\\{0:d4}\\", _model.Id, DATUtility.GetResPath());
            DATUtility.SaveModel(_model);
            System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(publishPath);
            if (!System.IO.Directory.Exists(dirInfo.FullName))
            {
                System.IO.Directory.CreateDirectory(dirInfo.FullName);
            }
            PACKUtility.PublishModel(_model, publishPath);
            MessageBox.Show("发布成功!");
        }

        private void _btnCopyToAll_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            if (selNode != null)
            {
            }

            if (selNode.Tag.GetType() == typeof(Frame2D))
            {
                Action2D sourceAction = selNode.Parent.Parent.Tag as Action2D;
                Direction2D sourceDir = selNode.Parent.Tag as Direction2D;
                Frame2D frame = selNode.Tag as Frame2D;

                foreach (var action in _model.Actions)
                {
                    foreach (var direction in action.Directions)
                    {
                        if (action.Id == sourceAction.Id && direction.Id == sourceDir.Id)
                        {
                            continue;
                        }
                        
                        Frame2D newFrame = new Frame2D();
                        newFrame.Data = frame.Data;
                        newFrame.Width = frame.Width;
                        newFrame.Height = frame.Height;
                        newFrame.OffsetX = frame.OffsetX;
                        newFrame.offsetY = frame.offsetY;

                        // 计算Index
                        if (direction.Frames.Count > 0)
                        {
                            newFrame.Index = direction.Frames.Last().Index + 1;
                        }
                        else
                        {
                            newFrame.Index = 1;
                        }

                        direction.Frames.Add(newFrame);
                    }
                }

                BindToTree(treeView1, _model);
            }
        }
    }
}


