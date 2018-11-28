using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Client
{
    internal class C_0x0E_SendProtocolVersion : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x0E;

        public C_0x0E_SendProtocolVersion()
        {

        }

        protected override void Read(BinaryReader reader)
        {
            int protocol = reader.ReadInt32();
            int accountId = reader.ReadInt32();
            byte[] modulus = reader.ReadBytes(256);
            reader.ReadInt32();

            Client.SendPacketAsync(new S_0x2E_VersionCheck());
        }
    }
}
