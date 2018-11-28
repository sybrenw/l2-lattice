using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Client
{
    internal class C_0x0F_MoveToLocation : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x0F;

        public C_0x0F_MoveToLocation()
        {

        }

        protected override void Read(BinaryReader reader)
        {
            int[] dest = new int[3];
            int[] pos = new int[3];

            for (int i = 0; i < 3; i++)
                dest[i] = reader.ReadInt32();
            for (int i = 0; i < 3; i++)
                pos[i] = reader.ReadInt32();
            int controller = reader.ReadInt32();

            Client.Broadcast(new S_0x2F_MoveToLocation(Client.Character.ObjectId,dest,pos));
        }
    }
}
