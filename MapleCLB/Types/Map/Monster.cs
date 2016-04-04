using System;
using MapleLib.Packet;

namespace MapleCLB.Types.Map {
    internal class Monster : MapObject {
        internal Monster(int id) : base(id) { }
    }

    internal static class MonsterPacketExtensions {
        internal static Monster ReadMonster(this PacketReader pr) {
            pr.ReadByte();
            var m = new Monster(pr.ReadInt());
            pr.ReadBool();
            pr.ReadInt(); // [MonsterId (4)]
            if (pr.ReadBool()) {
                throw new NotImplementedException("Read Monster cannot handle: " + pr);
            }
            pr.Skip(4 + 16 + 151); // [?? (4)] [?? (16)] [MobStatus (151)]
            m.Position = pr.ReadPosition();
            // [Stance (1)] [Fh (2)] [InitFh (2)] [Animation -1=instant,-2=fade (2)] FF 7D 
            // [Zero (24)] [FF FF FF FF] 00 [Zero (8)] [FF FF FF FF] [00 00 00 00]

            return m;
        }
    }
}
