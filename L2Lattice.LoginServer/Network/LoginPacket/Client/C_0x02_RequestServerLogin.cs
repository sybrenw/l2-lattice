using L2Lattice.L2Core.Network.Packet;
using L2Lattice.LoginServer.Network.LoginPacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.LoginServer.Network.LoginPacket.Client
{
    internal class C_0x02_RequestServerLogin : ReceivablePacket<LoginClient>
    {
        public const byte Opcode = 0x02;

        protected override async Task ReadAsync(BinaryReader reader)
        {
            int accountId = reader.ReadInt32();
            int authKey = reader.ReadInt32();
            byte server = reader.ReadByte();

            if (Client.Session.Verify(accountId, authKey))
            {
                await Client.SendPacket(new S_0x07_PlayOk(server));
            }
        }
    }
}
