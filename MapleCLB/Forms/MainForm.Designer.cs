using System.Windows.Forms;

namespace MapleCLB.Forms
{
    sealed partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AccountTabs = new TabControl();
            this.Account1 = new TabPage();
            this.clientForm1 = new MapleCLB.Forms.ClientForm();
            this.AccountTabs.SuspendLayout();
            this.Account1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountTabs
            // 
            this.AccountTabs.Controls.Add(this.Account1);
            this.AccountTabs.Location = new System.Drawing.Point(7, 8);
            this.AccountTabs.Name = "AccountTabs";
            this.AccountTabs.SelectedIndex = 0;
            this.AccountTabs.Size = new System.Drawing.Size(653, 409);
            this.AccountTabs.TabIndex = 25;
            // 
            // Account1
            // 
            this.Account1.Controls.Add(this.clientForm1);
            this.Account1.Location = new System.Drawing.Point(4, 22);
            this.Account1.Name = "Account1";
            this.Account1.Padding = new Padding(3);
            this.Account1.Size = new System.Drawing.Size(645, 383);
            this.Account1.TabIndex = 0;
            this.Account1.Text = "Some Acc";
            this.Account1.UseVisualStyleBackColor = true;
            // 
            // clientForm1
            // 
            this.clientForm1.AutoScroll = true;
            this.clientForm1.Location = new System.Drawing.Point(3, 7);
            this.clientForm1.Name = "clientForm1";
            this.clientForm1.Size = new System.Drawing.Size(643, 378);
            this.clientForm1.TabIndex = 24;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 423);
            this.Controls.Add(this.AccountTabs);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MapleStory Clientless Bot";
            this.AccountTabs.ResumeLayout(false);
            this.Account1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ClientForm clientForm1;
        private TabControl AccountTabs;
        private TabPage Account1;
    }
}

