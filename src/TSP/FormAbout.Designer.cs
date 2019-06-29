namespace TSP
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lnklblEmail = new System.Windows.Forms.LinkLabel();
            this.lbll = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(340, 278);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lnklblEmail
            // 
            this.lnklblEmail.AutoSize = true;
            this.lnklblEmail.BackColor = System.Drawing.Color.White;
            this.lnklblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnklblEmail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnklblEmail.Location = new System.Drawing.Point(3, 247);
            this.lnklblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnklblEmail.Name = "lnklblEmail";
            this.lnklblEmail.Size = new System.Drawing.Size(310, 25);
            this.lnklblEmail.TabIndex = 1;
            this.lnklblEmail.TabStop = true;
            this.lnklblEmail.Text = "Contact to Mr. Behzad Khosravifar";
            this.lnklblEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblEmail_LinkClicked);
            // 
            // lbll
            // 
            this.lbll.AutoSize = true;
            this.lbll.BackColor = System.Drawing.Color.White;
            this.lbll.Location = new System.Drawing.Point(3, 7);
            this.lbll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbll.Name = "lbll";
            this.lbll.Size = new System.Drawing.Size(325, 34);
            this.lbll.TabIndex = 2;
            this.lbll.Text = "TSP program\'s created by Mr. Behzad Khosravifar\r\nCopyright @ 2010-2019";
            this.lbll.Click += new System.EventHandler(this.lbll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(124, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 42);
            this.label1.TabIndex = 3;
            this.label1.Text = "TSP";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(340, 278);
            this.ControlBox = false;
            this.Controls.Add(this.lbll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnklblEmail);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Click += new System.EventHandler(this.FormAbout_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lnklblEmail;
        private System.Windows.Forms.Label lbll;
        private System.Windows.Forms.Label label1;
    }
}