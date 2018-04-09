namespace UserValidation.SignInForms
{
    partial class SignInForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbox_email = new System.Windows.Forms.TextBox();
            this.tbox_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLab_forgotPasw = new System.Windows.Forms.LinkLabel();
            this.cbox_remMe = new System.Windows.Forms.CheckBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbox_email
            // 
            this.tbox_email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbox_email.Location = new System.Drawing.Point(12, 29);
            this.tbox_email.MaxLength = 64;
            this.tbox_email.Name = "tbox_email";
            this.tbox_email.Size = new System.Drawing.Size(260, 20);
            this.tbox_email.TabIndex = 0;
            this.tbox_email.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbox_password
            // 
            this.tbox_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbox_password.Location = new System.Drawing.Point(12, 74);
            this.tbox_password.MaxLength = 64;
            this.tbox_password.Name = "tbox_password";
            this.tbox_password.PasswordChar = '*';
            this.tbox_password.Size = new System.Drawing.Size(260, 20);
            this.tbox_password.TabIndex = 1;
            this.tbox_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "EMail:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // linkLab_forgotPasw
            // 
            this.linkLab_forgotPasw.AutoSize = true;
            this.linkLab_forgotPasw.Location = new System.Drawing.Point(187, 97);
            this.linkLab_forgotPasw.Name = "linkLab_forgotPasw";
            this.linkLab_forgotPasw.Size = new System.Drawing.Size(85, 13);
            this.linkLab_forgotPasw.TabIndex = 4;
            this.linkLab_forgotPasw.TabStop = true;
            this.linkLab_forgotPasw.Text = "Forgot password";
            this.linkLab_forgotPasw.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLab_forgotPasw_LinkClicked);
            // 
            // cbox_remMe
            // 
            this.cbox_remMe.AutoSize = true;
            this.cbox_remMe.Location = new System.Drawing.Point(12, 97);
            this.cbox_remMe.Name = "cbox_remMe";
            this.cbox_remMe.Size = new System.Drawing.Size(94, 17);
            this.cbox_remMe.TabIndex = 5;
            this.cbox_remMe.Text = "Remember me";
            this.cbox_remMe.UseVisualStyleBackColor = true;
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(12, 120);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(75, 30);
            this.btn_login.TabIndex = 6;
            this.btn_login.Text = "Log in";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // SignInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 163);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.cbox_remMe);
            this.Controls.Add(this.linkLab_forgotPasw);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbox_password);
            this.Controls.Add(this.tbox_email);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SignInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign in";
            this.Load += new System.EventHandler(this.SignInForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbox_email;
        private System.Windows.Forms.TextBox tbox_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLab_forgotPasw;
        private System.Windows.Forms.CheckBox cbox_remMe;
        private System.Windows.Forms.Button btn_login;
    }
}