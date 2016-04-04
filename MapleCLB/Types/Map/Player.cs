using MapleLib.Packet;

namespace MapleCLB.Types.Map {
    internal class Player : MapObject {
        internal string Ign { get; set; }
        internal short Fh { get; set; }
        internal bool HasPermit { get; set; }

        internal Player(int id) : base(id) { }
    }

    internal static class PlayerPacketExtensions {
        internal static Player ReadPlayer(this PacketReader pr) {
            var p = new Player(pr.ReadInt());
            pr.ReadByte(); //Level
            p.Ign = pr.ReadMapleString();
            pr.ReadMapleString(); // Ultimate Explorer's Parent
            pr.ReadMapleString(); // Guild
            pr.Skip(19); // [LogoBG (2)] [Color (1)] [Logo (2)] [Color (1)] 00 [40 00 00 00] [01 00 00 00] [00 00 00 00]

            // Sub
            pr.Skip(64 + 4 + 64); // [Mostly Zeros (64] FF FF FF FF [??? (64)]
            pr.Next(01); // End of Time Encoding
            pr.Skip(12); // ?? ?? ?? ?? [Zero (8)]
            pr.Skip(30); // [TimeEncode (5)] [00 00] [00 00 00 00] [00 00 00 00] [TimeEncode (5)] [00 00] [00 00 00 00] [00 00 00 00]
            pr.Skip(13); // [TimeEncode (5)] [00 00 00 00] [00 00 00 00]
            pr.Skip(20); // [TimeEncode (5)] 00 [DE AC 77 DA] [00 00 00 00] [00 00 00 00] [00 00]
            pr.Skip(28); // [TimeEncode (5)] [Zero (16)] [TimeEncode (5)] 00 00

            short job = pr.ReadShort(); // JOB 
            pr.Skip(6); // [SubJob (2)] [? (4)]
            pr.SkipAppearance(job);
            pr.Skip(4 * 14 + 14); // [00 00 00 00] * 14 [FF FF] [00 00 00 00 00 00 00 00 00 00 00 00]

            p.Position = pr.ReadPosition();
            pr.Skip(1); // Type or stance?
            p.Fh = pr.ReadShort();
            pr.Skip(17); // Unknown shit
            p.HasPermit = pr.ReadBool(); // 0x05 means Permit

            return p;
        }
    }
}
