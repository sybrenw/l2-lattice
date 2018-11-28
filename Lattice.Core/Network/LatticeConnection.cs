using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.Core.Network
{
    public abstract class LatticeConnection : NetworkConnection
    {
        private const int HEADER_SIZE = 4;

        public LatticeConnection() { }

        public LatticeConnection(Socket socket) : base(socket) { }

        protected override async Task HandlePacket(byte[] raw)
        {
            // First two bytes are reserved for opcode
            short opcode = BitConverter.ToInt16(raw, 0);
            // Process packet
            await ProcessPacket(opcode, raw, 2, raw.Length - 2);
        }

        protected async Task SendPacketAsync(byte opcode, IMessage msg)
        {
            byte[] bytes = msg.ToByteArray();
            byte[] header = BitConverter.GetBytes(bytes.Length + HEADER_SIZE);
            header[2] = opcode;
            await SendPacketAsync(header, bytes, 0, bytes.Length);
        }

        protected static T ReadMessage<T>(byte[] raw) where T : IMessage, new()
        {            
            T message = new T();
            message.MergeFrom(raw, 2, raw.Length -2);
            return message;
        }

        protected static T ReadMessage<T>(byte[] raw, int offset, int size) where T : IMessage, new()
        {
            T message = new T();
            message.MergeFrom(raw, offset, size);
            return message;
        }

        protected abstract Task ProcessPacket(short opcode, byte[] raw, int rawOffset, int rawLength);
    }
}
