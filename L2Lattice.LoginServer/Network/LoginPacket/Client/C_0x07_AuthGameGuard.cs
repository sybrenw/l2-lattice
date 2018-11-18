using L2Lattice.L2Core.Network.Packet;
using L2Lattice.LoginServer.Network.LoginPacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.LoginServer.Network.LoginPacket.Client
{
    internal class C_0x07_AuthGameGuard : ReceivablePacketBase<LoginClient>
    {
        public const byte Opcode = 0x07;

        public override void Read(BinaryReader reader)
        {
            int session = reader.ReadInt32();

            if (Client.Session.Id == session)
            {
                Client.SendPacket(new S_0x0B_AuthGameGuard());
            }

        }
    }
}
