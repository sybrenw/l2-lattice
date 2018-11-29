using Lattice.L2Common.Model;
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
    internal class C_0x11_RequestEnterWorld : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x11;

        public C_0x11_RequestEnterWorld()
        {

        }

        private Character[] _characters;

        protected override void Read(BinaryReader reader)
        {
            Client.SendPacketAsync(new S_0xFE_017C_ExChatEnterWorld());
            Client.SendPacketAsync(new S_0xFE_01C7_ExEnterWorld());
            Client.SendPacketAsync(new S_0x32_ExUserInfo());

            Client.SendPacketAsync(new S_0x40_UserAck()); 
            Client.SendPacketAsync(new S_0xFE_0060_ExBasicActionList());
        }
    }
}
