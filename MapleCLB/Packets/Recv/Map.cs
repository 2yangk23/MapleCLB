using MapleCLB.MapleClient;
using MapleCLB.Types.Map;
using MapleLib.Packet;

namespace MapleCLB.Packets.Recv {
    internal static class Map {
        #region Player Spawn Handlers
        public static void SpawnPlayer(Client c, PacketReader r) {
            var player = r.ReadPlayer();
            c.UidMap[player.Id] = player;
        }

        public static void RemovePlayer(Client c, PacketReader r) {
            int uid = r.ReadInt();

            Player trash;
            c.UidMap.TryRemove(uid, out trash);
        }
        #endregion

        #region Monster Spawn Handlers
        public static void SpawnMonster(Client c, PacketReader r) {
            var monster = r.ReadMonster();
            c.MonsterMap[monster.Id] = monster;
        }

        public static void RemoveMonster(Client c, PacketReader r) {
            int id = r.ReadInt();
            // r.ReadBool();
            Monster trash;
            c.MonsterMap.TryRemove(id, out trash);
        }
        #endregion Spawn Handlers

        #region Reactor Spawn Handlers
        public static void SpawnReactor(Client c, PacketReader r) {
            var reactor = r.ReadReactor();
            c.ReactorMap[reactor.Id] = reactor;
        }

        public static void UpdateReactor(Client c, PacketReader r) {
            var reactor = r.ReadReactor();
            switch (reactor.Hits) {
                case 0: // Spawn
                    c.ReactorMap[reactor.Id] = reactor;
                    break;
                case 1: // Damage
                case 2: // Damage
                case 3: // Damage
                    c.ReactorMap[reactor.Id].Hits = reactor.Hits;
                    break;
                case 4: // Destroy
                    Reactor trash;
                    c.ReactorMap.TryRemove(reactor.Id, out trash);
                    break;
            }
        }
        #endregion
    }
}
