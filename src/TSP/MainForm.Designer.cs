namespace TSP
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numPopulation = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatuslblNumCity0 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatuslblNumCity = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatuslblLocate = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLenght = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGeneration = new System.Windows.Forms.Label();
            this.dgvCity = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newRandomCitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.timerFitnessGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicalGraphicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessPriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetPriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RealtimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HighToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboveNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BelowNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetAffinityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.pGAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskParallelismToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadParallelismToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parallelForToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartStop = new System.Windows.Forms.CheckBox();
            this.btnPauseResume = new System.Windows.Forms.CheckBox();
            this.timerGenerationGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.generationFitnessGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numPopulation)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCity)).BeginInit();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            // 
            // numPopulation
            // 
            this.numPopulation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numPopulation.Location = new System.Drawing.Point(703, 510);
            this.numPopulation.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPopulation.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPopulation.Name = "numPopulation";
            this.numPopulation.Size = new System.Drawing.Size(69, 20);
            this.numPopulation.TabIndex = 10;
            this.numPopulation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.numPopulation, "Number of Population Chromosomes");
            this.numPopulation.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numPopulation.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numPopulation.ValueChanged += new System.EventHandler(this.numPopulation_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatuslblNumCity0,
            this.toolStripStatuslblNumCity,
            this.toolStripProgressBar1,
            this.toolStripStatuslblLocate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 510);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(794, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatuslblNumCity0
            // 
            this.toolStripStatuslblNumCity0.Name = "toolStripStatuslblNumCity0";
            this.toolStripStatuslblNumCity0.Size = new System.Drawing.Size(95, 17);
            this.toolStripStatuslblNumCity0.Text = "Number of City: ";
            // 
            // toolStripStatuslblNumCity
            // 
            this.toolStripStatuslblNumCity.Name = "toolStripStatuslblNumCity";
            this.toolStripStatuslblNumCity.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatuslblNumCity.Text = "0";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.RightToLeftLayout = true;
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatuslblLocate
            // 
            this.toolStripStatuslblLocate.ForeColor = System.Drawing.Color.DarkRed;
            this.toolStripStatuslblLocate.Name = "toolStripStatuslblLocate";
            this.toolStripStatuslblLocate.Size = new System.Drawing.Size(469, 17);
            this.toolStripStatuslblLocate.Spring = true;
            this.toolStripStatuslblLocate.Text = "                    X = 0  ,  Y = 0";
            this.toolStripStatuslblLocate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.label1.Cursor = System.Windows.Forms.Cursors.No;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(252, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Length: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblLenght
            // 
            this.lblLenght.AutoSize = true;
            this.lblLenght.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.lblLenght.Cursor = System.Windows.Forms.Cursors.No;
            this.lblLenght.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLenght.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLenght.ForeColor = System.Drawing.Color.Crimson;
            this.lblLenght.Location = new System.Drawing.Point(322, 1);
            this.lblLenght.Name = "lblLenght";
            this.lblLenght.Size = new System.Drawing.Size(40, 17);
            this.lblLenght.TabIndex = 3;
            this.lblLenght.Text = "0000";
            this.lblLenght.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Cursor = System.Windows.Forms.Cursors.No;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(484, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Generation:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblGeneration
            // 
            this.lblGeneration.AutoSize = true;
            this.lblGeneration.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblGeneration.Cursor = System.Windows.Forms.Cursors.No;
            this.lblGeneration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGeneration.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneration.ForeColor = System.Drawing.Color.Crimson;
            this.lblGeneration.Location = new System.Drawing.Point(584, 1);
            this.lblGeneration.Name = "lblGeneration";
            this.lblGeneration.Size = new System.Drawing.Size(40, 17);
            this.lblGeneration.TabIndex = 3;
            this.lblGeneration.Text = "0000";
            this.lblGeneration.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dgvCity
            // 
            this.dgvCity.AllowUserToAddRows = false;
            this.dgvCity.AllowUserToDeleteRows = false;
            this.dgvCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colCP});
            this.dgvCity.Location = new System.Drawing.Point(542, 35);
            this.dgvCity.Name = "dgvCity";
            this.dgvCity.ReadOnly = true;
            this.dgvCity.RowHeadersVisible = false;
            this.dgvCity.RowHeadersWidth = 30;
            this.dgvCity.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvCity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCity.Size = new System.Drawing.Size(240, 469);
            this.dgvCity.TabIndex = 5;
            // 
            // colNo
            // 
            this.colNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Monotype Corsiva", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.colNo.FillWeight = 50F;
            this.colNo.Frozen = true;
            this.colNo.HeaderText = "Number City";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Width = 99;
            // 
            // colCP
            // 
            this.colCP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCP.HeaderText = "City Positions";
            this.colCP.Name = "colCP";
            this.colCP.ReadOnly = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Text files|*.txt";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label3.Location = new System.Drawing.Point(586, 513);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Number of Population:";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.ProcessPriorityToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(794, 24);
            this.menuStripMain.TabIndex = 14;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.importToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importToolStripMenuItem.Image")));
            this.importToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.importToolStripMenuItem.Text = "&Import Cities Position";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(224, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.exportToolStripMenuItem.Text = "&Export Cities Position";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(224, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newRandomCitiesToolStripMenuItem,
            this.toolStripSeparator2,
            this.timerFitnessGraphToolStripMenuItem,
            this.timerGenerationGraphToolStripMenuItem,
            this.generationFitnessGraphToolStripMenuItem,
            this.toolStripSeparator5,
            this.dynamicalGraphicToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // newRandomCitiesToolStripMenuItem
            // 
            this.newRandomCitiesToolStripMenuItem.Name = "newRandomCitiesToolStripMenuItem";
            this.newRandomCitiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F5)));
            this.newRandomCitiesToolStripMenuItem.ShowShortcutKeys = false;
            this.newRandomCitiesToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.newRandomCitiesToolStripMenuItem.Text = "&New Random Cities";
            this.newRandomCitiesToolStripMenuItem.Click += new System.EventHandler(this.newRandomCitiesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(245, 6);
            // 
            // timerFitnessGraphToolStripMenuItem
            // 
            this.timerFitnessGraphToolStripMenuItem.Name = "timerFitnessGraphToolStripMenuItem";
            this.timerFitnessGraphToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F5)));
            this.timerFitnessGraphToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.timerFitnessGraphToolStripMenuItem.Text = "Timer Fitness Graph";
            this.timerFitnessGraphToolStripMenuItem.Click += new System.EventHandler(this.timerFitnessGraphToolStripMenuItem_Click);
            // 
            // dynamicalGraphicToolStripMenuItem
            // 
            this.dynamicalGraphicToolStripMenuItem.Checked = true;
            this.dynamicalGraphicToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dynamicalGraphicToolStripMenuItem.Name = "dynamicalGraphicToolStripMenuItem";
            this.dynamicalGraphicToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.dynamicalGraphicToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.dynamicalGraphicToolStripMenuItem.Text = "&Dynamical Graphic";
            this.dynamicalGraphicToolStripMenuItem.Click += new System.EventHandler(this.dynamicalGraphicToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShowShortcutKeys = false;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ProcessPriorityToolStripMenuItem
            // 
            this.ProcessPriorityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetPriorityToolStripMenuItem,
            this.SetAffinityToolStripMenuItem,
            this.toolStripSeparator4,
            this.pGAToolStripMenuItem});
            this.ProcessPriorityToolStripMenuItem.Name = "ProcessPriorityToolStripMenuItem";
            this.ProcessPriorityToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.ProcessPriorityToolStripMenuItem.Text = "&Process Setting";
            // 
            // SetPriorityToolStripMenuItem
            // 
            this.SetPriorityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RealtimeToolStripMenuItem,
            this.HighToolStripMenuItem,
            this.AboveNormalToolStripMenuItem,
            this.NormalToolStripMenuItem,
            this.BelowNormalToolStripMenuItem,
            this.LowToolStripMenuItem});
            this.SetPriorityToolStripMenuItem.Name = "SetPriorityToolStripMenuItem";
            this.SetPriorityToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.SetPriorityToolStripMenuItem.ShowShortcutKeys = false;
            this.SetPriorityToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.SetPriorityToolStripMenuItem.Text = "&Set Priority";
            // 
            // RealtimeToolStripMenuItem
            // 
            this.RealtimeToolStripMenuItem.AutoToolTip = true;
            this.RealtimeToolStripMenuItem.CheckOnClick = true;
            this.RealtimeToolStripMenuItem.Name = "RealtimeToolStripMenuItem";
            this.RealtimeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.RealtimeToolStripMenuItem.ShowShortcutKeys = false;
            this.RealtimeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.RealtimeToolStripMenuItem.Text = "&Realtime";
            this.RealtimeToolStripMenuItem.ToolTipText = "Realtime Processing in OS";
            this.RealtimeToolStripMenuItem.Click += new System.EventHandler(this.RealtimeToolStripMenuItem_Click);
            // 
            // HighToolStripMenuItem
            // 
            this.HighToolStripMenuItem.AutoToolTip = true;
            this.HighToolStripMenuItem.Checked = true;
            this.HighToolStripMenuItem.CheckOnClick = true;
            this.HighToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HighToolStripMenuItem.Name = "HighToolStripMenuItem";
            this.HighToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.HighToolStripMenuItem.ShowShortcutKeys = false;
            this.HighToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.HighToolStripMenuItem.Text = "&High";
            this.HighToolStripMenuItem.ToolTipText = "High Processing in OS";
            this.HighToolStripMenuItem.Click += new System.EventHandler(this.HighToolStripMenuItem_Click);
            // 
            // AboveNormalToolStripMenuItem
            // 
            this.AboveNormalToolStripMenuItem.AutoToolTip = true;
            this.AboveNormalToolStripMenuItem.CheckOnClick = true;
            this.AboveNormalToolStripMenuItem.Name = "AboveNormalToolStripMenuItem";
            this.AboveNormalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.AboveNormalToolStripMenuItem.ShowShortcutKeys = false;
            this.AboveNormalToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.AboveNormalToolStripMenuItem.Text = "&Above Normal";
            this.AboveNormalToolStripMenuItem.ToolTipText = "Above Normal Processing in OS";
            this.AboveNormalToolStripMenuItem.Click += new System.EventHandler(this.AboveNormalToolStripMenuItem_Click);
            // 
            // NormalToolStripMenuItem
            // 
            this.NormalToolStripMenuItem.AutoToolTip = true;
            this.NormalToolStripMenuItem.CheckOnClick = true;
            this.NormalToolStripMenuItem.Name = "NormalToolStripMenuItem";
            this.NormalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NormalToolStripMenuItem.ShowShortcutKeys = false;
            this.NormalToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.NormalToolStripMenuItem.Text = "&Normal";
            this.NormalToolStripMenuItem.ToolTipText = "Normal Processing in OS";
            this.NormalToolStripMenuItem.Click += new System.EventHandler(this.NormalToolStripMenuItem_Click);
            // 
            // BelowNormalToolStripMenuItem
            // 
            this.BelowNormalToolStripMenuItem.AutoToolTip = true;
            this.BelowNormalToolStripMenuItem.CheckOnClick = true;
            this.BelowNormalToolStripMenuItem.Name = "BelowNormalToolStripMenuItem";
            this.BelowNormalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.BelowNormalToolStripMenuItem.ShowShortcutKeys = false;
            this.BelowNormalToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.BelowNormalToolStripMenuItem.Text = "&Below Normal";
            this.BelowNormalToolStripMenuItem.ToolTipText = "Below Normal Processing in OS";
            this.BelowNormalToolStripMenuItem.Click += new System.EventHandler(this.BelowNormalToolStripMenuItem_Click);
            // 
            // LowToolStripMenuItem
            // 
            this.LowToolStripMenuItem.AutoToolTip = true;
            this.LowToolStripMenuItem.CheckOnClick = true;
            this.LowToolStripMenuItem.Name = "LowToolStripMenuItem";
            this.LowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.LowToolStripMenuItem.ShowShortcutKeys = false;
            this.LowToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.LowToolStripMenuItem.Text = "&Low";
            this.LowToolStripMenuItem.ToolTipText = "Low Processing in OS";
            this.LowToolStripMenuItem.Click += new System.EventHandler(this.LowToolStripMenuItem_Click);
            // 
            // SetAffinityToolStripMenuItem
            // 
            this.SetAffinityToolStripMenuItem.Name = "SetAffinityToolStripMenuItem";
            this.SetAffinityToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.SetAffinityToolStripMenuItem.ShowShortcutKeys = false;
            this.SetAffinityToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.SetAffinityToolStripMenuItem.Text = "Set &Affinity";
            this.SetAffinityToolStripMenuItem.Click += new System.EventHandler(this.SetAffinityToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(173, 6);
            // 
            // pGAToolStripMenuItem
            // 
            this.pGAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskParallelismToolStripMenuItem,
            this.threadParallelismToolStripMenuItem,
            this.parallelForToolStripMenuItem});
            this.pGAToolStripMenuItem.Name = "pGAToolStripMenuItem";
            this.pGAToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.pGAToolStripMenuItem.Text = "&Parallel Computing";
            this.pGAToolStripMenuItem.Click += new System.EventHandler(this.pGAToolStripMenuItem_Click);
            // 
            // taskParallelismToolStripMenuItem
            // 
            this.taskParallelismToolStripMenuItem.Name = "taskParallelismToolStripMenuItem";
            this.taskParallelismToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.taskParallelismToolStripMenuItem.Text = "Task Parallelism (.Net 4)";
            this.taskParallelismToolStripMenuItem.Click += new System.EventHandler(this.taskParallelismToolStripMenuItem_Click);
            // 
            // threadParallelismToolStripMenuItem
            // 
            this.threadParallelismToolStripMenuItem.Name = "threadParallelismToolStripMenuItem";
            this.threadParallelismToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.threadParallelismToolStripMenuItem.Text = "Thread Parallelism (Semaphore)";
            this.threadParallelismToolStripMenuItem.Click += new System.EventHandler(this.threadParallelismToolStripMenuItem_Click);
            // 
            // parallelForToolStripMenuItem
            // 
            this.parallelForToolStripMenuItem.Name = "parallelForToolStripMenuItem";
            this.parallelForToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.parallelForToolStripMenuItem.Text = "Paralell.For (.Net 4)";
            this.parallelForToolStripMenuItem.Click += new System.EventHandler(this.parallelForToolStripMenuItem_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnStartStop.AutoSize = true;
            this.btnStartStop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnStartStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnStartStop.Location = new System.Drawing.Point(12, 35);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(98, 26);
            this.btnStartStop.TabIndex = 15;
            this.btnStartStop.Text = "&Start Process";
            this.btnStartStop.UseVisualStyleBackColor = false;
            this.btnStartStop.CheckedChanged += new System.EventHandler(this.btnStartStop_CheckedChanged);
            // 
            // btnPauseResume
            // 
            this.btnPauseResume.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPauseResume.AutoSize = true;
            this.btnPauseResume.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnPauseResume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPauseResume.Enabled = false;
            this.btnPauseResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnPauseResume.Location = new System.Drawing.Point(155, 35);
            this.btnPauseResume.Name = "btnPauseResume";
            this.btnPauseResume.Size = new System.Drawing.Size(110, 26);
            this.btnPauseResume.TabIndex = 15;
            this.btnPauseResume.Text = "&Pause Process";
            this.btnPauseResume.UseVisualStyleBackColor = false;
            this.btnPauseResume.CheckedChanged += new System.EventHandler(this.btnPauseResume_CheckedChanged);
            // 
            // timerGenerationGraphToolStripMenuItem
            // 
            this.timerGenerationGraphToolStripMenuItem.Name = "timerGenerationGraphToolStripMenuItem";
            this.timerGenerationGraphToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F6)));
            this.timerGenerationGraphToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.timerGenerationGraphToolStripMenuItem.Text = "Timer Generation Graph";
            this.timerGenerationGraphToolStripMenuItem.Click += new System.EventHandler(this.timerGenerationGraphToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(245, 6);
            // 
            // generationFitnessGraphToolStripMenuItem
            // 
            this.generationFitnessGraphToolStripMenuItem.Name = "generationFitnessGraphToolStripMenuItem";
            this.generationFitnessGraphToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F7)));
            this.generationFitnessGraphToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.generationFitnessGraphToolStripMenuItem.Text = "Generation Fitness Graph";
            this.generationFitnessGraphToolStripMenuItem.Click += new System.EventHandler(this.generationFitnessGraphToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(794, 532);
            this.Controls.Add(this.btnPauseResume);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblGeneration);
            this.Controls.Add(this.lblLenght);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numPopulation);
            this.Controls.Add(this.dgvCity);
            this.Controls.Add(this.statusStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(810, 300);
            this.Name = "MainForm";
            this.Text = "Travelling Sales man Problem";
            this.toolTip1.SetToolTip(this, "Click to create a city place");
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.numPopulation)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCity)).EndInit();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatuslblNumCity0;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatuslblNumCity;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLenght;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatuslblLocate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblGeneration;
        private System.Windows.Forms.DataGridView dgvCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCP;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.NumericUpDown numPopulation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newRandomCitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timerFitnessGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProcessPriorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetPriorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RealtimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HighToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboveNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BelowNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetAffinityToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem dynamicalGraphicToolStripMenuItem;
        private System.Windows.Forms.CheckBox btnStartStop;
        private System.Windows.Forms.CheckBox btnPauseResume;
        private System.Windows.Forms.ToolStripMenuItem pGAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem taskParallelismToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadParallelismToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parallelForToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timerGenerationGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generationFitnessGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}