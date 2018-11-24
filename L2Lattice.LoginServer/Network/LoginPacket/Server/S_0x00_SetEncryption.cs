using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x00_SetEncryption : SendablePacket<LoginClient>
    {
        public const byte Opcode = 0x00;
        public const uint ProtocolVersion = 50721;
        
        public S_0x00_SetEncryption() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            // Session
            writer.Write(Client.Session.Id);
            // Protocol version
            writer.Write(ProtocolVersion);
            // rsa key
            writer.Write(Client.ScrambledModulus);
            // GG
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(Client.BlowfishKey); // BlowFish key
            writer.Write(4);

            // Padding
            writer.Write(0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
        }
    }
}
