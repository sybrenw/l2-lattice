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

            Character[] characters = new Character[1600];
            for (int i = 0; i < 1600; i++)
            {
                int ix = i % 40;
                int iy = (i - ix) / 40;

                Character c = new Character();
                c.Name = "Kerzouner";
                c.ObjectId = 100 + i;
                c.Race = 0;
                c.Sex = 0;
                c.VisibleClassId = 90;
                c.Position = new System.Numerics.Vector3(145980 + (ix - 20) * 15, 13448 - iy * 15, -1151);
                Client.SendPacketAsync(new S_0x31_CharInfo(c));

                characters[i] = c;
            }

            _characters = characters;


            Task.Run(async () => {
                await Task.Delay(10000);
                Random rnd = new Random();
                while (Client.Connected)
                {

                    for (int i = 0; i < 1600; i++)
                    {
                        Character c = _characters[i];
                        int[] pos = new int[] { (int)c.Position.X, (int)c.Position.Y, (int)c.Position.Z };
                        int[] dest = new int[] { (int)c.Position.X + rnd.Next(-100,100), (int)c.Position.Y + rnd.Next(-100, 100), (int)c.Position.Z };
                        c.Position = new System.Numerics.Vector3(dest[0], dest[1], dest[2]);
                        Client.SendPacketAsync(new S_0x2F_MoveToLocation(c.ObjectId, dest, pos));
                       // Client.SendPacketAsync(new S_0x27_SocialAction(100 + i, 29));
                    }
                    await Task.Delay(5000);
                }
            });
            
        }
    }
}
