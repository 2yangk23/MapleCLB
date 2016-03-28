using MapleCLB.MapleClient;
using MapleCLB.Resources;
using MapleCLB.Types;
using MapleCLB.Types.Items;
using MapleLib.Packet;

namespace MapleCLB.Packets.Recv {
    internal class Load {
        private const int MAGIC_NUM = -1770422;
        //[Header (2)] 00 [Char count (1)] [UID (4)] [IGN (13)] ...
        public static void Charlist(Client c, PacketReader pr) {
            pr.Skip(1);
            pr.ReadMapleString(); // v170?
            pr.Skip(18);

            int temp = pr.ReadInt(); // Weird loopy shit (uids?) v167
            for (int i = 0; i < temp; i++) {
                pr.ReadInt();
            }
            byte count = pr.ReadByte();

            for (byte i = 0; i < count; ++i) {
                /* Character Stats */
                var m = Mapler.Parse(pr);

                /* AddPlayer Appearance */
                Mapler.SkipAppearance(pr, m.Job);

                bool hasRank = pr.ReadBool(); // [HasRanking (1)]
                if (hasRank) {
                    pr.Skip(16); // [Rank (4)] [Rank Move (4)] [JobRank (4)] [JobRank Move (4)]
                }

                if (m.IsZero) { // Zero
                    for (int j = 0; j < 6; ++j) { // I guess Zero has 2 extra appearance?
                        pr.Next(0xFF);
                    }
                }
                // System.Diagnostics.Debug.WriteLine("" + chr.Id + " : " + chr.Job + " : " + chr.Name + Environment.NewLine);
                c.CharMap.Add(i, m.Name.ToLower(), m.Id);
            }
        }

        public static void CharInfo(Client c, PacketReader pr) {
            //TODO: Better fix for this? Although this should always work so maybe it's good enough
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
