using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserValidation
{
    public partial class SelectSignForm : Form
    {
        

        public SelectSignForm()
        {
            InitializeComponent();
        }

        private void btn_signin_Click(object sender, EventArgs e)
        {
            SignInForms.SignInForm signInForm = new SignInForms.SignInForm();
            signInForm.Show();
        }

        private void btn_signup_Click(object sender, EventArgs e)
        {
            SignUpForm signUpForm = new SignUpForm();
            signUpForm.Show();
        }
    }
}
