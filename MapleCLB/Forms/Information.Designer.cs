namespace MapleCLB.Forms
{
    partial class Information
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.etcTab = new System.Windows.Forms.TabPage();
            this.EtcListView = new System.Windows.Forms.ListView();
            this.ItemName_Etc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantity_Etc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.setupTab = new System.Windows.Forms.TabPage();
            this.SetupListView = new System.Windows.Forms.ListView();
            this.ItemName_SetUp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantity_SetUp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.useTab = new System.Windows.Forms.TabPage();
            this.UseListView = new System.Windows.Forms.ListView();
            this.ItemName_Use = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantity_Use = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.equipTab = new System.Windows.Forms.TabPage();
            this.EquipListView = new System.Windows.Forms.ListView();
            this.ItemName_Equip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantity_Equip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1.SuspendLayout();
            this.etcTab.SuspendLayout();
            this.setupTab.SuspendLayout();
            this.useTab.SuspendLayout();
            this.equipTab.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dropToolStripMenuItem,
            this.tradeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 48);
            // 
            // dropToolStripMenuItem
            // 
            this.dropToolStripMenuItem.Name = "dropToolStripMenuItem";
            this.dropToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.dropToolStripMenuItem.Text = "Drop";
            // 
            // tradeToolStripMenuItem
            // 
            this.tradeToolStripMenuItem.Name = "tradeToolStripMenuItem";
            this.tradeToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.tradeToolStripMenuItem.Text = "Trade";
            // 
            // etcTab
            // 
            this.etcTab.Controls.Add(this.EtcListView);
            this.etcTab.Location = new System.Drawing.Point(4, 22);
            this.etcTab.Name = "etcTab";
            this.etcTab.Padding = new System.Windows.Forms.Padding(3);
            this.etcTab.Size = new System.Drawing.Size(234, 266);
            this.etcTab.TabIndex = 3;
            this.etcTab.Text = "      Etc      ";
            this.etcTab.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.EtcListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemName_Etc,
            this.Quantity_Etc});
            this.EtcListView.FullRowSelect = true;
            this.EtcListView.GridLines = true;
            this.EtcListView.Location = new System.Drawing.Point(-4, 0);
            this.EtcListView.Name = "listView1";
            this.EtcListView.Size = new System.Drawing.Size(238, 266);
            this.EtcListView.TabIndex = 0;
            this.EtcListView.UseCompatibleStateImageBehavior = false;
            this.EtcListView.View = System.Windows.Forms.View.Details;
            // 
            // ItemName_Etc
            // 
            this.ItemName_Etc.Text = "Item Name";
            this.ItemName_Etc.Width = 180;
            // 
            // Quantity_Etc
            // 
            this.Quantity_Etc.Text = "Quantity";
            this.Quantity_Etc.Width = 54;
            // 
            // setupTab
            // 
            this.setupTab.Controls.Add(this.SetupListView);
            this.setupTab.Location = new System.Drawing.Point(4, 22);
            this.setupTab.Name = "setupTab";
            this.setupTab.Padding = new System.Windows.Forms.Padding(3);
            this.setupTab.Size = new System.Drawing.Size(234, 266);
            this.setupTab.TabIndex = 2;
            this.setupTab.Text = "   SetUp   ";
            this.setupTab.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.SetupListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemName_SetUp,
            this.Quantity_SetUp});
            this.SetupListView.GridLines = true;
            this.SetupListView.Location = new System.Drawing.Point(-4, 0);
            this.SetupListView.Name = "listView2";
            this.SetupListView.Size = new System.Drawing.Size(238, 266);
            this.SetupListView.TabIndex = 0;
            this.SetupListView.UseCompatibleStateImageBehavior = false;
            this.SetupListView.View = System.Windows.Forms.View.Details;
            // 
            // ItemName_SetUp
            // 
            this.ItemName_SetUp.Text = "Item Name";
            this.ItemName_SetUp.Width = 180;
            // 
            // Quantity_SetUp
            // 
            this.Quantity_SetUp.Text = "Quantity";
            this.Quantity_SetUp.Width = 54;
            // 
            // useTab
            // 
            this.useTab.Controls.Add(this.UseListView);
            this.useTab.Location = new System.Drawing.Point(4, 22);
            this.useTab.Name = "useTab";
            this.useTab.Padding = new System.Windows.Forms.Padding(3);
            this.useTab.Size = new System.Drawing.Size(234, 266);
            this.useTab.TabIndex = 1;
            this.useTab.Text = "    Use    ";
            this.useTab.UseVisualStyleBackColor = true;
            // 
            // UseTab listView3
            // 
            this.UseListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemName_Use,
            this.Quantity_Use});
            this.UseListView.GridLines = true;
            this.UseListView.Location = new System.Drawing.Point(-4, 0);
            this.UseListView.Name = "listView3";
            this.UseListView.Size = new System.Drawing.Size(238, 266);
            this.UseListView.TabIndex = 0;
            this.UseListView.UseCompatibleStateImageBehavior = false;
            this.UseListView.View = System.Windows.Forms.View.Details;
            // 
            // ItemName_Use
            // 
            this.ItemName_Use.Text = "Item Name";
            this.ItemName_Use.Width = 180;
            // 
            // Quantity_Use
            // 
            this.Quantity_Use.Text = "Quantity";
            this.Quantity_Use.Width = 54;
            // 
            // equipTab
            // 
            this.equipTab.Controls.Add(this.EquipListView);
            this.equipTab.Location = new System.Drawing.Point(4, 22);
            this.equipTab.Name = "equipTab";
            this.equipTab.Padding = new System.Windows.Forms.Padding(3);
            this.equipTab.Size = new System.Drawing.Size(234, 266);
            this.equipTab.TabIndex = 0;
            this.equipTab.Text = "   Equip   ";
            this.equipTab.UseVisualStyleBackColor = true;
            // 
            // EquipTab listview4
            // 
            this.EquipListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemName_Equip,
            this.Quantity_Equip});
            this.EquipListView.GridLines = true;
            this.EquipListView.Location = new System.Drawing.Point(-4, 0);
            this.EquipListView.Name = "listView4";
            this.EquipListView.Size = new System.Drawing.Size(238, 263);
            this.EquipListView.TabIndex = 0;
            this.EquipListView.UseCompatibleStateImageBehavior = false;
            this.EquipListView.View = System.Windows.Forms.View.Details;
            // 
            // ItemName_Equip
            // 
            this.ItemName_Equip.Text = "Item Name";
            this.ItemName_Equip.Width = 180;
            // 
            // Quantity_Equip
            // 
            this.Quantity_Equip.Text = "Quantity";
            this.Quantity_Equip.Width = 54;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.equipTab);
            this.tabControl1.Controls.Add(this.useTab);
            this.tabControl1.Controls.Add(this.setupTab);
            this.tabControl1.Controls.Add(this.etcTab);
            this.tabControl1.Location = new System.Drawing.Point(-2, -3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(242, 292);
            this.tabControl1.TabIndex = 1;
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 280);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Information";
            this.Text = "Information";
            this.contextMenuStrip1.ResumeLayout(false);
            this.etcTab.ResumeLayout(false);
            this.setupTab.ResumeLayout(false);
            this.useTab.ResumeLayout(false);
            this.equipTab.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.TopMost = true;
            this.ControlBox = false;
        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tradeToolStripMenuItem;
        private System.Windows.Forms.TabPage etcTab;
        private System.Windows.Forms.ListView EtcListView;
        private System.Windows.Forms.TabPage setupTab;
        private System.Windows.Forms.ListView SetupListView;
        private System.Windows.Forms.TabPage useTab;
        private System.Windows.Forms.ListView UseListView;
        private System.Windows.Forms.TabPage equipTab;
        private System.Windows.Forms.ListView EquipListView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ColumnHeader ItemName_Equip;
        private System.Windows.Forms.ColumnHeader Quantity_Equip;
        private System.Windows.Forms.ColumnHeader ItemName_Use;
        private System.Windows.Forms.ColumnHeader Quantity_Use;
        private System.Windows.Forms.ColumnHeader ItemName_Etc;
        private System.Windows.Forms.ColumnHeader Quantity_Etc;
        private System.Windows.Forms.ColumnHeader ItemName_SetUp;
        private System.Windows.Forms.ColumnHeader Quantity_SetUp;
    }
}