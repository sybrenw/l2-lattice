using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2Core.Network.Packet
{
    public abstract class SendablePacket<T> : ISendablePacket<T> where T : L2Client
    {
        public T Client { get; private set; }

        private byte[] _opcodes;
                
        public SendablePacket(params byte[] opcodes)
        {
            _opcodes = opcodes;
        }

        public int Write(T client, out byte[] buffer)
        {            
            Client = client;
            int length = 0;
            using (MemoryStream stream = new MemoryStream(1024))
            using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Unicode))
            {
                // Write opcode
                writer.Write(_opcodes);
                Write(writer);
                writer.Flush();
                length = (int) stream.Position;
                // Pad some extra 
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                buffer = stream.ToArray();
            }

            return length;
        }

        int ISendablePacket.Write(L2Client client, out byte[] buffer)
        {
            return Write(client as T, out buffer);
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
