using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGEditer
{
    public partial class AutoModelingWindow : Form
    {
        public List<Point> _offsets;

        private int _fileIndex;

        private int _atkCount;
        private int _dieCount;
        private int _bhitCount;
        private int _defCount;
        private int _stdCount;
        private int _movCount;
        private int _totalCount;

        private int _modelId;
        private string _modelCaption;

        private string _directory;

        public AutoModelingWindow()
        {
            InitializeComponent();

            List<ModelCategory> categories = ModelCategory.GetDefs();
            foreach (var item in categories)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = dlg.FileName;
                FileInfo fi = new FileInfo(name);

                _directory = fi.Directory.FullName;
                string fileIndex = fi.Name.Replace(fi.Extension, string.Empty);

                _fileIndex = Convert.ToInt32(fileIndex);
                

                linkLabel1.Text = fileIndex;

                if (!LoadOffsets(fi.Directory.FullName))
                {
                    return;
                }

                if (!LoadCount())
                {
                    return;
                }

                if (!LoadInfo())
                {
                    return;
                }
                MessageBox.Show(fileIndex);
                
            }
        }

        private bool LoadOffsets(string path)
        {
            _offsets = new List<Point>();
            string filePath = Path.Combine(path, "偏移信息.txt");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("未发现偏移信息文件!");
                return false;
            }

            string[] offsetStrings = File.ReadAllLines(filePath);

            for (int i = 0; i < offsetStrings.Length; i++)
            {
                string[] offsetXY = offsetStrings[i].Replace(" ", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (offsetXY.Length == 2)
                {
                    Point pt = new Point(
                        Math.Abs(Convert.ToInt32(offsetXY[0])),
                        Math.Abs(Convert.ToInt32(offsetXY[1])));

                    _offsets.Add(pt);
                }
            }

            if (_offsets.Count != offsetStrings.Length - 1)
            {
                MessageBox.Show("偏移信息文件中的数据有错误!");
                return false;
            }
            return true;
        }

        private bool LoadCount()
        {
            try
            {
                _atkCount = Convert.ToInt32(textBox1.Text);
                _dieCount = Convert.ToInt32(textBox2.Text);
                _bhitCount = Convert.ToInt32(textBox3.Text);
                _defCount = Convert.ToInt32(textBox4.Text);
                _stdCount = Convert.ToInt32(textBox5.Text);
                _movCount = Convert.ToInt32(textBox6.Text);

                _totalCount = _atkCount + _dieCount + _bhitCount + _defCount + _stdCount + _movCount;

                return true;
            }
            catch
            {
                MessageBox.Show("参数信息输入不对!");
                return false;
            }
        }

        private bool LoadInfo()
        {
            try
            {
                _modelId = Convert.ToInt32(textBox7.Text);
            }
            catch
            {
                return false;
            }

            _modelCaption = textBox8.Text.Trim();
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!LoadCount())
            {
                return;
            }

            if (!LoadInfo())
            {
                return;
            }

            List<Direction2DDef> dirDefs = Direction2DDef.GetDefs();
            List<Action2DDef> actDefs = Action2DDef.GetDefs();

            Model2D model = new Model2D();
            model.Id = _modelId;
            model.Caption = _modelCaption;
            model.Category = comboBox1.SelectedItem as ModelCategory;

            foreach (var actDef in actDefs)
            {
                Action2D action = new Action2D();
                action.Id = actDef.Id;
                action.Caption = actDef.Caption;
                foreach (var dirDef in dirDefs)
                {
                    Direction2D direction = new Direction2D();
                    direction.Id = dirDef.Id;
                    direction.Caption = dirDef.Caption;

                    action.Directions.Add(direction);
                }

                model.Actions.Add(action);
            }

            LoadFrames(model, Action2DDef.Attack.Id, 0, _atkCount);
            LoadFrames(model, Action2DDef.Die.Id, _atkCount, _dieCount);
            LoadFrames(model, Action2DDef.BHit.Id, _atkCount + _dieCount, _bhitCount);
            LoadFrames(model, Action2DDef.Defense.Id, _atkCount + _dieCount + _bhitCount, _defCount);
            LoadFrames(model, Action2DDef.Stand.Id, _atkCount + _dieCount + _bhitCount + _defCount, _stdCount);
            LoadFrames(model, Action2DDef.Move.Id, _atkCount + _dieCount + _bhitCount + _defCount + _stdCount, _movCount);

            string publishPath = string.Format("{1}models\\{0:d4}\\", model.Id, DATUtility.GetResPath());
            DATUtility.SaveModel(model);
            System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(publishPath);
            if (!System.IO.Directory.Exists(dirInfo.FullName))
            {
                System.IO.Directory.CreateDirectory(dirInfo.FullName);
            }
            PACKUtility.PublishModel(model, publishPath);
            MessageBox.Show("发布成功!");
        }

        private void LoadFrames(Model2D model, int actionId, int offset, int frameCount)
        {
            Action2D action = model.GetAction(actionId);
            for (int dirIndex = 0; dirIndex < action.Directions.Count; dirIndex++)
            {
                Direction2D direction = action.Directions[dirIndex];
                int offsetIndex = dirIndex * _totalCount;
                offsetIndex += offset;

                for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
                {
                    int frameFileIndex = _fileIndex + offsetIndex + frameIndex;

                    Frame2D frame = new Frame2D();
                    frame.Index = frameIndex + 1;
                    frame.OffsetX = _offsets[offsetIndex + frameIndex].X;
                    frame.offsetY = _offsets[offsetIndex + frameIndex].Y;

                    string frameFileName = string.Format("{0}\\{1}.bmp", _directory, frameFileIndex);
                    Bitmap bitmap = new Bitmap(frameFileName);
                    frame.Width = bitmap.Width;
                    frame.Height = bitmap.Height;
                    frame.Data = File.ReadAllBytes(frameFileName);

                    direction.Frames.Add(frame);
                }
            }
        }
    }
}

/*
 5 3 2 1 4 3   
 5 3 2 1 4 3  
 5 3 2 1 4 3  
 5 3 2 1 4 3  
 5 3 2 1 4 3  
 5 3 2 1 4 3  
 5 3 2 1 4 3  
 5 3 2 1 4 3
*/