using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Client
{
    internal class C_0x11_RequestEnterWorld : ReceivablePacketBase<GameClient>
    {
        public const byte Opcode = 0x11;

        public C_0x11_RequestEnterWorld()
        {

        }

        public override void Read(BinaryReader reader)
        {
            Client.SendPacket(new S_0xFE7C01_ExChatEnterWorld());
            Client.SendPacket(new S_0xFEC701_ExEnterWorld());
        }
    }
}
