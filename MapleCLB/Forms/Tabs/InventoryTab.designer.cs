using System.Windows.Forms;

namespace MapleCLB.Forms
{
    partial class InventoryTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.InventoryTabs = new System.Windows.Forms.TabControl();
            this.EquippedTab = new System.Windows.Forms.TabPage();
            this.EquippedListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dropAllTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EquipsTab = new System.Windows.Forms.TabPage();
            this.EquipsListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UseTab = new System.Windows.Forms.TabPage();
            this.UseListView = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SetUpTab = new System.Windows.Forms.TabPage();
            this.SetUpListView = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EtcTab = new System.Windows.Forms.TabPage();
            this.EtcListView = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CashTab = new System.Windows.Forms.TabPage();
            this.CashListView = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dropInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InventoryTabs.SuspendLayout();
            this.EquippedTab.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.EquipsTab.SuspendLayout();
            this.UseTab.SuspendLayout();
            this.SetUpTab.SuspendLayout();
            this.EtcTab.SuspendLayout();
            this.CashTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // InventoryTabs
            // 
            this.InventoryTabs.Controls.Add(this.EquippedTab);
            this.InventoryTabs.Controls.Add(this.EquipsTab);
            this.InventoryTabs.Controls.Add(this.UseTab);
            this.InventoryTabs.Controls.Add(this.SetUpTab);
            this.InventoryTabs.Controls.Add(this.EtcTab);
            this.InventoryTabs.Controls.Add(this.CashTab);
            this.InventoryTabs.Location = new System.Drawing.Point(0, 0);
            this.InventoryTabs.Name = "InventoryTabs";
            this.InventoryTabs.Padding = new System.Drawing.Point(8, 3);
            this.InventoryTabs.SelectedIndex = 0;
            this.InventoryTabs.Size = new System.Drawing.Size(321, 285);
            this.InventoryTabs.TabIndex = 0;
            // 
            // EquippedTab
            // 
            this.EquippedTab.Controls.Add(this.EquippedListView);
            this.EquippedTab.Location = new System.Drawing.Point(4, 22);
            this.EquippedTab.Name = "EquippedTab";
            this.EquippedTab.Padding = new System.Windows.Forms.Padding(3);
            this.EquippedTab.Size = new System.Drawing.Size(313, 259);
            this.EquippedTab.TabIndex = 0;
            this.EquippedTab.Text = "Equipped";
            this.EquippedTab.UseVisualStyleBackColor = true;
            // 
            // EquippedListView
            // 
            this.EquippedListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.EquippedListView.ContextMenuStrip = this.contextMenuStrip1;
            this.EquippedListView.GridLines = true;
            this.EquippedListView.Location = new System.Drawing.Point(-4, 0);
            this.EquippedListView.MultiSelect = false;
            this.EquippedListView.Name = "EquippedListView";
            this.EquippedListView.Size = new System.Drawing.Size(321, 263);
            this.EquippedListView.TabIndex = 0;
            this.EquippedListView.UseCompatibleStateImageBehavior = false;
            this.EquippedListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item Name";
            this.columnHeader1.Width = 255;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Quantity";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dropToolStripMenuItem,
            this.tradeToolStripMenuItem,
            this.dropAllTabToolStripMenuItem,
            this.dropInventoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 114);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // dropToolStripMenuItem
            // 
            this.dropToolStripMenuItem.Name = "dropToolStripMenuItem";
            this.dropToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.dropToolStripMenuItem.Text = "Drop";
            // 
            // tradeToolStripMenuItem
            // 
            this.tradeToolStripMenuItem.Name = "tradeToolStripMenuItem";
            this.tradeToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.tradeToolStripMenuItem.Text = "Trade";
            // 
            // dropAllTabToolStripMenuItem
            // 
            this.dropAllTabToolStripMenuItem.Name = "dropAllTabToolStripMenuItem";
            this.dropAllTabToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.dropAllTabToolStripMenuItem.Text = "Drop All Tab";
            // 
            // EquipsTab
            // 
            this.EquipsTab.Controls.Add(this.EquipsListView);
            this.EquipsTab.Location = new System.Drawing.Point(4, 22);
            this.EquipsTab.Name = "EquipsTab";
            this.EquipsTab.Padding = new System.Windows.Forms.Padding(3);
            this.EquipsTab.Size = new System.Drawing.Size(313, 259);
            this.EquipsTab.TabIndex = 1;
            this.EquipsTab.Text = " Equips ";
            this.EquipsTab.UseVisualStyleBackColor = true;
            // 
            // EquipsListView
            // 
            this.EquipsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.EquipsListView.GridLines = true;
            this.EquipsListView.Location = new System.Drawing.Point(-4, 0);
            this.EquipsListView.MultiSelect = false;
            this.EquipsListView.Name = "EquipsListView";
            this.EquipsListView.Size = new System.Drawing.Size(321, 263);
            this.EquipsListView.TabIndex = 1;
            this.EquipsListView.UseCompatibleStateImageBehavior = false;
            this.EquipsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item Name";
            this.columnHeader3.Width = 255;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Quantity";
            // 
            // UseTab
            // 
            this.UseTab.Controls.Add(this.UseListView);
            this.UseTab.Location = new System.Drawing.Point(4, 22);
            this.UseTab.Name = "UseTab";
            this.UseTab.Padding = new System.Windows.Forms.Padding(3);
            this.UseTab.Size = new System.Drawing.Size(313, 259);
            this.UseTab.TabIndex = 2;
            this.UseTab.Text = "  Use   ";
            this.UseTab.UseVisualStyleBackColor = true;
            // 
            // UseListView
            // 
            this.UseListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.UseListView.GridLines = true;
            this.UseListView.Location = new System.Drawing.Point(-4, 0);
            this.UseListView.MultiSelect = false;
            this.UseListView.Name = "UseListView";
            this.UseListView.Size = new System.Drawing.Size(321, 263);
            this.UseListView.TabIndex = 1;
            this.UseListView.UseCompatibleStateImageBehavior = false;
            this.UseListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Item Name";
            this.columnHeader5.Width = 255;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Quantity";
            // 
            // SetUpTab
            // 
            this.SetUpTab.Controls.Add(this.SetUpListView);
            this.SetUpTab.Location = new System.Drawing.Point(4, 22);
            this.SetUpTab.Name = "SetUpTab";
            this.SetUpTab.Size = new System.Drawing.Size(313, 259);
            this.SetUpTab.TabIndex = 3;
            this.SetUpTab.Text = "Set Up";
            this.SetUpTab.UseVisualStyleBackColor = true;
            // 
            // SetUpListView
            // 
            this.SetUpListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.SetUpListView.GridLines = true;
            this.SetUpListView.Location = new System.Drawing.Point(-4, 0);
            this.SetUpListView.MultiSelect = false;
            this.SetUpListView.Name = "SetUpListView";
            this.SetUpListView.Size = new System.Drawing.Size(321, 263);
            this.SetUpListView.TabIndex = 1;
            this.SetUpListView.UseCompatibleStateImageBehavior = false;
            this.SetUpListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Item Name";
            this.columnHeader7.Width = 255;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Quantity";
            // 
            // EtcTab
            // 
            this.EtcTab.Controls.Add(this.EtcListView);
            this.EtcTab.Location = new System.Drawing.Point(4, 22);
            this.EtcTab.Name = "EtcTab";
            this.EtcTab.Size = new System.Drawing.Size(313, 259);
            this.EtcTab.TabIndex = 4;
            this.EtcTab.Text = "   Etc  ";
            this.EtcTab.UseVisualStyleBackColor = true;
            // 
            // EtcListView
            // 
            this.EtcListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.EtcListView.GridLines = true;
            this.EtcListView.Location = new System.Drawing.Point(-4, 0);
            this.EtcListView.MultiSelect = false;
            this.EtcListView.Name = "EtcListView";
            this.EtcListView.Size = new System.Drawing.Size(321, 263);
            this.EtcListView.TabIndex = 1;
            this.EtcListView.UseCompatibleStateImageBehavior = false;
            this.EtcListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Item Name";
            this.columnHeader9.Width = 255;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Quantity";
            // 
            // CashTab
            // 
            this.CashTab.Controls.Add(this.CashListView);
            this.CashTab.Location = new System.Drawing.Point(4, 22);
            this.CashTab.Name = "CashTab";
            this.CashTab.Size = new System.Drawing.Size(313, 259);
            this.CashTab.TabIndex = 5;
            this.CashTab.Text = "  Cash  ";
            this.CashTab.UseVisualStyleBackColor = true;
            // 
            // CashListView
            // 
            this.CashListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12});
            this.CashListView.GridLines = true;
            this.CashListView.Location = new System.Drawing.Point(-4, 0);
            this.CashListView.MultiSelect = false;
            this.CashListView.Name = "CashListView";
            this.CashListView.Size = new System.Drawing.Size(321, 263);
            this.CashListView.TabIndex = 1;
            this.CashListView.UseCompatibleStateImageBehavior = false;
            this.CashListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Item Name";
            this.columnHeader11.Width = 255;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Quantity";
            // 
            // dropInventoryToolStripMenuItem
            // 
            this.dropInventoryToolStripMenuItem.Name = "dropInventoryToolStripMenuItem";
            this.dropInventoryToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.dropInventoryToolStripMenuItem.Text = "Drop Inventory";
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InventoryTabs);
            this.Name = "InventoryForm";
            this.Size = new System.Drawing.Size(324, 285);
            this.InventoryTabs.ResumeLayout(false);
            this.EquippedTab.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.EquipsTab.ResumeLayout(false);
            this.UseTab.ResumeLayout(false);
            this.SetUpTab.ResumeLayout(false);
            this.EtcTab.ResumeLayout(false);
            this.CashTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl InventoryTabs;
        private System.Windows.Forms.TabPage EquippedTab;
        private System.Windows.Forms.TabPage EquipsTab;
        private System.Windows.Forms.TabPage UseTab;
        private System.Windows.Forms.TabPage SetUpTab;
        private System.Windows.Forms.TabPage EtcTab;
        private System.Windows.Forms.TabPage CashTab;
        private System.Windows.Forms.ListView EquippedListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private ListView EquipsListView;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ListView UseListView;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ListView SetUpListView;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ListView EtcListView;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ListView CashListView;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem dropToolStripMenuItem;
        private ToolStripMenuItem tradeToolStripMenuItem;
        private ToolStripMenuItem dropAllTabToolStripMenuItem;
        private ToolStripMenuItem dropInventoryToolStripMenuItem;
    }
}
