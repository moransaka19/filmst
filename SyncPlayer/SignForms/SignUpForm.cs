using DAL.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using SyncPlayer.WorkerForms;

namespace SyncPlayer.SignForms
{
    public partial class SignUpForm : MaterialSkin.Controls.MaterialForm
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            if (tbox_email.TextLength >= 5 && tbox_password.TextLength >= 5 && tbox_repeatPasw.TextLength >= 5)
            {
                if (tbox_password.Text == tbox_repeatPasw.Text)
                {
                    string email = tbox_email.Text;
                    string password = tbox_password.Text;
                    //HttpPost
                    string response = UserDataPostRequest(email, password);
                    if (response == "sdsdvsdv")
                    {
                        ApplicationUser currentUser = new ApplicationUser() { Email = email, Password = password };
                        //Remember me system
                        Properties.Settings.Default.remEmail = email;
                        Properties.Settings.Default.remPassword = password;
                        Properties.Settings.Default.Save();
                        ConnectToRoomForm connectToRoomForm = new ConnectToRoomForm(currentUser);
                        connectToRoomForm.Show();
                        this.Close();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    tbox_repeatPasw.Text = string.Empty;
                    tbox_repeatPasw.Focus();
                }
            }
            else
            {
                tbox_email.Focus();
            }
        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string UserDataPostRequest(string email, string password)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new { Email = email, Password = password });

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

    }
}
