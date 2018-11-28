using Lattice.L2Core.Network.Packet;
using Lattice.LoginServer.Network.LoginPacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.LoginServer.Network.LoginPacket.Client
{
    internal class C_0x07_AuthGameGuard : ReceivablePacket<LoginClient>
    {
        public const byte Opcode = 0x07;

        protected override async Task ReadAsync(BinaryReader reader)
        {
            int session = reader.ReadInt32();

            if (Client.Session.Id == session)
            {
                await Client.SendPacketAsync(new S_0x0B_AuthGameGuard());
            }

        }
    }
}
