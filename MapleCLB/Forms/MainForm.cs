using System.Reflection;
using System.Windows.Forms;

namespace MapleCLB.Forms {
    public sealed partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            Text = $"[{Assembly.GetEntryAssembly().GetName().Version}] MapleStory Clientless Bot";
        }
    }
}
