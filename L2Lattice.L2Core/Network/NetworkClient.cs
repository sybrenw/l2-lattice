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
            try
            {
                while (_connected)
                {
                    ReadResult result = await reader.ReadAsync();

                    Logger.LogInformation("Received packet");

                    if (result.IsCanceled || result.IsCompleted)
                        break;

                    ReadOnlySequence<byte> buffer = result.Buffer;
                    SequencePosition consumed = buffer.Start;

                    long position = 0;

                    while (position + 2 < buffer.Length)
                    {
                        var header = buffer.Slice(position, 2);
                        int dataLength = BitConverter.ToUInt16(header.First.Span) - 2;

                        position += 2;

                        // Check if all data is in buffer
                        if (position + dataLength > buffer.Length)
                            break;

                        // Extract data
                        var data = buffer.Slice(position, dataLength);
                        byte[] raw = data.ToArray();
                        HandlePacket(raw);

                        // Update position
                        position += dataLength;

                        // Set consumed to package end
                        consumed = data.End;
                    }

                    reader.AdvanceTo(consumed, buffer.End);

                }
            }
            catch(Exception e)
            {
                Logger.LogError("Reading failed: {0}", e);
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
