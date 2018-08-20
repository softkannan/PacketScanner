namespace PacketScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.startStopBttn = new System.Windows.Forms.Button();
            this.adapterComboBox = new System.Windows.Forms.ComboBox();
            this.showIPTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DGV = new BrightIdeasSoftware.FastObjectListView();
            this.srcColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.srcPortColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.destColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.destPortColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.typeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.flagsColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.seqNumberColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ackNumberColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.dataColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.listViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBySourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupByDestinationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBySourcePortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupByDestinationPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupByTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBySeqNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupByAckNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayBufferNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.clearBttn = new System.Windows.Forms.Button();
            this.protocolTypeCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.listViewContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayBufferNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // startStopBttn
            // 
            this.startStopBttn.Location = new System.Drawing.Point(437, 4);
            this.startStopBttn.Name = "startStopBttn";
            this.startStopBttn.Size = new System.Drawing.Size(75, 23);
            this.startStopBttn.TabIndex = 0;
            this.startStopBttn.Text = "Start";
            this.startStopBttn.UseVisualStyleBackColor = true;
            // 
            // adapterComboBox
            // 
            this.adapterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.adapterComboBox.FormattingEnabled = true;
            this.adapterComboBox.Location = new System.Drawing.Point(101, 6);
            this.adapterComboBox.Name = "adapterComboBox";
            this.adapterComboBox.Size = new System.Drawing.Size(330, 21);
            this.adapterComboBox.TabIndex = 1;
            // 
            // showIPTextBox
            // 
            this.showIPTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showIPTextBox.Location = new System.Drawing.Point(1142, 4);
            this.showIPTextBox.Name = "showIPTextBox";
            this.showIPTextBox.Size = new System.Drawing.Size(240, 20);
            this.showIPTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1059, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Show Only IP :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Adapter :";
            // 
            // DGV
            // 
            this.DGV.AllColumns.Add(this.srcColumn);
            this.DGV.AllColumns.Add(this.srcPortColumn);
            this.DGV.AllColumns.Add(this.destColumn);
            this.DGV.AllColumns.Add(this.destPortColumn);
            this.DGV.AllColumns.Add(this.typeColumn);
            this.DGV.AllColumns.Add(this.flagsColumn);
            this.DGV.AllColumns.Add(this.seqNumberColumn);
            this.DGV.AllColumns.Add(this.ackNumberColumn);
            this.DGV.AllColumns.Add(this.dataColumn);
            this.DGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV.CellEditUseWholeCell = false;
            this.DGV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.srcColumn,
            this.srcPortColumn,
            this.destColumn,
            this.destPortColumn,
            this.typeColumn,
            this.flagsColumn,
            this.seqNumberColumn,
            this.ackNumberColumn,
            this.dataColumn});
            this.DGV.ContextMenuStrip = this.listViewContextMenu;
            this.DGV.Cursor = System.Windows.Forms.Cursors.Default;
            this.DGV.FullRowSelect = true;
            this.DGV.Location = new System.Drawing.Point(15, 33);
            this.DGV.Name = "DGV";
            this.DGV.ShowGroups = false;
            this.DGV.Size = new System.Drawing.Size(1367, 702);
            this.DGV.TabIndex = 6;
            this.DGV.UseAlternatingBackColors = true;
            this.DGV.UseCompatibleStateImageBehavior = false;
            this.DGV.UseExplorerTheme = true;
            this.DGV.UseFilterIndicator = true;
            this.DGV.UseFiltering = true;
            this.DGV.UseHotItem = true;
            this.DGV.View = System.Windows.Forms.View.Details;
            this.DGV.VirtualMode = true;
            // 
            // srcColumn
            // 
            this.srcColumn.AspectName = "Source";
            this.srcColumn.IsEditable = false;
            this.srcColumn.Text = "Source";
            this.srcColumn.Width = 140;
            this.srcColumn.WordWrap = true;
            // 
            // srcPortColumn
            // 
            this.srcPortColumn.AspectName = "SourcePort";
            this.srcPortColumn.IsEditable = false;
            this.srcPortColumn.Text = "SourcePort";
            this.srcPortColumn.Width = 80;
            // 
            // destColumn
            // 
            this.destColumn.AspectName = "Destination";
            this.destColumn.IsEditable = false;
            this.destColumn.Text = "Destination";
            this.destColumn.Width = 140;
            // 
            // destPortColumn
            // 
            this.destPortColumn.AspectName = "DestinationPort";
            this.destPortColumn.IsEditable = false;
            this.destPortColumn.Text = "DestinationPort";
            this.destPortColumn.Width = 90;
            // 
            // typeColumn
            // 
            this.typeColumn.AspectName = "Type";
            this.typeColumn.IsEditable = false;
            this.typeColumn.Text = "Type";
            this.typeColumn.Width = 50;
            // 
            // flagsColumn
            // 
            this.flagsColumn.AspectName = "Flags";
            this.flagsColumn.IsEditable = false;
            this.flagsColumn.Text = "Flags";
            this.flagsColumn.Width = 160;
            // 
            // seqNumberColumn
            // 
            this.seqNumberColumn.AspectName = "SeqNumber";
            this.seqNumberColumn.IsEditable = false;
            this.seqNumberColumn.Text = "SeqNumber";
            this.seqNumberColumn.Width = 100;
            // 
            // ackNumberColumn
            // 
            this.ackNumberColumn.AspectName = "AckNumber";
            this.ackNumberColumn.IsEditable = false;
            this.ackNumberColumn.Text = "AckNumber";
            this.ackNumberColumn.Width = 80;
            // 
            // dataColumn
            // 
            this.dataColumn.AspectName = "Data";
            this.dataColumn.Groupable = false;
            this.dataColumn.IsEditable = false;
            this.dataColumn.Sortable = false;
            this.dataColumn.Text = "Data";
            this.dataColumn.Width = 500;
            // 
            // listViewContextMenu
            // 
            this.listViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.clearGroupToolStripMenuItem,
            this.groupBySourceToolStripMenuItem,
            this.groupByDestinationToolStripMenuItem,
            this.groupBySourcePortToolStripMenuItem,
            this.groupByDestinationPortToolStripMenuItem,
            this.groupByTypeToolStripMenuItem,
            this.groupBySeqNumberToolStripMenuItem,
            this.groupByAckNumberToolStripMenuItem});
            this.listViewContextMenu.Name = "listViewContextMenu";
            this.listViewContextMenu.Size = new System.Drawing.Size(212, 202);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // clearGroupToolStripMenuItem
            // 
            this.clearGroupToolStripMenuItem.Name = "clearGroupToolStripMenuItem";
            this.clearGroupToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.clearGroupToolStripMenuItem.Text = "Clear Group";
            this.clearGroupToolStripMenuItem.Click += new System.EventHandler(this.clearGroupToolStripMenuItem_Click);
            // 
            // groupBySourceToolStripMenuItem
            // 
            this.groupBySourceToolStripMenuItem.Name = "groupBySourceToolStripMenuItem";
            this.groupBySourceToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.groupBySourceToolStripMenuItem.Text = "Group By Source";
            this.groupBySourceToolStripMenuItem.Click += new System.EventHandler(this.groupBySourceToolStripMenuItem_Click);
            // 
            // groupByDestinationToolStripMenuItem
            // 
            this.groupByDestinationToolStripMenuItem.Name = "groupByDestinationToolStripMenuItem";
            this.groupByDestinationToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.groupByDestinationToolStripMenuItem.Text = "Group By Destination";
            this.groupByDestinationToolStripMenuItem.Click += new System.EventHandler(this.groupByDestinationToolStripMenuItem_Click);
            // 
            // groupBySourcePortToolStripMenuItem
            // 
            this.groupBySourcePortToolStripMenuItem.Name = "groupBySourcePortToolStripMenuItem";
            this.groupBySourcePortToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.groupBySourcePortToolStripMenuItem.Text = "Group By Source Port";
            this.groupBySourcePortToolStripMenuItem.Click += new System.EventHandler(this.groupBySourcePortToolStripMenuItem_Click);
            // 
            // groupByDestinationPortToolStripMenuItem
            // 
            this.groupByDestinationPortToolStripMenuItem.Name = "groupByDestinationPortToolStripMenuItem";
            this.groupByDestinationPortToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.groupByDestinationPortToolStripMenuItem.Text = "Group By Destination Port";
            this.groupByDestinationPortToolStripMenuItem.Click += new System.EventHandler(this.groupByDestinationPortToolStripMenuItem_Click);
            // 
            // groupByTypeToolStripMenuItem
            // 
            this.groupByTypeToolStripMenuItem.Name = "groupByTypeToolStripMenuItem";
            this.groupByTypeToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.groupByTypeToolStripMenuItem.Text = "Group By Type";
            this.groupByTypeToolStripMenuItem.Click += new System.EventHandler(this.groupByTypeToolStripMenuItem_Click);
            // 
            // groupBySeqNumberToolStripMenuItem
            // 
            this.groupBySeqNumberToolStripMenuItem.Name = "groupBySeqNumberToolStripMenuItem";
            this.groupBySeqNumberToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.groupBySeqNumberToolStripMenuItem.Text = "Group By SeqNumber";
            this.groupBySeqNumberToolStripMenuItem.Click += new System.EventHandler(this.groupBySeqNumberToolStripMenuItem_Click);
            // 
            // groupByAckNumberToolStripMenuItem
            // 
            this.groupByAckNumberToolStripMenuItem.Name = "groupByAckNumberToolStripMenuItem";
            this.groupByAckNumberToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.groupByAckNumberToolStripMenuItem.Text = "Group By AckNumber";
            this.groupByAckNumberToolStripMenuItem.Click += new System.EventHandler(this.groupByAckNumberToolStripMenuItem_Click);
            // 
            // displayBufferNumeric
            // 
            this.displayBufferNumeric.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.displayBufferNumeric.Location = new System.Drawing.Point(989, 5);
            this.displayBufferNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.displayBufferNumeric.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.displayBufferNumeric.Name = "displayBufferNumeric";
            this.displayBufferNumeric.Size = new System.Drawing.Size(64, 20);
            this.displayBufferNumeric.TabIndex = 7;
            this.displayBufferNumeric.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.displayBufferNumeric.ValueChanged += new System.EventHandler(this.displayBufferNumeric_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(905, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Display Buffer :";
            // 
            // clearBttn
            // 
            this.clearBttn.Location = new System.Drawing.Point(518, 4);
            this.clearBttn.Name = "clearBttn";
            this.clearBttn.Size = new System.Drawing.Size(75, 23);
            this.clearBttn.TabIndex = 9;
            this.clearBttn.Text = "Clear";
            this.clearBttn.UseVisualStyleBackColor = true;
            this.clearBttn.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // protocolTypeCombo
            // 
            this.protocolTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolTypeCombo.FormattingEnabled = true;
            this.protocolTypeCombo.Items.AddRange(new object[] {
            "AllThisAdapter",
            "UDPThisAdapter",
            "TCPThisAdapter",
            "All",
            "UDP",
            "TCP"});
            this.protocolTypeCombo.Location = new System.Drawing.Point(599, 5);
            this.protocolTypeCombo.Name = "protocolTypeCombo";
            this.protocolTypeCombo.Size = new System.Drawing.Size(180, 21);
            this.protocolTypeCombo.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 747);
            this.Controls.Add(this.protocolTypeCombo);
            this.Controls.Add(this.clearBttn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.displayBufferNumeric);
            this.Controls.Add(this.DGV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showIPTextBox);
            this.Controls.Add(this.adapterComboBox);
            this.Controls.Add(this.startStopBttn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Packet Scanner";
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.listViewContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.displayBufferNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startStopBttn;
        private System.Windows.Forms.ComboBox adapterComboBox;
        private System.Windows.Forms.TextBox showIPTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private BrightIdeasSoftware.FastObjectListView DGV;
        private BrightIdeasSoftware.OLVColumn srcColumn;
        private BrightIdeasSoftware.OLVColumn destColumn;
        private BrightIdeasSoftware.OLVColumn typeColumn;
        private BrightIdeasSoftware.OLVColumn dataColumn;
        private BrightIdeasSoftware.OLVColumn srcPortColumn;
        private BrightIdeasSoftware.OLVColumn destPortColumn;
        private BrightIdeasSoftware.OLVColumn flagsColumn;
        private BrightIdeasSoftware.OLVColumn seqNumberColumn;
        private BrightIdeasSoftware.OLVColumn ackNumberColumn;
        private System.Windows.Forms.NumericUpDown displayBufferNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button clearBttn;
        private System.Windows.Forms.ComboBox protocolTypeCombo;
        private System.Windows.Forms.ContextMenuStrip listViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupBySourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupByDestinationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupBySourcePortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupByDestinationPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupByTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupBySeqNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupByAckNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearGroupToolStripMenuItem;
    }
}

