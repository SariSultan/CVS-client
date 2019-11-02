namespace CVS.ClientV1
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.devIdTxtBox = new System.Windows.Forms.TextBox();
            this.authKeyTxtBox = new System.Windows.Forms.TextBox();
            this.ExitPB = new System.Windows.Forms.PictureBox();
            this.MinimizePB = new System.Windows.Forms.PictureBox();
            this.scanBtn = new System.Windows.Forms.Button();
            this.versionLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ExitPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizePB)).BeginInit();
            this.SuspendLayout();
            // 
            // devIdTxtBox
            // 
            this.devIdTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.devIdTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.devIdTxtBox.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.devIdTxtBox.Location = new System.Drawing.Point(103, 240);
            this.devIdTxtBox.Name = "devIdTxtBox";
            this.devIdTxtBox.Size = new System.Drawing.Size(222, 19);
            this.devIdTxtBox.TabIndex = 0;
            this.devIdTxtBox.Text = "FreeTrial";
            // 
            // authKeyTxtBox
            // 
            this.authKeyTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.authKeyTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authKeyTxtBox.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.authKeyTxtBox.Location = new System.Drawing.Point(103, 290);
            this.authKeyTxtBox.Name = "authKeyTxtBox";
            this.authKeyTxtBox.PasswordChar = '*';
            this.authKeyTxtBox.Size = new System.Drawing.Size(222, 19);
            this.authKeyTxtBox.TabIndex = 0;
            this.authKeyTxtBox.Text = "Passw0rD";
            // 
            // ExitPB
            // 
            this.ExitPB.BackColor = System.Drawing.Color.Transparent;
            this.ExitPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExitPB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitPB.Image = global::CVS.ClientV1.Properties.Resources.x_Icon;
            this.ExitPB.Location = new System.Drawing.Point(349, 92);
            this.ExitPB.Name = "ExitPB";
            this.ExitPB.Size = new System.Drawing.Size(15, 15);
            this.ExitPB.TabIndex = 88;
            this.ExitPB.TabStop = false;
            // 
            // MinimizePB
            // 
            this.MinimizePB.BackColor = System.Drawing.Color.Transparent;
            this.MinimizePB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MinimizePB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimizePB.Image = global::CVS.ClientV1.Properties.Resources.Minus_Icon;
            this.MinimizePB.Location = new System.Drawing.Point(328, 92);
            this.MinimizePB.Name = "MinimizePB";
            this.MinimizePB.Size = new System.Drawing.Size(15, 15);
            this.MinimizePB.TabIndex = 89;
            this.MinimizePB.TabStop = false;
            // 
            // scanBtn
            // 
            this.scanBtn.BackColor = System.Drawing.Color.Transparent;
            this.scanBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scanBtn.FlatAppearance.BorderSize = 0;
            this.scanBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(29)))), ((int)(((byte)(124)))));
            this.scanBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(29)))), ((int)(((byte)(124)))));
            this.scanBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scanBtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scanBtn.ForeColor = System.Drawing.Color.White;
            this.scanBtn.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.scanBtn.Location = new System.Drawing.Point(29, 360);
            this.scanBtn.Name = "scanBtn";
            this.scanBtn.Size = new System.Drawing.Size(343, 63);
            this.scanBtn.TabIndex = 90;
            this.scanBtn.Text = "Scan This Machine";
            this.scanBtn.UseVisualStyleBackColor = false;
            this.scanBtn.Click += new System.EventHandler(this.scanBtn_Click);
            // 
            // versionLbl
            // 
            this.versionLbl.AutoSize = true;
            this.versionLbl.BackColor = System.Drawing.Color.Transparent;
            this.versionLbl.ForeColor = System.Drawing.Color.White;
            this.versionLbl.Location = new System.Drawing.Point(268, 163);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(69, 13);
            this.versionLbl.TabIndex = 91;
            this.versionLbl.Text = "Version: 1.1b";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(269, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 92;
            this.label1.Text = "Beta";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(402, 484);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.versionLbl);
            this.Controls.Add(this.scanBtn);
            this.Controls.Add(this.MinimizePB);
            this.Controls.Add(this.ExitPB);
            this.Controls.Add(this.devIdTxtBox);
            this.Controls.Add(this.authKeyTxtBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Push-Based Vulnerability Scanner";
            this.TransparencyKey = System.Drawing.Color.Black;
            ((System.ComponentModel.ISupportInitialize)(this.ExitPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizePB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox devIdTxtBox;
        private System.Windows.Forms.TextBox authKeyTxtBox;
        private System.Windows.Forms.PictureBox ExitPB;
        private System.Windows.Forms.PictureBox MinimizePB;
        private System.Windows.Forms.Button scanBtn;
        private System.Windows.Forms.Label versionLbl;
        private System.Windows.Forms.Label label1;
    }
}

