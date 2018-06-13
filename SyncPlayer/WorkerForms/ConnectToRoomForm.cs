using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using SyncPlayer.Models;

namespace SyncPlayer.WorkerForms
{
	public partial class ConnectToRoomForm : MaterialSkin.Controls.MaterialForm
    {
        private List<Track> Playlist;
        private HttpClient client;
        private ApplicationUser currentUser;

        public ConnectToRoomForm(ApplicationUser CurrentUser)
        {
            InitializeComponent();
            Playlist = new List<Track>();
            client = new HttpClient();
            currentUser = CurrentUser;
        }

        private void ChooseDirectoryBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (DirectoryDialog.ShowDialog() == DialogResult.OK)
                {
                    Playlist.Clear();
                    string[] formats = { "*.aac", "*.aiff", "*.flac", "*.m4a", "*.mmf", "*.mp3", "*.opus", "*.wav", "*.wma", "*.ogg",
                                         "*.3g2"," *.3gp"," *.avi"," *.flv"," *.mkv"," *.mov"," *.mp4"," *.mpeg"," *.webm"," *.wmv" };
                    foreach (string format in formats)
                    {
                        foreach (var item in Directory.GetFiles(DirectoryDialog.SelectedPath, format, SearchOption.AllDirectories))
                        {
                            var trackTags = TagLib.File.Create(item);
                            Playlist.Add(new Track()
                            {
                                TrackName = trackTags.Tag.Title,
                                Year = trackTags.Tag.Year.ToString(),
                                Genre = trackTags.Tag.FirstGenre,
                                Author = trackTags.Tag.FirstArtist,
                                Album = trackTags.Tag.Album,
                                AudioChanels = trackTags.Properties.AudioChannels.ToString(),
                                Bits = trackTags.Properties.AudioSampleRate.ToString(),
                                Length = trackTags.Properties.Duration.ToString(),
                                Bitrate = trackTags.Properties.AudioBitrate.ToString(),
                                FileSize = (int)new FileInfo(item).Length
                            });
                        }
                    }
                    DirecoryPathTB.Text = DirectoryDialog.SelectedPath;
                }
            }
            catch
            {
                MessageBox.Show("Нет доступа к одной из выбранных папок", "System");
            }
        }

        private string RoomFindPostRequest(string roomName, string roomPassword, List<Track> playlist)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new { RoomName = roomName, RoomPassword = roomPassword, Playlist = playlist });

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var result = string.Empty;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        private void ConnectBTN_Click(object sender, EventArgs e)
        {
            if (Playlist.Count != 0)
            {
                string response = RoomFindPostRequest(RoomNameTB.Text, RoomPasswordTB.Text, Playlist);
                if (response == "dfvdfdfbdfb")
                {
                    PlayerForm playerForm = new PlayerForm(currentUser, Playlist);
                    playerForm.Show();
                    this.Close();
                }
                else
                {
                    //error message
                }
            }
            else
            {
                MessageBox.Show("Fill playlist", "Filmst");
            }
        }
    }
}
