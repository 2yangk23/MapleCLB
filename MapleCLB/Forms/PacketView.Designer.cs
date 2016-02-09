namespace MapleCLB.Forms {
    partial class PacketView {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.SendTree = new System.Windows.Forms.TreeView();
            this.PacketMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ManagePacket = new System.Windows.Forms.ToolStripMenuItem();
            this.IgnorePacket = new System.Windows.Forms.ToolStripMenuItem();
            this.RemovePacket = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSep = new System.Windows.Forms.ToolStripSeparator();
            this.ClearPacket = new System.Windows.Forms.ToolStripMenuItem();
            this.LogPacket = new System.Windows.Forms.CheckBox();
            this.PacketTabs = new System.Windows.Forms.TabControl();
            this.SendTab = new System.Windows.Forms.TabPage();
            this.RecvTab = new System.Windows.Forms.TabPage();
            this.RecvTree = new System.Windows.Forms.TreeView();
            this.PacketMenu.SuspendLayout();
            this.PacketTabs.SuspendLayout();
            this.SendTab.SuspendLayout();
            this.RecvTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // SendTree
            // 
            this.SendTree.ContextMenuStrip = this.PacketMenu;
            this.SendTree.Font = new System.Drawing.Font("Consolas", 8.75F);
            this.SendTree.Location = new System.Drawing.Point(-1, -1);
            this.SendTree.Margin = new System.Windows.Forms.Padding(0);
            this.SendTree.Name = "SendTree";
            this.SendTree.Size = new System.Drawing.Size(619, 285);
            this.SendTree.TabIndex = 0;
            this.SendTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PacketTree_MouseDoubleClick);
            // 
            // PacketMenu
            // 
            this.PacketMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ManagePacket,
            this.IgnorePacket,
            this.RemovePacket,
            this.MenuSep,
            this.ClearPacket});
            this.PacketMenu.Name = "PacketMenu";
            this.PacketMenu.Size = new System.Drawing.Size(118, 98);
            this.PacketMenu.Opening += new System.ComponentModel.CancelEventHandler(this.PacketMenu_Opening);
            // 
            // ManagePacket
            // 
            this.ManagePacket.Enabled = false;
            this.ManagePacket.Name = "ManagePacket";
            this.ManagePacket.Size = new System.Drawing.Size(117, 22);
            this.ManagePacket.Text = "Manage";
            this.ManagePacket.Click += new System.EventHandler(this.ManagePacket_Click);
            // 
            // IgnorePacket
            // 
            this.IgnorePacket.Enabled = false;
            this.IgnorePacket.Name = "IgnorePacket";
            this.IgnorePacket.Size = new System.Drawing.Size(117, 22);
            this.IgnorePacket.Text = "Ignore";
            this.IgnorePacket.Click += new System.EventHandler(this.IgnorePacket_Click);
            // 
            // RemovePacket
            // 
            this.RemovePacket.Name = "RemovePacket";
            this.RemovePacket.Size = new System.Drawing.Size(117, 22);
            this.RemovePacket.Text = "Remove";
            this.RemovePacket.Click += new System.EventHandler(this.RemovePacket_Click);
            // 
            // MenuSep
            // 
            this.MenuSep.Name = "MenuSep";
            this.MenuSep.Size = new System.Drawing.Size(114, 6);
            // 
            // ClearPacket
            // 
            this.ClearPacket.Name = "ClearPacket";
            this.ClearPacket.Size = new System.Drawing.Size(117, 22);
            this.ClearPacket.Text = "Clear";
            this.ClearPacket.Click += new System.EventHandler(this.ClearPacket_Click);
            // 
            // LogPacket
            // 
            this.LogPacket.AutoSize = true;
            this.LogPacket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogPacket.Location = new System.Drawing.Point(558, 1);
            this.LogPacket.Name = "LogPacket";
            this.LogPacket.Size = new System.Drawing.Size(64, 17);
            this.LogPacket.TabIndex = 2;
            this.LogPacket.Text = "Logging";
            this.LogPacket.UseVisualStyleBackColor = true;
            this.LogPacket.CheckedChanged += new System.EventHandler(this.LogPacket_CheckedChanged);
            // 
            // PacketTabs
            // 
            this.PacketTabs.Controls.Add(this.SendTab);
            this.PacketTabs.Controls.Add(this.RecvTab);
            this.PacketTabs.Location = new System.Drawing.Point(0, 0);
            this.PacketTabs.Margin = new System.Windows.Forms.Padding(0);
            this.PacketTabs.Name = "PacketTabs";
            this.PacketTabs.SelectedIndex = 0;
            this.PacketTabs.Size = new System.Drawing.Size(625, 309);
            this.PacketTabs.TabIndex = 3;
            this.PacketTabs.SelectedIndexChanged += new System.EventHandler(this.PacketTabs_SelectedIndexChanged);
            // 
            // SendTab
            // 
            this.SendTab.Controls.Add(this.SendTree);
            this.SendTab.Location = new System.Drawing.Point(4, 22);
            this.SendTab.Name = "SendTab";
            this.SendTab.Padding = new System.Windows.Forms.Padding(3);
            this.SendTab.Size = new System.Drawing.Size(617, 283);
            this.SendTab.TabIndex = 0;
            this.SendTab.Text = "Send Log";
            this.SendTab.UseVisualStyleBackColor = true;
            // 
            // RecvTab
            // 
            this.RecvTab.Controls.Add(this.RecvTree);
            this.RecvTab.Location = new System.Drawing.Point(4, 22);
            this.RecvTab.Name = "RecvTab";
            this.RecvTab.Padding = new System.Windows.Forms.Padding(3);
            this.RecvTab.Size = new System.Drawing.Size(617, 283);
            this.RecvTab.TabIndex = 1;
            this.RecvTab.Text = "Recv Log";
            this.RecvTab.UseVisualStyleBackColor = true;
            // 
            // RecvTree
            // 
            this.RecvTree.ContextMenuStrip = this.PacketMenu;
            this.RecvTree.Font = new System.Drawing.Font("Consolas", 8.75F);
            this.RecvTree.Location = new System.Drawing.Point(-1, -1);
            this.RecvTree.Margin = new System.Windows.Forms.Padding(0);
            this.RecvTree.Name = "RecvTree";
            this.RecvTree.Size = new System.Drawing.Size(619, 285);
            this.RecvTree.TabIndex = 1;
            // 
            // PacketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LogPacket);
            this.Controls.Add(this.PacketTabs);
            this.Name = "PacketView";
            this.Size = new System.Drawing.Size(625, 309);
            this.PacketMenu.ResumeLayout(false);
            this.PacketTabs.ResumeLayout(false);
            this.SendTab.ResumeLayout(false);
            this.RecvTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView SendTree;
        private System.Windows.Forms.ContextMenuStrip PacketMenu;
        private System.Windows.Forms.ToolStripMenuItem IgnorePacket;
        private System.Windows.Forms.ToolStripMenuItem RemovePacket;
        private System.Windows.Forms.ToolStripSeparator MenuSep;
        private System.Windows.Forms.ToolStripMenuItem ClearPacket;
        private System.Windows.Forms.CheckBox LogPacket;
        private System.Windows.Forms.ToolStripMenuItem ManagePacket;
        private System.Windows.Forms.TabControl PacketTabs;
        private System.Windows.Forms.TabPage SendTab;
        private System.Windows.Forms.TabPage RecvTab;
        private System.Windows.Forms.TreeView RecvTree;
    }
}
