using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network
{
    public abstract class LatticeClient : NetworkClient
    {
        private const int HEADER_SIZE = 4;

        public LatticeClient() { }

        public LatticeClient(Socket socket) : base(socket) { }
                               
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

    }
}
