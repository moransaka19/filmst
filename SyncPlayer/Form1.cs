using System;
using System.IO;
using System.Windows.Forms;

namespace SyncPlayer
{
    public partial class Form1 : Form
    {
        //[System.Runtime.InteropServices.DllImport("winmm.dll")]
        //private static extern Boolean PlaySound(string lpszName, int hmodule, int dwflags);
        
        public Form1()
        {
            InitializeComponent();
        }
        string[] paths;
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
            axWMP.URL = paths[PlaylistLB.SelectedIndex];
        }

        private void ChFoldBTN_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                paths = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                foreach (string path in paths)
                {
                    PlaylistLB.Items.Add(Path.GetFileName(path));
                }
            }
        }

        private void ChPlLstBTN_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                paths = openFileDialog1.FileNames;
                foreach (string path in paths)
                {
                    PlaylistLB.Items.Add(Path.GetFileName(path));
                }
            }
        }
    }
}