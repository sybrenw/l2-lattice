using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0x2E_VersionCheck : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x2E;

        public S_0x2E_VersionCheck() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(true);
            writer.Write(Client.Key);
            writer.Write(1);
            writer.Write(1);
            writer.Write(true);
            // For now no obfuscation
            writer.Write(0);
            // classic
            writer.Write(false);
            // arena
            writer.Write(false);
        }
    }
}
