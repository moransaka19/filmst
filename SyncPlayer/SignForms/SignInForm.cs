using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using SyncPlayer.WorkerForms;
using SyncPlayer.Models;

namespace SyncPlayer.SignForms
{
    //TODO: Refactor after libs
	public partial class SignInForm : MaterialSkin.Controls.MaterialForm
    {
        public SignInForm()
        {
            InitializeComponent();
        }

        private string UserDataPostRequest(string email, string password)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new { Email = email, Password = password});

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

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (tbox_email.TextLength >= 5)
            {
                string email = tbox_email.Text;
                string password = tbox_password.Text;
                //httpPost validation
                string response = UserDataPostRequest(email, password);
                if (response == "sdsdvsdv")
                {
                    ApplicationUser currentUser = new ApplicationUser() { Email = email, Password = password };
                    //Remember me system
                    if (cbox_remMe.Checked == true)
                    {
                        Properties.Settings.Default.remEmail = email;
                        Properties.Settings.Default.remPassword = password;
                        Properties.Settings.Default.Save();
                    }
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
                tbox_email.Focus();
            }
        }

        private void SignInForm_Load(object sender, EventArgs e)
        {
            //Remember me system // load info
            tbox_email.Text = Properties.Settings.Default.remEmail;
            tbox_password.Text = Properties.Settings.Default.remPassword;
            if (tbox_email.TextLength >= 5)
            {
                if (cbox_remMe.Checked == false) cbox_remMe.Checked = true;
            }
        }
        
        private void linkLab_forgotPasw_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotForm = new ForgotPasswordForm();
            this.Hide();
            forgotForm.ShowDialog();
            this.Show();
        }
    }
}
