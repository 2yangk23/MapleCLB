namespace MapleCLB.Forms.Tabs {
    partial class RusherTab {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RusherTab));
            this.RushTree = new System.Windows.Forms.TreeView();
            this.RushStatusBar = new System.Windows.Forms.StatusStrip();
            this.MapStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.MapStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusSep = new System.Windows.Forms.ToolStripStatusLabel();
            this.RushStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.RushStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusSep2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.RushList = new System.Windows.Forms.ToolStripDropDownButton();
            this.elliniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.henesysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RushStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // RushTree
            // 
            this.RushTree.Location = new System.Drawing.Point(0, 0);
            this.RushTree.Margin = new System.Windows.Forms.Padding(0);
            this.RushTree.Name = "RushTree";
            this.RushTree.Size = new System.Drawing.Size(683, 299);
            this.RushTree.TabIndex = 1;
            this.RushTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RushTree_MouseDoubleClick);
            // 
            // RushStatusBar
            // 
            this.RushStatusBar.BackColor = System.Drawing.Color.White;
            this.RushStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MapStatusLbl,
            this.MapStatus,
            this.StatusSep,
            this.RushStatusLbl,
            this.RushStatus,
            this.StatusSep2,
            this.RushList});
            this.RushStatusBar.Location = new System.Drawing.Point(0, 299);
            this.RushStatusBar.Name = "RushStatusBar";
            this.RushStatusBar.Size = new System.Drawing.Size(683, 22);
            this.RushStatusBar.TabIndex = 2;
            this.RushStatusBar.Text = "Status";
            // 
            // MapStatusLbl
            // 
            this.MapStatusLbl.Name = "MapStatusLbl";
            this.MapStatusLbl.Size = new System.Drawing.Size(34, 17);
            this.MapStatusLbl.Text = "Map:";
            // 
            // MapStatus
            // 
            this.MapStatus.Name = "MapStatus";
            this.MapStatus.Size = new System.Drawing.Size(58, 17);
            this.MapStatus.Text = "Unknown";
            // 
            // StatusSep
            // 
            this.StatusSep.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.StatusSep.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.StatusSep.Name = "StatusSep";
            this.StatusSep.Size = new System.Drawing.Size(4, 17);
            // 
            // RushStatusLbl
            // 
            this.RushStatusLbl.Name = "RushStatusLbl";
            this.RushStatusLbl.Size = new System.Drawing.Size(42, 17);
            this.RushStatusLbl.Text = "Status:";
            // 
            // RushStatus
            // 
            this.RushStatus.Name = "RushStatus";
            this.RushStatus.Size = new System.Drawing.Size(26, 17);
            this.RushStatus.Text = "Idle";
            // 
            // StatusSep2
            // 
            this.StatusSep2.Name = "StatusSep2";
            this.StatusSep2.Size = new System.Drawing.Size(389, 17);
            this.StatusSep2.Spring = true;
            // 
            // RushList
            // 
            this.RushList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RushList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RushList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.elliniaToolStripMenuItem,
            this.henesysToolStripMenuItem});
            this.RushList.Image = ((System.Drawing.Image)(resources.GetObject("RushList.Image")));
            this.RushList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RushList.Name = "RushList";
            this.RushList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RushList.Size = new System.Drawing.Size(115, 20);
            this.RushList.Text = "Continenent Rush";
            this.RushList.ToolTipText = "Continenent Rush";
            // 
            // elliniaToolStripMenuItem
            // 
            this.elliniaToolStripMenuItem.Name = "elliniaToolStripMenuItem";
            this.elliniaToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.elliniaToolStripMenuItem.Text = "Ellinia";
            // 
            // henesysToolStripMenuItem
            // 
            this.henesysToolStripMenuItem.Name = "henesysToolStripMenuItem";
            this.henesysToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.henesysToolStripMenuItem.Text = "Henesys";
            // 
            // RusherTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RushStatusBar);
            this.Controls.Add(this.RushTree);
            this.DoubleBuffered = true;
            this.Name = "RusherTab";
            this.Size = new System.Drawing.Size(683, 321);
            this.RushStatusBar.ResumeLayout(false);
            this.RushStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView RushTree;
        private System.Windows.Forms.StatusStrip RushStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel MapStatusLbl;
        private System.Windows.Forms.ToolStripStatusLabel MapStatus;
        private System.Windows.Forms.ToolStripStatusLabel StatusSep;
        private System.Windows.Forms.ToolStripStatusLabel RushStatusLbl;
        private System.Windows.Forms.ToolStripStatusLabel RushStatus;
        private System.Windows.Forms.ToolStripStatusLabel StatusSep2;
        private System.Windows.Forms.ToolStripDropDownButton RushList;
        private System.Windows.Forms.ToolStripMenuItem elliniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem henesysToolStripMenuItem;
    }
}
