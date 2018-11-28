using Lattice.L2Core;
using Lattice.L2Core.Crypt;
using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.LoginServer.Crypt;
using Lattice.LoginServer.Enum;
using Lattice.LoginServer.Network.LoginPacket.Client;
using Lattice.LoginServer.Network.LoginPacket.Server;
using Lattice.LoginServer.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.LoginServer.Network
{
    public class LoginClient : L2Client
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<LoginClient>();

        /* Public redirected properties */
        public byte[] ScrambledModulus => RSAKeyPair?.ScrambledModulus;
        public byte[] BlowfishKey => _crypt?.Key;

        /* Public properties */
        public Session Session { get; }
        public L2KeyPair RSAKeyPair { get; }

        /* Private members */
        private LoginCrypt _crypt;

        internal LoginClient(Socket socket) : base(socket)
        {
            // Create session
            Session = LoginService.Instance.CreateSession();

            // Setup encryption
            _crypt = new LoginCrypt(LoginCrypt.GenerateKey());
            RSAKeyPair = new L2KeyPair();
        }        

        protected override async Task Initialize()
        {
            await SendPacketAsync(new S_0x00_SetEncryption());
        }

        protected override int Encrypt(byte[] raw, int offset, int length)
        {
            return _crypt.Encrypt(raw, offset, length);
        }

        protected override void Decrypt(byte[] raw, int offset, int length)
        {
            if (!_crypt.Decrypt(raw, offset, length))
            {
                Logger.LogWarning("Checksum failed for packet");
            }
        }

        protected override async Task ProcessPacket(byte[] raw)
        {
            byte opcode = raw[0];

            ReceivablePacket<LoginClient> packet = SelectPacket(raw[0]);

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
    }
}
