using Lattice.L2Common.Enum;
using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using Lattice.L2PlayerServer.Service;
using Lattice.L2PlayerServer.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Client
{
    internal class C_0x49_Say : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x49;

        public C_0x49_Say()
        {

        }

        protected override void Read(BinaryReader reader)
        {
            string target = null;
            string text = ReadString(reader);
            ChatType type = (ChatType)reader.ReadInt32();
            if (type == ChatType.Private)
                target = ReadString(reader);

            ChatService.Instance.Say(Client.Character, type, text, target);
            Client.Broadcast(new S_0x4A_Say(Client.Character.ObjectId, type, Client.Character.Name, text).WriteSilent());
        }
        

    }
}
