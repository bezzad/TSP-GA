namespace TSP
{
    partial class ProcessorAffinityForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCurrentProcessName = new System.Windows.Forms.Label();
            this.chlstProcessors = new System.Windows.Forms.CheckedListBox();
            this.btnInfo = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.Location = new System.Drawing.Point(176, 287);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(257, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCurrentProcessName
            // 
            this.lblCurrentProcessName.AutoSize = true;
            this.lblCurrentProcessName.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblCurrentProcessName.Location = new System.Drawing.Point(12, 9);
            this.lblCurrentProcessName.Name = "lblCurrentProcessName";
            this.lblCurrentProcessName.Size = new System.Drawing.Size(217, 32);
            this.lblCurrentProcessName.TabIndex = 2;
            this.lblCurrentProcessName.Text = "Which processors are allowed to run\r\n(.exe)?";
            // 
            // chlstProcessors
            // 
            this.chlstProcessors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chlstProcessors.CheckOnClick = true;
            this.chlstProcessors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.chlstProcessors.FormattingEnabled = true;
            this.chlstProcessors.Location = new System.Drawing.Point(12, 54);
            this.chlstProcessors.Name = "chlstProcessors";
            this.chlstProcessors.ScrollAlwaysVisible = true;
            this.chlstProcessors.Size = new System.Drawing.Size(320, 208);
            this.chlstProcessors.TabIndex = 4;
            this.chlstProcessors.TabStop = false;
            // 
            // btnInfo
            // 
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Help;
            this.btnInfo.Location = new System.Drawing.Point(279, 5);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(53, 43);
            this.btnInfo.TabIndex = 5;
            this.btnInfo.Text = "&Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtInfo.Cursor = System.Windows.Forms.Cursors.Help;
            this.txtInfo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfo.Location = new System.Drawing.Point(-1, -1);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(346, 324);
            this.txtInfo.TabIndex = 6;
            this.txtInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInfo.Visible = false;
            this.txtInfo.Click += new System.EventHandler(this.txtInfo_Click);
            // 
            // ProcessorAffinityForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(344, 322);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.chlstProcessors);
            this.Controls.Add(this.lblCurrentProcessName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProcessorAffinityForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processor Affinity";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProcessorAffinityForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCurrentProcessName;
        private System.Windows.Forms.CheckedListBox chlstProcessors;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.TextBox txtInfo;
    }
}