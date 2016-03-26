using System;
using System.Windows.Forms;
using MapleCLB.Tools;
namespace MapleCLB.Forms
{
    public partial class FreeMarketForm : Form {
        private readonly ClientForm mParent;

        public FreeMarketForm(ClientForm frm1) {
            InitializeComponent();
            mParent = frm1;
            Win32.SendMessage(FMStealIgnBox.Handle, Win32.EM_SETCUEBANNER, 0, "Enter IGN");
            Win32.SendMessage(XTBox.Handle, Win32.EM_SETCUEBANNER, 0, "X");
            Win32.SendMessage(YTBox.Handle, Win32.EM_SETCUEBANNER, 0, "Y");
            Win32.SendMessage(FHTBox.Handle, Win32.EM_SETCUEBANNER, 0, "FH");
            Win32.SendMessage(NameOfShopTBox.Handle, Win32.EM_SETCUEBANNER, 0, "Enter Shop Name");
        }

        private void StartFMBotNorButton_Click(object sender, EventArgs e) {
            if (NameOfShopTBox.Text.Equals("") || (PermitCB.Checked && MushCB.Checked) || (!PermitCB.Checked && !MushCB.Checked))
                mParent.WriteLine("Incorrect Shop Information"); //Pop up eventually
            else if (FMStealIgnBox.Text.Equals("") && !NextSpotCB.Checked)
                mParent.WriteLine("Incorrect Stealing Information");//Pop up eventually
            else{//string IGN, string shopNAME, string FH, string X, string Y, bool PermitCB, bool SCMode, bool takeAnyCB
                mParent.StartScript(FMStealIgnBox.Text,NameOfShopTBox.Text,FHTBox.Text,XTBox.Text,YTBox.Text, PermitCB.Checked, false, NextSpotCB.Checked);
            }
        }

        private void SCModeStartButton_Click(object sender, EventArgs e){
            if (NameOfShopTBox.Text.Equals("") || (PermitCB.Checked && MushCB.Checked) || (!PermitCB.Checked && !MushCB.Checked))
                mParent.WriteLine("Incorrect Shop Information"); //Pop up eventually
            else if(FHTBox.Text.Equals("") || XTBox.Text.Equals("") || YTBox.Text.Equals(""))
                mParent.WriteLine("Incorrect Stealing Information");//Pop up eventually
            else{
                mParent.StartScript(FMStealIgnBox.Text, NameOfShopTBox.Text, FHTBox.Text, XTBox.Text, YTBox.Text, PermitCB.Checked, true, NextSpotCB.Checked);
            }
        }
    }
}
