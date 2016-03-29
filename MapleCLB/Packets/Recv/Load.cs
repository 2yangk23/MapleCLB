using MapleCLB.MapleClient;
using MapleCLB.Types;
using MapleCLB.Types.Items;
using MapleLib.Packet;

namespace MapleCLB.Packets.Recv {
    internal static class Load {
        private const int MAGIC_NUM = -1770422;

        public static void CharInfo(object o, PacketReader pr) {
            var c = o as Client;
            if (pr.Available < 100) {
                pr.Skip(44);
                c.Mapler.Map = pr.ReadInt();
                c.UpdateMapler.Report(c.Mapler);
                return;
            }

            pr.Skip(18); // [02 00 01 00 00 00 00 00 00 00 02 00 00 00 00 00 00 00]
            int channel = pr.ReadInt(); //CH Connected To
            /* [00 00 00 00 00 01 00 00 00 00] Unknown 8 Bytes that change
             * [01 00 00] Unknown 12 bytes something to do with connection
             * [FF FF FF FF FF FF FF FF] [00 FX FF FF FF FX FF FF FF FX FF FF FF]
             * [00 00 00 00 00 00 00]
             */
            pr.Skip(18 + 15 + 21 + 7);

            /* Character Stats */
            c.Mapler = Mapler.Parse(pr);

            /* Char Info */
            pr.Skip(1); // BL Size
            if (pr.ReadBool()) { // Skips Fairy Blessing
                pr.ReadMapleString();
            }
            if (pr.ReadBool()) { // Skips Empress Blessing
                pr.ReadMapleString();
            }
            if (pr.ReadBool()) { // Skips Ultimate Explorer's Parent
                pr.ReadMapleString();
            }

            /* Inventory Info */
            c.Inventory = Inventory.Parse(pr);

            c.Channel = (byte) (channel + 1);

            c.UpdateMesos.Report(c.Inventory.Mesos);
            c.UpdateItems.Report(c.totalItemCount);
            c.UpdateMapler.Report(c.Mapler);
            c.UpdateChannel.Report(c.Channel);
        }

        public static void Seed(object o, PacketReader pr) {
            var c = o as Client;
            int seed = pr.ReadInt();
            c.PortalCrc = c.Mapler.Id ^ seed ^ MAGIC_NUM;
            c.PortalCount = 1;
        }
    }
}
