using MapleCLB.MapleClient;
using MapleCLB.Types.Items;
using MapleLib.Packet;

namespace MapleCLB.Packets.Recv {
    internal static class Update {
        public static void Inventory(Client c, PacketReader r) {
            r.ReadByte(); // [EnableActions Bool]
            for (int i = r.ReadShort(); i > 0; i--){
                byte action = r.ReadByte();
                var tab = (InventoryTab)r.ReadByte();
                short slot = r.ReadShort();
                switch (action){
                    case 0x00: //Add New Item If not already in slot or already full?
                        if (tab == InventoryTab.EQUIP){
                            c.Inventory.Add(tab, r.Read<Equip>(slot));
                        }
                        else {
                            c.Inventory.Add(tab, r.Read<Other>(slot));
                        }
                            break;
                    case 0x01: //Update Item if more of same slot looted
                        c.Inventory.Update(tab, slot, r.ReadShort());
                        break;
                    case 0x02: //Move Item Between Slots (Probably not used)
                        c.Inventory.Move(tab, slot, r.ReadShort());
                        break;
                    case 0x03: //Remove All of that Slot
                        c.Inventory.Update(tab, slot, 0);
                        break;
                }

            }
          }
     
            //TODO: Report inventory update to client?
        

        //TODO: See other function vals (spoof RECV), example:
        // [42 00] 00 00 00 [01] 00 00 00 00 00 [1A 5D 6D D9 01 00 00 00] FF 00 00 00 00
        public static void Status(Client c, PacketReader r) {
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
                default:
                    break;
            }
        }
    }
}
