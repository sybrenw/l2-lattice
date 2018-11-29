using Lattice.L2Core.Network.Packet;
using Lattice.Core.Network;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.L2Core.Network
{
    public abstract class L2Client : NetworkConnection
    {
        public L2Client(Socket socket) : base(socket)
        {

        }

        protected override async Task HandlePacket(byte[] raw)
        {
            // Decrypt
            Decrypt(raw, 0, raw.Length);
            try
            {
                await ProcessPacket(raw);
            }
            catch(Exception ex)
            {

            }
        }
        
        public async Task SendPacketAsync(ISendablePacket packet)
        {
            byte[] buffer;
            int length = packet.Write(this, out buffer);
            // Encrypt data block
            length = Encrypt(buffer, 0, length);

            // Create header
            byte[] header = BitConverter.GetBytes((short)(length + 2));

            // Send packet
            try
            {
                await SendPacketAsync(header, buffer, 0, length);
            }
            catch(Exception ex)
            {

            }
        }

        public async Task SendPacketAsync(byte[] raw, int rawLength)
        {
            byte[] raw2 = new byte[rawLength];
            Array.Copy(raw, raw2, rawLength);

            // Encrypt data block
            rawLength = Encrypt(raw2, 0, rawLength);

            // Create header
            byte[] header = BitConverter.GetBytes((short)(rawLength + 2));

            // Send packet
            try
            {
                await SendPacketAsync(header, raw2, 0, rawLength);
            }
            catch (Exception ex)
            {

            }
        }

        protected abstract override Task Initialize();
        protected abstract Task ProcessPacket(byte[] raw);
        protected abstract int Encrypt(byte[] raw, int offset, int length);
        protected abstract void Decrypt(byte[] raw, int offset, int length);
    }
}
