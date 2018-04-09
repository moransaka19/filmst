namespace UserValidation
{
    partial class SignUpForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbox_email = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbox_password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbox_repeatPasw = new System.Windows.Forms.TextBox();
            this.btn_register = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "EMail:";
            // 
            // tbox_email
            // 
            this.tbox_email.Location = new System.Drawing.Point(12, 30);
            this.tbox_email.MaxLength = 64;
            this.tbox_email.Name = "tbox_email";
            this.tbox_email.Size = new System.Drawing.Size(259, 20);
            this.tbox_email.TabIndex = 1;
            this.tbox_email.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // tbox_password
            // 
            this.tbox_password.Location = new System.Drawing.Point(12, 69);
            this.tbox_password.MaxLength = 64;
            this.tbox_password.Name = "tbox_password";
            this.tbox_password.PasswordChar = '*';
            this.tbox_password.Size = new System.Drawing.Size(259, 20);
            this.tbox_password.TabIndex = 3;
            this.tbox_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Repeat password:";
            // 
            // tbox_repeatPasw
            // 
            this.tbox_repeatPasw.Location = new System.Drawing.Point(12, 108);
            this.tbox_repeatPasw.MaxLength = 64;
            this.tbox_repeatPasw.Name = "tbox_repeatPasw";
            this.tbox_repeatPasw.PasswordChar = '*';
            this.tbox_repeatPasw.Size = new System.Drawing.Size(259, 20);
            this.tbox_repeatPasw.TabIndex = 5;
            this.tbox_repeatPasw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_register
            // 
            this.btn_register.Location = new System.Drawing.Point(12, 134);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(75, 23);
            this.btn_register.TabIndex = 6;
            this.btn_register.Text = "Register";
            this.btn_register.UseVisualStyleBackColor = true;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // SignUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 171);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.tbox_repeatPasw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbox_password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbox_email);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SignUpForm";
            this.Text = "SignUpForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbox_email;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbox_password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbox_repeatPasw;
        private System.Windows.Forms.Button btn_register;
    }
}