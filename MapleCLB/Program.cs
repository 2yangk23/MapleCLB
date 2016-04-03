using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MapleCLB.Forms;
using MapleLib.Crypto;

namespace MapleCLB {
    internal static class Program {
        private static MainForm gui;

        private static int rngSeed = Environment.TickCount;
        private static readonly byte[] userKey = { //171.3
            0xF1, 0x00, 0x00, 0x00,
            0x02, 0x00, 0x00, 0x00,
            0x15, 0x00, 0x00, 0x00,
            0xE2, 0x00, 0x00, 0x00,
            0x68, 0x00, 0x00, 0x00,
            0xA6, 0x00, 0x00, 0x00,
            0xF2, 0x00, 0x00, 0x00,
            0xDE, 0x00, 0x00, 0x00
        };

        public static readonly IPAddress LoginIp = IPAddress.Parse("8.31.99.143");
        public static readonly short LoginPort = 8484;
        public static readonly AesCipher AesCipher = new AesCipher(userKey);

        public static readonly ThreadLocal<Random> Rng =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref rngSeed)));

        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            gui = new MainForm();
            Application.Run(gui);
        }
    }
}
