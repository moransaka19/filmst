using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncPlayer
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
                    this.Close();
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
        
    }
}
