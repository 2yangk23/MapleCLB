using System.Collections.Concurrent;
using System.Collections.Generic;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.ScriptLib;
using MapleCLB.Packets.Send;

namespace MapleCLB.MapleClient.Scripts {
    internal class PlayerLoader : ComplexScript {
        // All public fields should be thread-safe
        public readonly IDictionary<int, string> UidMap = new ConcurrentDictionary<int, string>(); //Player-UID's -> ign
        public readonly IDictionary<int, byte[]> UidMovementPacket = new Dictionary<int, byte[]>(); //Player-UID's -> MovementPacket
        public readonly IDictionary<int, string> UidMushMap = new ConcurrentDictionary<int, string>(); //Mushroom UID's -> ign
        public readonly IDictionary<int, byte[]> UidMushMovementPacket = new Dictionary<int, byte[]>(); //Mushroom-UID's -> MovementPacket

        public PlayerLoader(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.REMOVE_PLAYER, RemovePlayer);
            RegisterRecv(RecvOps.SPAWN_PLAYER, SpawnPlayer);
            RegisterRecv(RecvOps.LOAD_MUSHY, SpawnMushy);
        }

        protected override void Execute() {
            WaitRecv(0xFFFF); // Wait forever
        }

        /* Handlers */
        private void RemovePlayer(PacketReader r) {
            int uid = r.ReadInt();

            Client.totalItemCount = Client.totalPeopleCount--;
            Client.UpdatePeople.Report(Client.totalPeopleCount);

            UidMap.Remove(uid);
            WriteLog($"[{uid:X8}] removed.");
        }

        private void SpawnMushy(PacketReader r) {
            int uid = r.ReadInt();
            r.Skip(4);
            short x = r.ReadShort();
            short y = r.ReadShort();
            short fh = r.ReadShort();
            string ign = r.ReadMapleString();
            byte type = r.ReadByte(); 
            int counter = r.ReadInt(); // Used to enter shops

            UidMushMap[uid] = ign;
            UidMushMovementPacket[uid] = Movement.Teleport(SendOps.FM1_CRC, x, y, fh);

            WriteLog("Added Mushroom : " + ign + " to UID : " + uid + "@ " + x + ", " + y + ", fh: " + fh);
        }

        private void SpawnPlayer(PacketReader r)
        {
            int uid = r.ReadInt();
            byte otherLEVEL = r.ReadByte();
            string ign = r.ReadMapleString();
            r.Skip(2); //Ultimate adventurer parent name
            int temp = r.ReadShort();
            if (temp != 0)
            {
                string otherGUILD = r.ReadHexString(temp);
                r.Skip(2); //Guild LogoBG
                r.Skip(1); //Guild LogoBG Colour
                r.Skip(2); //Guild Logo
                r.Skip(1); //Guild Logo Colour
            }
            else {
                r.Skip(6); //Skip the guild if no guild
            }
            r.Skip(1); //New v135
            r.Skip(12);// [40 00 00 00 01 00 00 00 00 00 00 00]
            r.Skip(64); //Mostly Zeros 
            r.Skip(4); //-1
            r.Skip(64); //Figure this part out later but until then cheat
            r.Next(01);//Time Encoding
            r.Skip(4);
            r.Skip(8);// Zero(8)
            r.Skip(5);//Time Encoding
            r.Skip(10);//Zero(10)
            r.Skip(5);//Time Encoding
            r.Skip(10);//Zero(10)
            r.Skip(5);//Time Encoding
            r.Skip(8);

            r.Skip(5);///Time Encoding

            r.Skip(5); //[00][4 Unknown]
            r.Skip(10);//Zero(10)
            r.Skip(5);//Time Encoding
            r.Skip(16);//Zero(16)
            r.Skip(5);//Time Encoding
            r.Skip(2);//Zero(2) SOMETIMES DIFFERENT???
            r.Skip(2);// JOB 
            r.Skip(2);//Sub JOB?
            r.Skip(4); //Unknown
            //Char Shit here fix later cheat look for next Short FF FF
            r.Next(0xFF);
            bool notfound = true;
            int count = 0;
            while (notfound && count != 5)
            {
                if (r.ReadByte() == 0xFF)
                {
                    r.Skip(4);
                    if (r.ReadByte() == 0 && r.ReadByte() == 0 && r.ReadByte() == 0 && r.ReadByte() == 0)
                    {
                        notfound = false;
                    }
                    else {
                        r.Next(0xFF);
                        count++;
                    }
                }
                else {
                    r.Next(0xFF);
                    count++;
                    if (count == 5)
                        WriteLog("Giving up finding permit for: " + ign);
                }
            }
            r.Skip(4); //Skip remaining zeros
            short x = r.ReadShort();
            short y = r.ReadShort();
            r.Skip(1);//Type or stance?
            short fh = r.ReadShort();
            r.Skip(18); //Unknown shit
            int counter = r.ReadInt(); //Counter used to enter shops
            UidMap[uid] = ign;

            Client.totalItemCount = Client.totalPeopleCount++;
            Client.UpdatePeople.Report(Client.totalPeopleCount);

            UidMovementPacket[uid] = Movement.Teleport(SendOps.FM1_CRC, x, y, fh);
            WriteLog("Added : " + ign + " to UID : " + uid + "@ " + x + ", " + y + ", fh: " + fh);
        }



    } 
}
