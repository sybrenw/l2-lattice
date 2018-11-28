using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.L2PlayerServer.Network.GamePacket.Client
{
    internal class C_0x13_RequestNewCharacter : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x13;

        public C_0x13_RequestNewCharacter()
        {

        }

        protected override async Task ReadAsync(BinaryReader reader)
        {
            Client.SendPacketAsync(new S_0x0D_NewCharacterSuccess());
        }
    }
}
