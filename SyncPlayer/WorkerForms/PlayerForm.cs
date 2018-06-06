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
using Microsoft.AspNet.SignalR.Client;

namespace SyncPlayer.WorkerForms
{
    public partial class PlayerForm : MaterialSkin.Controls.MaterialForm
    {
        bool IsPlaying = false;
        ApplicationUser currentUser;
        IHubProxy _hub;
        HubConnection connection;

        public PlayerForm(ApplicationUser CurrentUser, IEnumerable<Track> Playlist )
        {
            InitializeComponent();
            Player.Ctlcontrols.stop();
            Player.uiMode = "none";
            Player.enableContextMenu = false;
            currentUser = CurrentUser;
            string url = @"http://localhost:8080/";
            connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("ChatRoomHub");
            connection.Start().Wait();
            _hub.Invoke("Connect", currentUser.Email).Wait();
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
            _hub.Invoke("Send", ChatBTN.Text, currentUser.Email).Wait();
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

        private void PlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _hub.Invoke("OnDisconnected", currentUser.Email).Wait();
            connection.Stop();
        }
    }
}
