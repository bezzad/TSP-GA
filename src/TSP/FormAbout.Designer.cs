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
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(259, 230);
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
            this.lnklblEmail.Location = new System.Drawing.Point(2, 201);
            this.lnklblEmail.Name = "lnklblEmail";
            this.lnklblEmail.Size = new System.Drawing.Size(251, 20);
            this.lnklblEmail.TabIndex = 1;
            this.lnklblEmail.TabStop = true;
            this.lnklblEmail.Text = "Contact to Mr. Behzad Khosravifar";
            this.lnklblEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblEmail_LinkClicked);
            // 
            // lbll
            // 
            this.lbll.AutoSize = true;
            this.lbll.BackColor = System.Drawing.Color.White;
            this.lbll.Location = new System.Drawing.Point(2, 6);
            this.lbll.Name = "lbll";
            this.lbll.Size = new System.Drawing.Size(245, 26);
            this.lbll.TabIndex = 2;
            this.lbll.Text = "TSP program\'s created by Mr. Behzad Khosravifar\r\n                         Creadit" +
    " Date: 2010/01/01";
            this.lbll.Click += new System.EventHandler(this.lbll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 33);
            this.label1.TabIndex = 3;
            this.label1.Text = "TSP";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 230);
            this.ControlBox = false;
            this.Controls.Add(this.lbll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnklblEmail);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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