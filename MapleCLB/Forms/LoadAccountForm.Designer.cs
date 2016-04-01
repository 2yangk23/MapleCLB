namespace MapleCLB.Forms {
    partial class LoadAccountForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.AccountList = new System.Windows.Forms.ListView();
            this.UserCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SelectBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AccountList
            // 
            this.AccountList.BackColor = System.Drawing.SystemColors.Window;
            this.AccountList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserCol});
            this.AccountList.FullRowSelect = true;
            this.AccountList.GridLines = true;
            this.AccountList.LabelWrap = false;
            this.AccountList.Location = new System.Drawing.Point(0, 0);
            this.AccountList.Name = "AccountList";
            this.AccountList.Size = new System.Drawing.Size(224, 232);
            this.AccountList.TabIndex = 0;
            this.AccountList.TabStop = false;
            this.AccountList.UseCompatibleStateImageBehavior = false;
            this.AccountList.View = System.Windows.Forms.View.Details;
            this.AccountList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.AccountList_ColumnClick);
            this.AccountList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.AccountList_ColumnWidthChanging);
            this.AccountList.DoubleClick += new System.EventHandler(this.SelectAccount_Click);
            // 
            // UserCol
            // 
            this.UserCol.Text = "Username";
            this.UserCol.Width = 220;
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(0, 233);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(107, 30);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.TabStop = false;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SelectBtn
            // 
            this.SelectBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SelectBtn.Location = new System.Drawing.Point(107, 233);
            this.SelectBtn.Name = "SelectBtn";
            this.SelectBtn.Size = new System.Drawing.Size(117, 30);
            this.SelectBtn.TabIndex = 1;
            this.SelectBtn.Text = "Select";
            this.SelectBtn.UseVisualStyleBackColor = true;
            this.SelectBtn.Click += new System.EventHandler(this.SelectAccount_Click);
            // 
            // LoadAccountForm
            // 
            this.AcceptButton = this.SelectBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(224, 263);
            this.Controls.Add(this.SelectBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.AccountList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoadAccountForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load Account";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader UserCol;
        private System.Windows.Forms.ListView AccountList;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SelectBtn;
    }
}