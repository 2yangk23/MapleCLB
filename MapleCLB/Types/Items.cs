using System;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Types {
    public sealed class Items {

        public int itemType { get; set; }
        public int Id { get; set; }
        public int PotentialLevel { get; set; }
        public int EnhanceLevel { get; set; }

        public int Quantity { get; set; }



        public static Items Parse(PacketReader pr) {
            var e = new Items();
            e.Quantity = 1;
            //if (itemType == 1)
            //    pr.Skip(2);
            //else
            //    pr.Skip(1);
            e.itemType = pr.ReadByte(); 
            e.Id = pr.ReadInt();
            pr.Skip(1); // Zero(1);
            pr.Skip(8); // Long Time Stamp
            pr.Skip(4); // [FF FF FF FF]
            if (e.itemType == 1) //If Equip 
            {
                pr.Next(0xFF);// Skip item stats -> does this set pos to FF?
                //pr.Skip(1); //Skip the FF?
                pr.Skip(4); //[00 11 00 00] Unknown
                pr.ReadMapleString();
                e.PotentialLevel = pr.ReadByte();
                e.EnhanceLevel = pr.ReadByte();
                pr.Skip(6); //Potential 1,2,3
                pr.Skip(6); //Bonus Potential 1,2,3
                pr.Skip(2); //Zero(2)
                pr.Skip(8); //[Socket State(2)][Socket 1 (2)][Socket 2 (2)][Socket 3 (2)]
                pr.Skip(8); //DataBase ID (8)
                pr.Skip(8); //Long Time Stamp
                pr.Skip(4); // [FF FF FF FF]
                pr.Skip(8); // Zero(8)
                pr.Skip(8); //Long Time Stamp
                pr.Skip(20); // Zero(20)
                pr.Skip(2); // Zero(2)
            }
            else 
            {
                e.Quantity = pr.ReadShort();
                pr.ReadMapleString();
                pr.Skip(2); //Item Flags? Maybe untradeable etc?
                if (e.Id / 10000 == 207 || e.Id / 10000 == 287 || e.Id / 10000 == 233)
                    pr.Skip(8);
            }
            return e;
        }
    }
}
