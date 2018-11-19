using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Client
{
    internal class C_0x2B_RequestLogin : ReceivablePacketBase<GameClient>
    {
        public const byte Opcode = 0x2B;
               
        public C_0x2B_RequestLogin()
        {

        }

        public override void Read(BinaryReader reader)
        {
            string accountName = ReadString(reader);
            int accountId = reader.ReadInt32();
            int sessionId = reader.ReadInt32();
            int accountId2 = reader.ReadInt32();
            int authKey = reader.ReadInt32();
                        
            Client.AccountId = accountId;
            Client.SendPacket(new S_0x0A_LoginResult());
            Client.SendPacket(new S_0x09_CharacterSelectionInfo());
        }

        /* // Opcode
         * 49 
         * // Unknown
         * 00 64 00 45
         * 00 6D 00 70 
         * 00 74 00 79 
         * 00 00 00 
         * // Account Id
         * 90 BA 9D 00 
         * // Session Id (from login)
         * A0 99 00 00 
         * // Account Id (again)
         * 90 BA 9D 00 
         * // Auth key (login)
         * EE 11 DB 2B 
         * 
         * 01 00 00 00 
         * 97 02 00 00 
         * // Padding?
         * 00 00 00 00 00 00 00 00
         * 
         */
    }
}
