using MapleCLB.MapleClient;
using MapleCLB.Types;
using MapleCLB.Types.Items;
using MapleLib.Packet;

namespace MapleCLB.Packets.Recv {
    internal static class Load {
        private const int MAGIC_NUM = -1770422;

        public static void CharInfo(Client c, PacketReader r) {
            if (r.Available < 100) {
                r.Skip(44);
                c.Mapler.Map = r.ReadInt();
                c.UpdateMapler.Report(c.Mapler);
                return;
            }

            r.Skip(18); // [02 00 01 00 00 00 00 00 00 00 02 00 00 00 00 00 00 00]
            int channel = r.ReadInt(); //CH Connected To
            /* [00 00 00 00 00 01 00 00 00 00] Unknown 8 Bytes that change
             * [01 00 00] Unknown 12 bytes something to do with connection
             * [FF FF FF FF FF FF FF FF] [00 FX FF FF FF FX FF FF FF FX FF FF FF]
             * [00 00 00 00 00 00 00]
             */
            r.Skip(18 + 15 + 21 + 7);

            /* Character Stats */
            c.Mapler = r.ReadMapler();

            /* Char Info */
            r.Skip(1); // BL Size
            if (r.ReadBool()) { // Skips Fairy Blessing
                r.ReadMapleString();
            }
            if (r.ReadBool()) { // Skips Emress Blessing
                r.ReadMapleString();
            }
            if (r.ReadBool()) { // Skips Ultimate Explorer's Parent
                r.ReadMapleString();
            }

            /* Inventory Info */
            c.Inventory = r.ReadInventory();

            c.Channel = (byte) (channel + 1);

            c.UpdateMesos.Report(c.Inventory.Mesos);
            c.UpdateItems.Report(c.totalItemCount);
            c.UpdateMapler.Report(c.Mapler);
            c.UpdateChannel.Report(c.Channel);
        }

        public static void Seed(Client c, PacketReader r) {
            int seed = r.ReadInt();
            c.PortalCrc = c.Mapler.Id ^ seed ^ MAGIC_NUM;
            c.PortalCount = 1;
        }
    }
}
