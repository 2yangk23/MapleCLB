namespace MapleCLB.Forms
{
    partial class FreeMarketForm
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
            this.RegSpotBotGroup = new System.Windows.Forms.GroupBox();
            this.StartFMBotNorButton = new System.Windows.Forms.Button();
            this.FMStealIgnBox = new System.Windows.Forms.TextBox();
            this.NextSpotCB = new System.Windows.Forms.CheckBox();
            this.ServerCheckGroup = new System.Windows.Forms.GroupBox();
            this.FHTBox = new System.Windows.Forms.TextBox();
            this.YTBox = new System.Windows.Forms.TextBox();
            this.XTBox = new System.Windows.Forms.TextBox();
            this.SCModeStartButton = new System.Windows.Forms.Button();
            this.ShopInfoGroup = new System.Windows.Forms.GroupBox();
            this.NameOfShopTBox = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.PermitCB = new System.Windows.Forms.CheckBox();
            this.RegSpotBotGroup.SuspendLayout();
            this.ServerCheckGroup.SuspendLayout();
            this.ShopInfoGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // RegSpotBotGroup
            // 
            this.RegSpotBotGroup.Controls.Add(this.StartFMBotNorButton);
            this.RegSpotBotGroup.Controls.Add(this.FMStealIgnBox);
            this.RegSpotBotGroup.Controls.Add(this.NextSpotCB);
            this.RegSpotBotGroup.Location = new System.Drawing.Point(7, 2);
            this.RegSpotBotGroup.Name = "RegSpotBotGroup";
            this.RegSpotBotGroup.Size = new System.Drawing.Size(216, 68);
            this.RegSpotBotGroup.TabIndex = 1;
            this.RegSpotBotGroup.TabStop = false;
            this.RegSpotBotGroup.Text = "Normal SpotBot";
            // 
            // StartFMBotNorButton
            // 
            this.StartFMBotNorButton.Location = new System.Drawing.Point(134, 29);
            this.StartFMBotNorButton.Name = "StartFMBotNorButton";
            this.StartFMBotNorButton.Size = new System.Drawing.Size(75, 23);
            this.StartFMBotNorButton.TabIndex = 0;
            this.StartFMBotNorButton.Text = "Start";
            this.StartFMBotNorButton.UseVisualStyleBackColor = true;
            this.StartFMBotNorButton.Click += new System.EventHandler(this.StartFMBotNorButton_Click);
            // 
            // FMStealIgnBox
            // 
            this.FMStealIgnBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.FMStealIgnBox.Location = new System.Drawing.Point(5, 42);
            this.FMStealIgnBox.Name = "FMStealIgnBox";
            this.FMStealIgnBox.Size = new System.Drawing.Size(108, 20);
            this.FMStealIgnBox.TabIndex = 3;
            // 
            // NextSpotCB
            // 
            this.NextSpotCB.AutoSize = true;
            this.NextSpotCB.Location = new System.Drawing.Point(6, 19);
            this.NextSpotCB.Name = "NextSpotCB";
            this.NextSpotCB.Size = new System.Drawing.Size(107, 17);
            this.NextSpotCB.TabIndex = 4;
            this.NextSpotCB.Text = "Take Next Spot?";
            this.NextSpotCB.UseVisualStyleBackColor = true;
            // 
            // ServerCheckGroup
            // 
            this.ServerCheckGroup.Controls.Add(this.FHTBox);
            this.ServerCheckGroup.Controls.Add(this.YTBox);
            this.ServerCheckGroup.Controls.Add(this.XTBox);
            this.ServerCheckGroup.Controls.Add(this.SCModeStartButton);
            this.ServerCheckGroup.Location = new System.Drawing.Point(7, 76);
            this.ServerCheckGroup.Name = "ServerCheckGroup";
            this.ServerCheckGroup.Size = new System.Drawing.Size(216, 52);
            this.ServerCheckGroup.TabIndex = 1;
            this.ServerCheckGroup.TabStop = false;
            this.ServerCheckGroup.Text = "Server Check Mode";
            // 
            // FHTBox
            // 
            this.FHTBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.FHTBox.Location = new System.Drawing.Point(6, 22);
            this.FHTBox.Name = "FHTBox";
            this.FHTBox.Size = new System.Drawing.Size(37, 20);
            this.FHTBox.TabIndex = 4;
            // 
            // YTBox
            // 
            this.YTBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.YTBox.Location = new System.Drawing.Point(91, 22);
            this.YTBox.Name = "YTBox";
            this.YTBox.Size = new System.Drawing.Size(37, 20);
            this.YTBox.TabIndex = 3;
            // 
            // XTBox
            // 
            this.XTBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.XTBox.Location = new System.Drawing.Point(49, 22);
            this.XTBox.Name = "XTBox";
            this.XTBox.Size = new System.Drawing.Size(37, 20);
            this.XTBox.TabIndex = 2;
            // 
            // SCModeStartButton
            // 
            this.SCModeStartButton.Location = new System.Drawing.Point(134, 20);
            this.SCModeStartButton.Name = "SCModeStartButton";
            this.SCModeStartButton.Size = new System.Drawing.Size(75, 23);
            this.SCModeStartButton.TabIndex = 1;
            this.SCModeStartButton.Text = "Start";
            this.SCModeStartButton.UseVisualStyleBackColor = true;
            this.SCModeStartButton.Click += new System.EventHandler(this.SCModeStartButton_Click);
            // 
            // ShopInfoGroup
            // 
            this.ShopInfoGroup.Controls.Add(this.NameOfShopTBox);
            this.ShopInfoGroup.Controls.Add(this.checkBox2);
            this.ShopInfoGroup.Controls.Add(this.PermitCB);
            this.ShopInfoGroup.Location = new System.Drawing.Point(7, 134);
            this.ShopInfoGroup.Name = "ShopInfoGroup";
            this.ShopInfoGroup.Size = new System.Drawing.Size(216, 66);
            this.ShopInfoGroup.TabIndex = 2;
            this.ShopInfoGroup.TabStop = false;
            this.ShopInfoGroup.Text = "Shop Information";
            // 
            // NameOfShopTBox
            // 
            this.NameOfShopTBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.NameOfShopTBox.Location = new System.Drawing.Point(6, 39);
            this.NameOfShopTBox.Name = "NameOfShopTBox";
            this.NameOfShopTBox.Size = new System.Drawing.Size(204, 20);
            this.NameOfShopTBox.TabIndex = 2;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(67, 16);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(75, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Mushroom";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // PermitCB
            // 
            this.PermitCB.AutoSize = true;
            this.PermitCB.Location = new System.Drawing.Point(6, 16);
            this.PermitCB.Name = "PermitCB";
            this.PermitCB.Size = new System.Drawing.Size(55, 17);
            this.PermitCB.TabIndex = 0;
            this.PermitCB.Text = "Permit";
            this.PermitCB.UseVisualStyleBackColor = true;
            // 
            // FreeMarketForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 203);
            this.ControlBox = false;
            this.Controls.Add(this.ShopInfoGroup);
            this.Controls.Add(this.ServerCheckGroup);
            this.Controls.Add(this.RegSpotBotGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FreeMarketForm";
            this.Text = "FM Functions";
            this.TopMost = true;
            this.RegSpotBotGroup.ResumeLayout(false);
            this.RegSpotBotGroup.PerformLayout();
            this.ServerCheckGroup.ResumeLayout(false);
            this.ServerCheckGroup.PerformLayout();
            this.ShopInfoGroup.ResumeLayout(false);
            this.ShopInfoGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox RegSpotBotGroup;
        private System.Windows.Forms.GroupBox ServerCheckGroup;
        private System.Windows.Forms.GroupBox ShopInfoGroup;
        private System.Windows.Forms.CheckBox NextSpotCB;
        private System.Windows.Forms.TextBox FMStealIgnBox;
        private System.Windows.Forms.Button StartFMBotNorButton;
        private System.Windows.Forms.TextBox NameOfShopTBox;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox PermitCB;
        private System.Windows.Forms.TextBox FHTBox;
        private System.Windows.Forms.TextBox YTBox;
        private System.Windows.Forms.TextBox XTBox;
        private System.Windows.Forms.Button SCModeStartButton;
    }
}