using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.LoginServer.Network.LoginPacket.Client
{
    internal class C_0x00_RequestLogin : ReceivablePacketBase<LoginClient>
    {
        public const byte Opcode = 0x00;

        public override void Read(BinaryReader reader)
        {
            byte[] userRaw = reader.ReadBytes(128);
            byte[] passRaw = reader.ReadBytes(128);
            int session = reader.ReadInt32();

            if (session == Client.Session.Id)
            {
                userRaw = Client.RSAKeyPair.Decrypt(userRaw);
                passRaw = Client.RSAKeyPair.Decrypt(passRaw);
                
                string user = Encoding.ASCII.GetString(userRaw, 1, userRaw.Length - 1).Trim('\0').ToLower();
                string password = Encoding.ASCII.GetString(passRaw, 1, passRaw.Length - 1).Trim('\0').ToLower();

                Client.TryLogin(user, password);
            }
        }

    }
}
