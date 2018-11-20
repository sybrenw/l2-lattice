using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.PlayerServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFEEA00_ExSubClassInfo : SendablePacketBase<GameClient>
    {
        public const byte Opcode = 0xFE;
        public const ushort SecondaryOpcode = 0xEA00;

        public S_0xFEEA00_ExSubClassInfo() : base(Opcode,SecondaryOpcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            Player player = Client.GetPlayers()[0];
            // Current class
            writer.Write(player.VisibleClass);
            // Race
            writer.Write(player.Race);
            //Available classes
            writer.Write(1);

            // Loop
            // Slot
            writer.Write(0);
            // Class
            writer.Write(player.VisibleClass);
            // Level
            writer.Write(110);
            // Sub?
            writer.Write((byte)0);

        }
    }
}
