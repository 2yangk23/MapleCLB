using System.Collections.Generic;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;


namespace MapleCLB.MapleClient.Scripts {
    internal sealed class SpotStealerBot : ComplexScript {
        private const int FM1_CRC = 0x2A7AC228;
        private readonly Dictionary<int, byte[]> UidMovementPacket = new Dictionary<int, byte[]>(); //UID -> MovementPacket
        private readonly Dictionary<int, string> UidMap = new Dictionary<int, string>(); //uid -> ign

        public SpotStealerBot(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.LOAD_MUSHY, SpawnMushy);
            RegisterRecv(RecvOps.SPAWN_PLAYER, SpawnPlayer);
            RegisterRecv(RecvOps.CLOSE_PERMIT, StealSpotWithPermit);
            RegisterRecv(RecvOps.CLOSE_MUSHY, StealSpotWithPermit); //Header is guessed, double check
        }

        //To Do: add custom items to permit
        protected override void Execute() {
            WaitRecv(RecvOps.FINISH_LOAD);
            SendPacket(Movement.beforeTeleport());
            SendPacket(Movement.beforeTeleport());
            SendPacket(Movement.Teleport(FM1_CRC, 80, 34, 52)); //Lands on the ground
            WaitRecv(RecvOps.FINISH_LOAD_PERMIT);
            SendPacket(Trade.PutItem(2, 1, 1, 1, 9999999));
            WaitRecv(RecvOps.FINISH_LOAD_PERMIT);
            SendPacket(Trade.OpenShop());
            WriteLog("Shop Open For Business!");
            //WaitRecv(0xFFFF); // Why wait? once you got the spot, the script is complete it can close and release threads
        }

        private void StealSpotWithPermit(PacketReader r) {
            int uid = r.ReadInt();
            if ("Rawche".Equals(UidMap[uid])) {
                //SendPacket(Movement.beforeTeleport()); I dont think you have to send it before your second movement have to test
                SendPacket(Movement.beforeTeleport());
                SendPacket(UidMovementPacket[uid]);
                WriteLog("Movement Sent!");
                SendPacket(Trade.CreateShop(5, "Thanks", 1, 5140000));
                UidMovementPacket.Remove(uid);
            }
        }

        private void SpawnMushy(PacketReader r) {
            int uid = r.ReadInt();
            r.Skip(4);
            short x = r.ReadShort();
            short y = r.ReadShort();
            short fh = r.ReadShort();
            string ign = r.ReadMapleString();

            UidMap[uid] = ign;
            UidMovementPacket[uid] = Movement.Teleport(FM1_CRC, x, y, fh);

            WriteLog("Added : " + ign + " to UID : " + uid + "@ " + x + ", " + y + ", fh: " + fh);
        }

        private void SpawnPlayer(PacketReader r) {
            int uid = r.ReadInt();
            byte otherLEVEL = r.ReadByte();
            string ign = r.ReadMapleString();
            r.Skip(2); //Ultimate adventurer parent name
            int temp = r.ReadShort();
            if (temp != 0) {
                string otherGUILD = r.ReadHexString(temp);
                r.Skip(2); //Guild LogoBG
                r.Skip(1); //Guild LogoBG Colour
                r.Skip(2); //Guild Logo
                r.Skip(1); //Guild Logo Colour
            } else {
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
            while (notfound && count != 5) {
                if (r.ReadByte() == 0xFF) {
                    r.Skip(4);
                    if (r.ReadByte() == 0 && r.ReadByte() == 0 && r.ReadByte() == 0 && r.ReadByte() == 0) {
                        notfound = false;
                    } else {
                        r.Next(0xFF);
                        count++;
                    }
                } else {
                    r.Next(0xFF);
                    count++;
                    if (count == 5)
                        WriteLog("Giving up finding permit for: " + ign);
                }
            }
            r.Skip(4); //Skip remaining zeros
            short x = r.ReadShort();
            short y = r.ReadShort();
            r.Skip(1);//Stance
            short fh = r.ReadShort();

            UidMap[uid] = ign;
            //To Do: Split this movement packet so players/permits and mushrooms have a different dictionary
            UidMovementPacket[uid] = Movement.Teleport(FM1_CRC, x, y, fh);
            WriteLog("Added : " + ign + " to UID : " + uid + "@ " + x + ", " + y + ", fh: " + fh);
        }
    }
}
