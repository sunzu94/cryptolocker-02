namespace CryptoLocker.WinForm
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTimeLeft = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.linkBitcoinHelp = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDecryptId = new System.Windows.Forms.TextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.lblDecryptInfo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.p1 = new System.Windows.Forms.Label();
            this.p2 = new System.Windows.Forms.Label();
            this.p3 = new System.Windows.Forms.Label();
            this.p4 = new System.Windows.Forms.Label();
            this.p5 = new System.Windows.Forms.Label();
            this.p6 = new System.Windows.Forms.Label();
            this.p7 = new System.Windows.Forms.Label();
            this.btnShowFiles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.AutoSize = true;
            this.lblTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeLeft.ForeColor = System.Drawing.Color.Yellow;
            this.lblTimeLeft.Location = new System.Drawing.Point(87, 359);
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(143, 37);
            this.lblTimeLeft.TabIndex = 0;
            this.lblTimeLeft.Text = "72:00:00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Your private key will be destroyed on ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(28, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(270, 259);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(102, 339);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time remaining";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(321, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(441, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Important! Your personal files are encrypted!";
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnNext.Location = new System.Drawing.Point(622, 437);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(139, 41);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Next >>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiryDate.Location = new System.Drawing.Point(66, 312);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(184, 20);
            this.lblExpiryDate.TabIndex = 7;
            this.lblExpiryDate.Text = "09/13/2016 10:00 pm";
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnPrev.Location = new System.Drawing.Point(462, 437);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(139, 41);
            this.btnPrev.TabIndex = 8;
            this.btnPrev.Text = "<< Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // linkBitcoinHelp
            // 
            this.linkBitcoinHelp.AutoSize = true;
            this.linkBitcoinHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkBitcoinHelp.LinkColor = System.Drawing.Color.Yellow;
            this.linkBitcoinHelp.Location = new System.Drawing.Point(16, 19);
            this.linkBitcoinHelp.MaximumSize = new System.Drawing.Size(400, 0);
            this.linkBitcoinHelp.Name = "linkBitcoinHelp";
            this.linkBitcoinHelp.Size = new System.Drawing.Size(356, 32);
            this.linkBitcoinHelp.TabIndex = 10;
            this.linkBitcoinHelp.TabStop = true;
            this.linkBitcoinHelp.Text = "Click here for general info on how to pay using Bitcoins (the cheapest)";
            this.linkBitcoinHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBitcoinHelp_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(380, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "When you have paid, you will receive an email with a password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(309, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Paste the password in this box and click decrypt";
            // 
            // tbDecryptId
            // 
            this.tbDecryptId.Location = new System.Drawing.Point(19, 171);
            this.tbDecryptId.Name = "tbDecryptId";
            this.tbDecryptId.Size = new System.Drawing.Size(373, 20);
            this.tbDecryptId.TabIndex = 13;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDecrypt.Location = new System.Drawing.Point(19, 197);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(98, 28);
            this.btnDecrypt.TabIndex = 14;
            this.btnDecrypt.Text = "DECRYPT";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // lblDecryptInfo
            // 
            this.lblDecryptInfo.AutoSize = true;
            this.lblDecryptInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecryptInfo.ForeColor = System.Drawing.Color.Yellow;
            this.lblDecryptInfo.Location = new System.Drawing.Point(16, 230);
            this.lblDecryptInfo.MaximumSize = new System.Drawing.Size(400, 0);
            this.lblDecryptInfo.Name = "lblDecryptInfo";
            this.lblDecryptInfo.Size = new System.Drawing.Size(29, 16);
            this.lblDecryptInfo.TabIndex = 15;
            this.lblDecryptInfo.Text = "Info";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(19, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "PAY";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.linkBitcoinHelp);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.lblDecryptInfo);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnDecrypt);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tbDecryptId);
            this.panel2.Location = new System.Drawing.Point(341, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 353);
            this.panel2.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnShowFiles);
            this.panel1.Controls.Add(this.p7);
            this.panel1.Controls.Add(this.p6);
            this.panel1.Controls.Add(this.p5);
            this.panel1.Controls.Add(this.p4);
            this.panel1.Controls.Add(this.p3);
            this.panel1.Controls.Add(this.p2);
            this.panel1.Controls.Add(this.p1);
            this.panel1.Location = new System.Drawing.Point(326, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 393);
            this.panel1.TabIndex = 19;
            // 
            // p1
            // 
            this.p1.AutoSize = true;
            this.p1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p1.Location = new System.Drawing.Point(10, 6);
            this.p1.MaximumSize = new System.Drawing.Size(425, 0);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(0, 17);
            this.p1.TabIndex = 0;
            // 
            // p2
            // 
            this.p2.AutoSize = true;
            this.p2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p2.Location = new System.Drawing.Point(31, 93);
            this.p2.MaximumSize = new System.Drawing.Size(400, 0);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(0, 17);
            this.p2.TabIndex = 1;
            // 
            // p3
            // 
            this.p3.AutoSize = true;
            this.p3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p3.Location = new System.Drawing.Point(17, 153);
            this.p3.MaximumSize = new System.Drawing.Size(400, 0);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(0, 17);
            this.p3.TabIndex = 2;
            // 
            // p4
            // 
            this.p4.AutoSize = true;
            this.p4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p4.Location = new System.Drawing.Point(15, 231);
            this.p4.MaximumSize = new System.Drawing.Size(400, 0);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(0, 17);
            this.p4.TabIndex = 3;
            // 
            // p5
            // 
            this.p5.AutoSize = true;
            this.p5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.p5.Location = new System.Drawing.Point(15, 274);
            this.p5.MaximumSize = new System.Drawing.Size(400, 0);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(0, 17);
            this.p5.TabIndex = 4;
            // 
            // p6
            // 
            this.p6.AutoSize = true;
            this.p6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.p6.Location = new System.Drawing.Point(17, 301);
            this.p6.MaximumSize = new System.Drawing.Size(400, 0);
            this.p6.Name = "p6";
            this.p6.Size = new System.Drawing.Size(0, 17);
            this.p6.TabIndex = 5;
            // 
            // p7
            // 
            this.p7.AutoSize = true;
            this.p7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p7.ForeColor = System.Drawing.Color.Red;
            this.p7.Location = new System.Drawing.Point(17, 327);
            this.p7.MaximumSize = new System.Drawing.Size(400, 0);
            this.p7.Name = "p7";
            this.p7.Size = new System.Drawing.Size(0, 17);
            this.p7.TabIndex = 6;
            // 
            // btnShowFiles
            // 
            this.btnShowFiles.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnShowFiles.Location = new System.Drawing.Point(136, 64);
            this.btnShowFiles.Name = "btnShowFiles";
            this.btnShowFiles.Size = new System.Drawing.Size(161, 23);
            this.btnShowFiles.TabIndex = 20;
            this.btnShowFiles.Text = "Show encrypted files";
            this.btnShowFiles.UseVisualStyleBackColor = true;
            this.btnShowFiles.Click += new System.EventHandler(this.btnShowFiles_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(773, 490);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.lblExpiryDate);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTimeLeft);
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "DarkCrypt3r";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTimeLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.LinkLabel linkBitcoinHelp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDecryptId;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Label lblDecryptInfo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label p1;
        private System.Windows.Forms.Label p7;
        private System.Windows.Forms.Label p6;
        private System.Windows.Forms.Label p5;
        private System.Windows.Forms.Label p4;
        private System.Windows.Forms.Label p3;
        private System.Windows.Forms.Label p2;
        private System.Windows.Forms.Button btnShowFiles;
    }
}

