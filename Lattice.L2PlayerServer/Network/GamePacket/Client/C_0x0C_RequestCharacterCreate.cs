using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using Lattice.L2PlayerServer.Service;
using Lattice.L2Common.Enum;
using Lattice.L2Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Client
{
    internal class C_0x0C_RequestCharacterCreate : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x0C;

        public C_0x0C_RequestCharacterCreate()
        {

        }

        protected override void Read(BinaryReader reader)
        {
            string name = ReadString(reader);
            int race = reader.ReadInt32();
            int sex = reader.ReadInt32();
            int @class = reader.ReadInt32();


            Character character = new Character();
            character.AccountId = Client.AccountId;
            character.Name = name;
            character.Race = (Race)race;
            character.Sex = (Sex)sex;
            character.VisibleClassId = @class;
            character.ActiveClassId = @class;
            character.Position = new System.Numerics.Vector3(-114449, 260029, -1192);

            CharacterService.Instance.CreateCharacter(character);

            Client.SendPacketAsync(new S_0x0F_CharacterCreateSuccess());
        }
    }
}
