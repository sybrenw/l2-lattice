using L2Lattice.L2Core.Network.Packet;
using L2Lattice.LoginServer.Enum;
using L2Lattice.LoginServer.Network.LoginPacket.Server;
using L2Lattice.LoginServer.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.LoginServer.Network.LoginPacket.Client
{
    internal class C_0x00_RequestLogin : ReceivablePacket<LoginClient>
    {
        public const byte Opcode = 0x00;

        protected override async Task ReadAsync(BinaryReader reader)
        {
            byte[] userRaw = reader.ReadBytes(128);
            byte[] passRaw = reader.ReadBytes(128);
            int session = reader.ReadInt32();

            // Verify session id
            if (session != Client.Session.Id)
            {
                await Client.SendPacket(new S_0x01_LoginFail());
                return;
            }

            // Decrypt user and password
            userRaw = Client.RSAKeyPair.Decrypt(userRaw);
            passRaw = Client.RSAKeyPair.Decrypt(passRaw);

            // Remove the padding
            string user = Encoding.ASCII.GetString(userRaw, 1, userRaw.Length - 1).Trim('\0').ToLower();
            string password = Encoding.ASCII.GetString(passRaw, 1, passRaw.Length - 1).Trim('\0').ToLower();

            // Try to authentica
            int accountId = await LoginService.Instance.Login(Client, user, password);

            if (accountId > 0)
            {
                // Authentication success
                Client.Session.AccountId = accountId;
                Client.Session.State = LoginState.Authed;
                await Client.SendPacket(new S_0x03_LoginOk());
            }
            else if (accountId == (int)LoginResult.Banned)
            {
                // Account is banned
                await Client.SendPacket(new S_0x02_AccountBanned(0x20));
            }
            else
            {
                // Login failed
                await Client.SendPacket(new S_0x01_LoginFail());
            }
        }

    }
}
