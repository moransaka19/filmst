using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WMPLib;
using TagLib;
using TagLib.Mpeg;

namespace SyncPlayer
{
    public partial class Form1 : Form
    {
        //[System.Runtime.InteropServices.DllImport("winmm.dll")]
        //private static extern Boolean PlaySound(string lpszName, int hmodule, int dwflags);

        public Form1()
        {
            InitializeComponent();
            paths = new List<string>();
        }
        List<string> paths;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

        private void RunBTN_Click(object sender, EventArgs e)
        {
            axWMP.URL = PathTB.Text;
            axWMP.Ctlcontrols.play();
        }

        private void PlaylistLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            PathTB.Text = paths[PlaylistLB.SelectedIndex];
        }

        private void ChFoldBTN_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = choosefileoption(false);
                folderBrowserDialog1.Description = "Выберите папку, содержащую аудио/видео файлы";
                folderBrowserDialog1.ShowNewFolderButton = false;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string[] formats = { "*.aac", "*.aiff", "*.flac", "*.m4a", "*.mmf", "*.mp3", "*.opus", "*.wav", "*.wma", "*.ogg",
                                         "*.3g2"," *.3gp"," *.avi"," *.flv"," *.mkv"," *.mov"," *.mp4"," *.mpeg"," *.webm"," *.wmv" };
                    foreach (string format in formats)
                    {
                        paths.AddRange(Directory.GetFiles(folderBrowserDialog1.SelectedPath, format, SearchOption.AllDirectories));

                        foreach (string path in Directory.GetFiles(folderBrowserDialog1.SelectedPath, format, SearchOption.AllDirectories))
                        {
                            PlaylistLB.Items.Add(Path.GetFileName(path));
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("Нет доступа к одной из выбранных папок", "System");
            }

        }

        private void ChangePlayListBTN_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = choosefileoption(true);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                paths.AddRange(openFileDialog1.FileNames);
                foreach (string path in openFileDialog1.FileNames)
                {
                    PlaylistLB.Items.Add(Path.GetFileName(path));
                }

            }
        }

        private void axWMP_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWMP.playState == WMPPlayState.wmppsMediaEnded)
            {
                try
                {
                    axWMP.URL = PathTB.Text = paths[PlaylistLB.SelectedIndex += 1];
                }
                catch
                {
                    axWMP.Ctlcontrols.stop();
                }
            }

            if (axWMP.playState != WMPPlayState.wmppsStopped && axWMP.playState != WMPPlayState.wmppsPaused)
            {
                axWMP.Ctlcontrols.play();
            }
        }

        private string choosefileoption(bool type)
        {
            if (type) openFileDialog1.Multiselect = true;
            else openFileDialog1.Multiselect = false;

            return "Media Files(*.aac;*.aiff;*.flac;*.m4a;*.mmf;*.mp3;*.opus;*.wav;*.wma;*.ogg;" +
                               "*.3g2;*.3gp;*.avi;*.flv;*.mkv;*.mov;*.mp4;*.mpeg;*.webm;*.wmv)|" +
                               "*.aac;*.aiff;*.flac;*.m4a;*.mmf;*.mp3;*.opus;*.wav;*.wma;*.ogg;" +
                               "*.3g2;*.3gp;*.avi;*.flv;*.mkv;*.mov;*.mp4;*.mpeg;*.webm;*.wmv";
        }

        private void axWMP_MediaChange(object sender, AxWMPLib._WMPOCXEvents_MediaChangeEvent e)
        {

        }
    }
}