using L2Lattice.L2Core.Crypt;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network
{
    public abstract class NetworkClient : IDisposable
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<NetworkClient>();

        private static Random rnd = new Random();

        /* Constants */
        const int MINIUM_BUFFER_SIZE = 1024;

        /* Private members */
        private Socket _socket;
        private DuplexPipe _pipe;
        
        // Socket is connected
        private bool _connected = true;
        
        public NetworkClient(Socket socket)
        {
            _socket = socket;
            _pipe = new DuplexPipe(_socket);
        }

        public NetworkClient(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync()
        {
            Task process = _pipe.ProcessAsync();
            Task reading = ReadAsync(_pipe.Input);
            Initialize();
            await Task.WhenAll(process, reading);
        }
        
        private async Task ReadAsync(PipeReader reader)
        {
            while(_connected)
            {
                ReadResult result = await reader.ReadAsync();
                
                Logger.LogInformation("Received packet");

                if (result.IsCanceled || result.IsCompleted)
                    break;

                ReadOnlySequence<byte> buffer = result.Buffer;
                SequencePosition? position = buffer.Start;
                
                if (buffer.Length > 2)
                {
                    var header = buffer.Slice(position.Value, 2);
                    int dataLength = BitConverter.ToUInt16(header.First.Span) - 2;

                    position = buffer.GetPosition(2, position.Value);

                    if (buffer.Length >= 2 + dataLength)
                    {
                        var data = buffer.Slice(position.Value, dataLength);
                        byte[] raw = data.ToArray();
                        HandlePacket(raw);

                        position = buffer.GetPosition(dataLength, position.Value);
                        reader.AdvanceTo(data.End);
                    }
                    else
                    {
                        reader.AdvanceTo(header.End);
                    }
                }
                else
                {
                    reader.AdvanceTo(buffer.End);
                }

            }

            Logger.LogInformation("Reading finished");
        }
               
        protected async Task SendPacketAsync(byte[] raw, int offset, int size)
        {
            await _pipe.Output.WriteAsync(new ReadOnlyMemory<byte>(raw, offset, size));
            await _pipe.Output.FlushAsync();
        }

        public void Disconnect()
        {
            _connected = false;
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }

        /* Abstract functions */
        protected abstract void HandlePacket(byte[] raw);
        protected abstract void Initialize();

    }
}
