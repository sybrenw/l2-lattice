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
    internal class C_0x00_Logout : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x00;

        public C_0x00_Logout()
        {

        }

        protected override async Task ReadAsync(BinaryReader reader)
        {
            await Client.SendPacketAsync(new S_0x00_LogoutOk());
            Client.Disconnect();
        }
    }
}
