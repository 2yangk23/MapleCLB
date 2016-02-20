using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Send;

namespace MapleCLB.Packets.Recv.Map {
    internal class Player {

        public static void SpawnPlayer(object o, PacketReader r){
            var c = o as Client;
            int uid = r.ReadInt();
            byte otherLEVEL = r.ReadByte();
            string ign = r.ReadMapleString();
            r.Skip(2); //Ultimate adventurer parent name
            int temp = r.ReadShort();
            if (temp != 0){
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
            while (notfound && count !=5){
                //if (r.ReadByte() == 0xFF && r.ReadByte() == 00 && r.ReadByte() == 00 && r.ReadByte() == 00 && r.ReadByte() == 00 && r.ReadByte() == 00)
                if(r.ReadByte() == 0xFF){
                    r.Skip(4);
                    if (r.ReadByte() == 0 && r.ReadByte() == 0 && r.ReadByte() == 0 && r.ReadByte() == 0)
                        notfound = false;
                    else{
                        r.Next(0xFF);
                        count++;
                         }

                }
                else{
                    r.Next(0xFF);
                    count++;
                    if (count == 5)
                        c.WriteLog.Report("Giving up finding permit for: " +ign);
                }
            }
            r.Skip(4); //Skip remaining zeros
            short x = r.ReadShort();
            short y = r.ReadShort();
            r.Skip(1);//Stance
            short pid = r.ReadShort();

            c.UidMap[uid] = ign;
            //To Do: Split this movement packet so players/permits and mushrooms have a different dictionary
            c.UidMovementPacket[uid] = Movement.Teleport(Client.FM1CRC, x, y, pid);
            c.WriteLog.Report("Added : " + ign + " to UID : " + uid + "@ "+x+", "+y+ ", PID: "+pid);
        }


        public static void RemovePlayer(object o, PacketReader r) {
            var c = o as Client;
            int uid = r.ReadInt();

            c.UidMap.Remove(uid);
            c.UidMovementPacket.Remove(uid);
            c.WriteLog.Report("Removed " + uid);
        }
    }
}
