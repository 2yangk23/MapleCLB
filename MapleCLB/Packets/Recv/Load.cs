using MapleCLB.MapleClient;
using MapleCLB.Types;
using MapleCLB.Types.Items;
using MapleLib.Packet;
using SharedTools;

namespace MapleCLB.Packets.Recv {
    internal static class Load {
        private const int MAGIC_NUM = -1770422;

        public static void CharInfo(object o, PacketReader pr) {
            var c = o as Client;
            Precondition.NotNull(c);

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
            c.Mapler = pr.ReadMapler();

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
            c.Inventory = pr.ReadInventory();

            c.Channel = (byte) (channel + 1);

            c.UpdateMesos.Report(c.Inventory.Mesos);
            c.UpdateItems.Report(c.totalItemCount);
            c.UpdateMapler.Report(c.Mapler);
            c.UpdateChannel.Report(c.Channel);
        }

        public static void UpdateInventory(object o, PacketReader r) {
            var c = o as Client;
            Precondition.NotNull(c);

            r.ReadByte(); // [EnableActions Bool]
            for (int i = r.ReadShort(); i > 0; i--) {
                var tab = (InventoryTab) r.ReadByte();
                short slot = r.ReadShort();
                switch (r.ReadByte()) {
                    case 0x00: // Add item
                        if (tab == InventoryTab.EQUIP) {
                            c.Inventory.Add(tab, r.Read<Equip>(slot));
                        } else {
                            c.Inventory.Add(tab, r.Read<Other>(slot));
                        }
                        break;
                    case 0x01: // Update item
                        c.Inventory.Update(tab, slot, r.ReadShort());
                        break;
                    case 0x02: // Move item
                        c.Inventory.Move(tab, slot, r.ReadShort());
                        break;
                    case 0x03: // Remove item
                        c.Inventory.Update(tab, slot, 0);
                        break;
                }
            }
            //TODO: Report inventory update to client?
        }

        //TODO: See other function vals (spoof RECV), example:
        // [42 00] 00 00 00 [01] 00 00 00 00 00 [1A 5D 6D D9 01 00 00 00] FF 00 00 00 00
        public static void UpdateStatus(object o, PacketReader r) {
            var c = o as Client;
            Precondition.NotNull(c);

            r.Skip(3); // [InChat (1)] 00 00
            switch (r.ReadByte()) {
                case 0x01: // Exp update
                    r.Skip(5); // [Zeros (5)]
                    c.Mapler.Exp = r.ReadLong();
                    c.UpdateExp.Report(c.Mapler.Exp);
                    break;
                case 0x04: // Meso update
                    r.Skip(5); // [Zeros (5)]
                    c.Inventory.Mesos = r.ReadLong();
                    c.UpdateMesos.Report(c.Inventory.Mesos);
                    break;
            }
        }

        public static void Seed(object o, PacketReader pr) {
            var c = o as Client;
            Precondition.NotNull(c);

            int seed = pr.ReadInt();
            c.PortalCrc = c.Mapler.Id ^ seed ^ MAGIC_NUM;
            c.PortalCount = 1;
        }
    }
}
