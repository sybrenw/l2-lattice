using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network
{
    public class DuplexPipe : IDuplexPipe
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<DuplexPipe>();

        const int MINIMUM_BUFFER_SIZE = 512;

        /* IDuplexPipe interface */
        public PipeReader Input => _inputPipe.Reader;
        public PipeWriter Output => _outputPipe.Writer;

        /* Private members */
        private Socket _socket;
        private Pipe _inputPipe;
        private Pipe _outputPipe;

        private int _minimumBufferSize;

        // Is the pipe open
        private bool _connected = true;

        public DuplexPipe(Socket socket) : this(socket, MINIMUM_BUFFER_SIZE) { }

        public DuplexPipe(Socket socket, int minimumBufferSize)
        {
            _socket = socket;
            _minimumBufferSize = minimumBufferSize;

            // Create input and output pipe
            _inputPipe = new Pipe();
            _outputPipe = new Pipe();
        }

        public async Task ProcessAsync()
        {
            Task receiving = ReceiveAsync(_socket, _inputPipe.Writer);
            Task sending = SendAsync(_socket, _outputPipe.Reader);
            await Task.WhenAll(receiving, sending);
        }

        private async Task ReceiveAsync(Socket socket, PipeWriter writer)
        {
            while (_connected)
            {
                Memory<byte> memory = writer.GetMemory(_minimumBufferSize);

                try
                {
                    int bytesRead = await socket.ReceiveAsync(memory, SocketFlags.None);

                    // Check if disconnected
                    if (bytesRead == 0)
                        break;
                                  
                    // Tell pipe how much was read
                    writer.Advance(bytesRead);
                }
                catch (Exception ex)
                {
                    Logger.LogTrace("Failed to receive message: {0}", ex);
                }

                // Flush data to the pipe
                FlushResult result = await writer.FlushAsync();

                // Socket closed
                if (result.IsCompleted)
                    break;
            }

            Logger.LogInformation("Receiving finished");
            // Cleanup
            writer.Complete();
        }

        private async Task SendAsync(Socket socket, PipeReader reader)
        {
            while (_connected)
            {
                ReadResult result = await reader.ReadAsync();

                if (result.IsCanceled || result.IsCompleted)
                    break;

                Logger.LogInformation("Sending packet");
                ReadOnlySequence<byte> buffer = result.Buffer;
                foreach(var segment in buffer)
                    await socket.SendAsync(segment, SocketFlags.None);
                Logger.LogInformation("Sent {0} bytes", buffer.Length);

                reader.AdvanceTo(buffer.End);
            }

            Logger.LogInformation("Sending finished");
            reader.Complete();
        }
    }
}
