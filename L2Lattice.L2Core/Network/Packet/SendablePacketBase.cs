using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.L2Core.Network.Packet
{
    public abstract class SendablePacketBase<T> : ISendablePacket where T : NetworkClient
    {
        public T Client { get; private set; }

        private byte[] _opcodes;
                
        public SendablePacketBase(params byte[] opcodes)
        {
            _opcodes = opcodes;
        }

        public int Write(T client, out byte[] buffer)
        {            
            Client = client;
            int length = 0;
            using (MemoryStream stream = new MemoryStream(new byte[1024]))
            using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Unicode))
            {
                // Reserve header (size)
                writer.Write((short)0);
                // Write opcode
                writer.Write(_opcodes);
                Write(writer);
                writer.Flush();
                length = (int) stream.Position;
                buffer = stream.ToArray();
            }

            return length;
        }

        public abstract void Write(BinaryWriter writer);

        public void WriteString(BinaryWriter writer, string text)
        {
            writer.Write(Encoding.Unicode.GetBytes(text));
            writer.Write((short)0);
        }

        public void WriteString2(BinaryWriter writer, string text)
        {
            writer.Write((short)text.Length);
            writer.Write(Encoding.Unicode.GetBytes(text));
        }
    }
}
