namespace SyncPlayer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axWMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.RunBTN = new System.Windows.Forms.Button();
            this.PathTB = new System.Windows.Forms.TextBox();
            this.PlaylistLB = new System.Windows.Forms.ListBox();
            this.ChangePlayListBTN = new System.Windows.Forms.Button();
            this.ChFoldBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).BeginInit();
            this.SuspendLayout();
            // 
            // axWMP
            // 
            this.axWMP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axWMP.Enabled = true;
            this.axWMP.Location = new System.Drawing.Point(12, 41);
            this.axWMP.Name = "axWMP";
            this.axWMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWMP.OcxState")));
            this.axWMP.Size = new System.Drawing.Size(550, 397);
            this.axWMP.TabIndex = 0;
            this.axWMP.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWMP_PlayStateChange);
            // 
            // RunBTN
            // 
            this.RunBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RunBTN.Location = new System.Drawing.Point(464, 12);
            this.RunBTN.Name = "RunBTN";
            this.RunBTN.Size = new System.Drawing.Size(98, 23);
            this.RunBTN.TabIndex = 2;
            this.RunBTN.Text = "Run this track";
            this.RunBTN.UseVisualStyleBackColor = true;
            this.RunBTN.Click += new System.EventHandler(this.RunBTN_Click);
            // 
            // PathTB
            // 
            this.PathTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathTB.Location = new System.Drawing.Point(12, 14);
            this.PathTB.Name = "PathTB";
            this.PathTB.Size = new System.Drawing.Size(446, 20);
            this.PathTB.TabIndex = 3;
            // 
            // PlaylistLB
            // 
            this.PlaylistLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaylistLB.Location = new System.Drawing.Point(568, 12);
            this.PlaylistLB.Name = "PlaylistLB";
            this.PlaylistLB.Size = new System.Drawing.Size(220, 394);
            this.PlaylistLB.TabIndex = 4;
            this.PlaylistLB.SelectedIndexChanged += new System.EventHandler(this.PlaylistLB_SelectedIndexChanged);
            // 
            // ChangePlayListBTN
            // 
            this.ChangePlayListBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChangePlayListBTN.Location = new System.Drawing.Point(568, 415);
            this.ChangePlayListBTN.Name = "ChangePlayListBTN";
            this.ChangePlayListBTN.Size = new System.Drawing.Size(108, 23);
            this.ChangePlayListBTN.TabIndex = 5;
            this.ChangePlayListBTN.Text = "Choose Playlist";
            this.ChangePlayListBTN.UseVisualStyleBackColor = true;
            this.ChangePlayListBTN.Click += new System.EventHandler(this.ChangePlayListBTN_Click);
            // 
            // ChFoldBTN
            // 
            this.ChFoldBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChFoldBTN.Location = new System.Drawing.Point(680, 415);
            this.ChFoldBTN.Name = "ChFoldBTN";
            this.ChFoldBTN.Size = new System.Drawing.Size(108, 23);
            this.ChFoldBTN.TabIndex = 6;
            this.ChFoldBTN.Text = "Choose Fold";
            this.ChFoldBTN.UseVisualStyleBackColor = true;
            this.ChFoldBTN.Click += new System.EventHandler(this.ChFoldBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ChFoldBTN);
            this.Controls.Add(this.ChangePlayListBTN);
            this.Controls.Add(this.PlaylistLB);
            this.Controls.Add(this.PathTB);
            this.Controls.Add(this.RunBTN);
            this.Controls.Add(this.axWMP);
            this.Name = "Form1";
            this.Text = "Player";
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWMP;
        private System.Windows.Forms.Button RunBTN;
        private System.Windows.Forms.TextBox PathTB;
        private System.Windows.Forms.ListBox PlaylistLB;
        private System.Windows.Forms.Button ChangePlayListBTN;
        private System.Windows.Forms.Button ChFoldBTN;
    }
}

