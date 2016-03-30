namespace MapleCLB.Forms
{
    partial class Inventory
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
            this.PacketTabs = new System.Windows.Forms.TabControl();
            this.EquippedTab = new System.Windows.Forms.TabPage();
            this.EquipTab = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UseTab = new System.Windows.Forms.TabPage();
            this.SetUpTab = new System.Windows.Forms.TabPage();
            this.EtcTab = new System.Windows.Forms.TabPage();
            this.CashTab = new System.Windows.Forms.TabPage();
            this.listView6 = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView4 = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView5 = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PacketTabs.SuspendLayout();
            this.EquippedTab.SuspendLayout();
            this.EquipTab.SuspendLayout();
            this.UseTab.SuspendLayout();
            this.SetUpTab.SuspendLayout();
            this.EtcTab.SuspendLayout();
            this.CashTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // PacketTabs
            // 
            this.PacketTabs.Controls.Add(this.EquippedTab);
            this.PacketTabs.Controls.Add(this.EquipTab);
            this.PacketTabs.Controls.Add(this.UseTab);
            this.PacketTabs.Controls.Add(this.SetUpTab);
            this.PacketTabs.Controls.Add(this.EtcTab);
            this.PacketTabs.Controls.Add(this.CashTab);
            this.PacketTabs.Location = new System.Drawing.Point(0, 9);
            this.PacketTabs.Margin = new System.Windows.Forms.Padding(0);
            this.PacketTabs.Name = "PacketTabs";
            this.PacketTabs.Padding = new System.Drawing.Point(10, 3);
            this.PacketTabs.SelectedIndex = 0;
            this.PacketTabs.Size = new System.Drawing.Size(620, 309);
            this.PacketTabs.TabIndex = 4;
            // 
            // EquippedTab
            // 
            this.EquippedTab.Controls.Add(this.listView6);
            this.EquippedTab.Location = new System.Drawing.Point(4, 22);
            this.EquippedTab.Name = "EquippedTab";
            this.EquippedTab.Padding = new System.Windows.Forms.Padding(3);
            this.EquippedTab.Size = new System.Drawing.Size(612, 283);
            this.EquippedTab.TabIndex = 0;
            this.EquippedTab.Text = "Equipped";
            this.EquippedTab.UseVisualStyleBackColor = true;
            // 
            // EquipTab
            // 
            this.EquipTab.Controls.Add(this.listView1);
            this.EquipTab.Location = new System.Drawing.Point(4, 22);
            this.EquipTab.Name = "EquipTab";
            this.EquipTab.Padding = new System.Windows.Forms.Padding(3);
            this.EquipTab.Size = new System.Drawing.Size(612, 283);
            this.EquipTab.TabIndex = 1;
            this.EquipTab.Text = "Equips";
            this.EquipTab.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(-4, -2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(317, 287);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item Name";
            this.columnHeader1.Width = 254;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Quantity";
            this.columnHeader2.Width = 59;
            // 
            // UseTab
            // 
            this.UseTab.Controls.Add(this.listView2);
            this.UseTab.Location = new System.Drawing.Point(4, 22);
            this.UseTab.Name = "UseTab";
            this.UseTab.Padding = new System.Windows.Forms.Padding(3);
            this.UseTab.Size = new System.Drawing.Size(612, 283);
            this.UseTab.TabIndex = 2;
            this.UseTab.Text = "Use";
            this.UseTab.UseVisualStyleBackColor = true;
            // 
            // SetUpTab
            // 
            this.SetUpTab.Controls.Add(this.listView3);
            this.SetUpTab.Location = new System.Drawing.Point(4, 22);
            this.SetUpTab.Name = "SetUpTab";
            this.SetUpTab.Padding = new System.Windows.Forms.Padding(3);
            this.SetUpTab.Size = new System.Drawing.Size(612, 283);
            this.SetUpTab.TabIndex = 3;
            this.SetUpTab.Text = "SetUp";
            this.SetUpTab.UseVisualStyleBackColor = true;
            // 
            // EtcTab
            // 
            this.EtcTab.Controls.Add(this.listView4);
            this.EtcTab.Location = new System.Drawing.Point(4, 22);
            this.EtcTab.Name = "EtcTab";
            this.EtcTab.Padding = new System.Windows.Forms.Padding(3);
            this.EtcTab.Size = new System.Drawing.Size(612, 283);
            this.EtcTab.TabIndex = 4;
            this.EtcTab.Text = "Etc";
            this.EtcTab.UseVisualStyleBackColor = true;
            // 
            // CashTab
            // 
            this.CashTab.Controls.Add(this.listView5);
            this.CashTab.Location = new System.Drawing.Point(4, 22);
            this.CashTab.Name = "CashTab";
            this.CashTab.Size = new System.Drawing.Size(612, 283);
            this.CashTab.TabIndex = 5;
            this.CashTab.Text = "Cash";
            this.CashTab.UseVisualStyleBackColor = true;
            // 
            // listView6
            // 
            this.listView6.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12});
            this.listView6.GridLines = true;
            this.listView6.Location = new System.Drawing.Point(-4, -2);
            this.listView6.Name = "listView6";
            this.listView6.Size = new System.Drawing.Size(317, 287);
            this.listView6.TabIndex = 5;
            this.listView6.UseCompatibleStateImageBehavior = false;
            this.listView6.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Item Name";
            this.columnHeader11.Width = 254;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Quantity";
            this.columnHeader12.Width = 59;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(-4, -2);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(317, 287);
            this.listView2.TabIndex = 5;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item Name";
            this.columnHeader3.Width = 254;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Quantity";
            this.columnHeader4.Width = 59;
            // 
            // listView3
            // 
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.listView3.GridLines = true;
            this.listView3.Location = new System.Drawing.Point(-4, -2);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(317, 287);
            this.listView3.TabIndex = 5;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Item Name";
            this.columnHeader5.Width = 254;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Quantity";
            this.columnHeader6.Width = 59;
            // 
            // listView4
            // 
            this.listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.listView4.GridLines = true;
            this.listView4.Location = new System.Drawing.Point(-4, -2);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(317, 287);
            this.listView4.TabIndex = 5;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Item Name";
            this.columnHeader7.Width = 254;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Quantity";
            this.columnHeader8.Width = 59;
            // 
            // listView5
            // 
            this.listView5.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.listView5.GridLines = true;
            this.listView5.Location = new System.Drawing.Point(-4, -2);
            this.listView5.Name = "listView5";
            this.listView5.Size = new System.Drawing.Size(317, 287);
            this.listView5.TabIndex = 5;
            this.listView5.UseCompatibleStateImageBehavior = false;
            this.listView5.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Item Name";
            this.columnHeader9.Width = 254;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Quantity";
            this.columnHeader10.Width = 59;
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PacketTabs);
            this.Name = "Inventory";
            this.Size = new System.Drawing.Size(620, 309);
            this.PacketTabs.ResumeLayout(false);
            this.EquippedTab.ResumeLayout(false);
            this.EquipTab.ResumeLayout(false);
            this.UseTab.ResumeLayout(false);
            this.SetUpTab.ResumeLayout(false);
            this.EtcTab.ResumeLayout(false);
            this.CashTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl PacketTabs;
        private System.Windows.Forms.TabPage EquippedTab;
        private System.Windows.Forms.TabPage EquipTab;
        private System.Windows.Forms.TabPage UseTab;
        private System.Windows.Forms.TabPage SetUpTab;
        private System.Windows.Forms.TabPage EtcTab;
        private System.Windows.Forms.TabPage CashTab;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listView6;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ListView listView5;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}
