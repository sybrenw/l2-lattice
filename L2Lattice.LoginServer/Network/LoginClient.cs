using L2Lattice.L2Core;
using L2Lattice.L2Core.Crypt;
using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.LoginServer.Crypt;
using L2Lattice.LoginServer.Enum;
using L2Lattice.LoginServer.Network.LoginPacket.Client;
using L2Lattice.LoginServer.Network.LoginPacket.Server;
using L2Lattice.LoginServer.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.LoginServer.Network
{
    public class LoginClient : NetworkClient
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<LoginClient>();

        /* Public redirected properties */
        public byte[] ScrambledModulus => RSAKeyPair?.ScrambledModulus;
        public byte[] BlowfishKey => _crypt?.Key;

        /* Public properties */
        public Session Session { get; }
        public L2KeyPair RSAKeyPair { get; }

        /* Private members */
        private LoginService _loginService;
        private LoginCrypt _crypt;

        internal LoginClient(Socket socket, LoginService loginService) : base(socket)
        {
            _loginService = loginService;

            // Create session
            Session = _loginService.CreateSession();

            // Setup encryption
            _crypt = new LoginCrypt(LoginCrypt.GenerateKey());
            RSAKeyPair = new L2KeyPair();
        }        

        protected override void Initialize()
        {
            Task sending = SendPacket(new S_0x00_SetEncryption());
            Task.WaitAll(sending);
        }

        protected override async Task HandlePacket(byte[] raw)
        {
            if (!_crypt.Decrypt(raw, 0, raw.Length))
            {
                Logger.LogWarning("Checksum failed for packet");
            }
            
            byte opcode = raw[0];
            ReceivablePacket<LoginClient> packet = SelectPacket(opcode);

            if (packet != null)
            {
                Logger.LogDebug("Received packet {0}", packet.GetType().Name);
                await packet.ReadAsync(this, raw);
            }
            else
            {
                Logger.LogWarning("Received packet with unknown opcode {0} and length {1}", opcode, raw.Length);
            }
        }

        public async Task SendPacket(SendablePacket<LoginClient> packet)
        {
            byte[] buffer;
            int length = packet.Write(this, out buffer);
            // Encrypt data block
            length = _crypt.Encrypt(buffer, 2, length - 2) + 2;
            
            // Copy header
            byte[] header = BitConverter.GetBytes((short)length);
            Array.Copy(header, 0, buffer, 0, 2);
            await SendPacketAsync(buffer, 0, length);
        }

        private ReceivablePacket<LoginClient> SelectPacket(byte opcode)
        {
            switch (opcode)
            {
                case C_0x00_RequestLogin.Opcode:
                    return new C_0x00_RequestLogin();
                case C_0x02_RequestServerLogin.Opcode:
                    return new C_0x02_RequestServerLogin();
                case C_0x05_RequestServerList.Opcode:
                    return new C_0x05_RequestServerList();
                case C_0x07_AuthGameGuard.Opcode:
                    return new C_0x07_AuthGameGuard();
            }

            return null;
        }

        public async Task TryLogin(string user, string password)
        {
            int accountId = await _loginService.Login(user, password, IpAddress);
            
            if (accountId > 0)
            {
                Session.AccountId = accountId;
                await SendPacket(new S_0x03_LoginOk());
            }
            else if (accountId == (int)LoginResult.Banned)
            {
                await SendPacket(new S_0x02_AccountBanned(0x20));
            }
            else 
            {
                await SendPacket(new S_0x01_LoginFail());
            }
        }
    }
}
