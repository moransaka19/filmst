using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WMPLib;
using static System.Windows.Forms.CheckedListBox;

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

        private void ChFileBTN_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "mp3 files (*.mp3)|*.mp3|" +
                                     "mp4 files (*.mp4)|*.mp4|" +
                                     "avi files (*.avi)|*.avi|" +
                                     "ogg files (*.ogg)|*.ogg|" +
                                     "wmv files (*.wmv)|*.wmv";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PathTB.Text = openFileDialog1.FileName;
            }
        }

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

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                paths.AddRange(Directory.GetFiles(folderBrowserDialog1.SelectedPath));
                foreach (string path in Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                {
                    PlaylistLB.Items.Add(Path.GetFileName(path));
                }
            }
        }

        private void ChangePlayListBTN_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
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
            
            if (axWMP.playState != WMPPlayState.wmppsStopped)
            {
                axWMP.Ctlcontrols.play();
            }

            if (axWMP.playState == WMPPlayState.wmppsPaused) axWMP.Ctlcontrols.pause();
        }
    }
}