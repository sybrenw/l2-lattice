using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Client
{
    internal class C_0x56_RequestAction : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x56;

        public C_0x56_RequestAction()
        {

        }

        protected override void Read(BinaryReader reader)
        {
            int action = reader.ReadInt32();

            Client.SendPacketAsync(new S_0x27_SocialAction(Client.Character.ObjectId, action));
        }
    }
}
