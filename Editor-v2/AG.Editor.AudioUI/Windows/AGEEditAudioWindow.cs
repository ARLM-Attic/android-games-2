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
using AG.Editor.Core.Metadata;

namespace AG.Editor.AudioUI.Windows
{
    public partial class AGEEditAudioWindow : Form
    {
        private bool _isCreateMode;

        public AGAudio Audio { get; private set; }

        public AGEEditAudioWindow()
        {
            InitializeComponent();
            _isCreateMode = true;
        }

        public AGEEditAudioWindow(AGAudio audio)
        {
            InitializeComponent();
            Audio = audio;
            _isCreateMode = false;
        }

        protected override void OnShown(EventArgs e)
        {
            if (Audio == null)
            {
                Audio = new AGAudio();
            }
            UpdateUI();

            base.OnShown(e);
        }

        private void UpdateUI()
        {
            ctlListCategory.Items.Clear();
            foreach (var category in AG.Editor.Core.AGEContext.Current.EProject.TProject.AudioCateogries)
            {
                ctlListCategory.Items.Add(category);
                if (Audio.Category != null && category.Id == Audio.CategoryId)
                {
                    ctlListCategory.SelectedItem = category;
                }
            }

            ctlEditId.Text = Audio.Id.ToString();
            if (!string.IsNullOrEmpty(Audio.Caption))
            {
                ctlLinkFile.Text = Audio.Caption;
            }
        }

        private void ctlLinkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_isCreateMode)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(dlg.FileName);
                    Audio.Caption = fileInfo.Name;
                    Audio.FilePath = fileInfo.Name;
                    // 拷贝文件到资源目录
                    string audioFolder = AG.Editor.Core.AGEContext.Current.EProject.GetDataAudioFolder();
                    string audioFilePath = string.Format("{0}{1}", audioFolder, Audio.FilePath);
                    if (!Directory.Exists(audioFolder))
                    {
                        Directory.CreateDirectory(audioFolder);
                    }
                    File.Copy(dlg.FileName, audioFilePath, true);

                    // 更新到界面上
                    ctlLinkFile.Text = Audio.Caption;
                }
            }
            else
            {
                if (MessageBox.Show("是否要修改音效资源文件?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Multiselect = false;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        FileInfo fileInfo = new FileInfo(dlg.FileName);
                        Audio.Caption = fileInfo.Name;
                        Audio.FilePath = fileInfo.Name;
                        // 拷贝文件到资源目录
                        string audioFolder = AG.Editor.Core.AGEContext.Current.EProject.GetDataAudioFolder();
                        string audioFilePath = string.Format("{0}{1}", audioFolder, Audio.FilePath);
                        if (!Directory.Exists(audioFolder))
                        {
                            Directory.CreateDirectory(audioFolder);
                        }
                        File.Copy(dlg.FileName, audioFilePath, true);

                        UpdateUI();
                    }
                }
            }
        }

        private void ctlBtnOK_Click(object sender, EventArgs e)
        {
            if (ctlListCategory.SelectedItem == null)
            {
                return;
            }

            int audioId = 0;
            if (int.TryParse(ctlEditId.Text, out audioId))
            {
                Audio.Id = audioId;
                Audio.Category = ctlListCategory.SelectedItem as AGAudioCategory;
                Audio.CategoryId = Audio.Category.Id;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
