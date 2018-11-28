using Lattice.L2Common.Model;
using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using Lattice.L2PlayerServer.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Client
{
    internal class C_0x12_RequestGameStart : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x12;

        public C_0x12_RequestGameStart()
        {

        }

        protected override void Read(BinaryReader reader)
        {
            int characterIdx = reader.ReadInt32();
            reader.ReadInt16();
            reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadInt32();


            List<Character> characters = CharacterService.Instance.GetCharacters(Client.AccountId);
            Client.Character = characters[characterIdx];
            Client.Character.ObjectId = (int) Client.Character.Id;

            Client.SendPacketAsync(new S_0x0B_CharacterSelected());
            Client.SendPacketAsync(new S_0xFE_00EA_ExSubClassInfo());
        }
    }
}
