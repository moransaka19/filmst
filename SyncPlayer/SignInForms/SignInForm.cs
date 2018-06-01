using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncPlayer.SignInForms
{
    public partial class SignInForm : MaterialSkin.Controls.MaterialForm
    {
        public SignInForm()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (tbox_email.TextLength >= 5)
            {
                string email = tbox_email.Text;
                string password = tbox_password.Text;
                //httpPost validation

                //Remember me system
                if (cbox_remMe.Checked == true)
                {
                    Properties.Settings.Default.remEmail = email;
                    Properties.Settings.Default.remPassword = password;
                    Properties.Settings.Default.Save();
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
