using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using AxWMPLib;

namespace SyncPlayer
{
    public partial class PlayerForm : MaterialSkin.Controls.MaterialForm
    {
        bool IsPlaying = false;
        ApplicationUser currentUser;

        public PlayerForm(ApplicationUser CurrentUser )
        {
            InitializeComponent();
            Player.URL = (@"C:\Users\Guilegaton\Desktop\Touhou Project\Bad Apple Rock Cover (Sam Luff Ver.) - Studio Yuraki.mp3");
            Player.Ctlcontrols.stop();
            Player.uiMode = "none";
            Player.enableContextMenu = false;
            currentUser = CurrentUser;
        }

        private void PlayPauseBTN_Click(object sender, EventArgs e)
        {
            if (!IsPlaying)
            {
                PlayPauseBTN.Text = "stop";
                Player.Ctlcontrols.play();
                IsPlaying = true;
            }
            else
            {
                PlayPauseBTN.Text = "play";
                Player.Ctlcontrols.pause();
                IsPlaying = false;
            }
        }

        private void ChatBTN_Click(object sender, EventArgs e)
        {
            AddTextToChat(currentUser, ChatTB.Text);
            ChatTB.Text = string.Empty;
        }

        private void AddTextToChat(ApplicationUser applicationUser, string message) => ChatRTB.Text += applicationUser.Email + ": " + message + "\r\n";

        private void ChatTB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                AddTextToChat(currentUser, ChatTB.Text);
                ChatTB.Text = string.Empty;
            }
        }
    }
}
