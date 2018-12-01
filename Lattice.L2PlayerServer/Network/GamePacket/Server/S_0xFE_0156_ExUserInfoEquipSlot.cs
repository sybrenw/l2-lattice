using Lattice.L2Common.Model;
using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFE_0156_ExUserInfoEquipSlot : SendablePacket<GameClient>
    {
        public const byte Opcode = 0xFE;

        public S_0xFE_0156_ExUserInfoEquipSlot() : base(Opcode, 0x56, 0x01)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            Character character = Client.Character;
            writer.Write(character.ObjectId);
            
            // Equipment slots
            writer.Write((short)60);
            // Masks
            writer.Write(0xFFFFFFFF);
            writer.Write(0xFFFFFFFF);
            List<EquipmentSlot> items = CreateItems();

            foreach(EquipmentSlot item in items)
            {
                writer.Write((short)22);
                writer.Write(item.ObjectId);
                writer.Write(item.ItemId);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
            }
        }

        private List<EquipmentSlot> CreateItems()
        {
            List<EquipmentSlot> items = new List<EquipmentSlot>();

            // Underwear
            items.Add(new EquipmentSlot() { ObjectId = 0, ItemId = 0 });
            // Jewels
            items.Add(new EquipmentSlot() { ObjectId = 0, ItemId = 0 });
            items.Add(new EquipmentSlot() { ObjectId = 0, ItemId = 0 });
            items.Add(new EquipmentSlot() { ObjectId = 0, ItemId = 0 });
            items.Add(new EquipmentSlot() { ObjectId = 0, ItemId = 0 });
            items.Add(new EquipmentSlot() { ObjectId = 0, ItemId = 0 });
            // Head
            items.Add(new EquipmentSlot() { ObjectId = 1000, ItemId = 35069 });
            // Weapon
            items.Add(new EquipmentSlot() { ObjectId = 1001, ItemId = 36495 });
            // Shield
            items.Add(new EquipmentSlot() { ObjectId = 0, ItemId = 0 });
            // Gloves
            items.Add(new EquipmentSlot() { ObjectId = 1002, ItemId = 35072 });
            // Upper
            items.Add(new EquipmentSlot() { ObjectId = 1003, ItemId = 35070 });
            // Lower
            items.Add(new EquipmentSlot() { ObjectId = 1004, ItemId = 35071 });
            // Boots
            items.Add(new EquipmentSlot() { ObjectId = 1005, ItemId = 35073 });
            // Weapon (2H)
            items.Add(new EquipmentSlot() { ObjectId = 1001, ItemId = 36495 });
            // ETC
            for (int i = 0; i < 27; i++)
                items.Add(new EquipmentSlot() { ObjectId = 0, ItemId = 0 });

            return items;
        }
    }
}
