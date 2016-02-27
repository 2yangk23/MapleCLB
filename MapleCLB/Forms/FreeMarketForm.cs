using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapleCLB.Tools;
namespace MapleCLB.Forms
{
    public partial class FreeMarketForm : Form{
        public FreeMarketForm(){
            InitializeComponent();
            Win32.SendMessage(FMStealIgnBox.Handle, Win32.EM_SETCUEBANNER, 0, "Enter IGN");
            Win32.SendMessage(XTBox.Handle, Win32.EM_SETCUEBANNER, 0, "X");
            Win32.SendMessage(YTBox.Handle, Win32.EM_SETCUEBANNER, 0, "Y");
            Win32.SendMessage(FHTBox.Handle, Win32.EM_SETCUEBANNER, 0, "FH");
            Win32.SendMessage(NameOfShopTBox.Handle, Win32.EM_SETCUEBANNER, 0, "Enter Shop Name");
        }

        private void StartFMBotNorButton_Click(object sender, EventArgs e){

        }

        private void SCModeStartButton_Click(object sender, EventArgs e){

        }
    }
}
