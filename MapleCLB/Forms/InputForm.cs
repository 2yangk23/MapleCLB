﻿using System;
using System.Windows.Forms;
using SharedTools;

namespace MapleCLB.Forms {
    public sealed partial class InputForm : Form {
        private InputForm(string title, string text, string banner, string confirm) {
            InitializeComponent();
            Text = title;
            NameInput.Text = text;
            ConfirmBtn.Text = confirm;

            NameInput.SetCueBanner(banner);
        }

        public static DialogResult Show(string title, ref string text, string banner = "", string confirm = "OK") {
            var form = new InputForm(title, text, banner, confirm);
            var result = form.ShowDialog();
            text = form.NameInput.Text;
            return result;
        }

        private void SaveBtn_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
