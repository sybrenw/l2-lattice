﻿using L2Lattice.L2Core.Crypt;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network
{
    public abstract class NetworkClient : INetworkClient, IDisposable
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<NetworkClient>();

        private static Random rnd = new Random();

        /* Constants */
        const int MINIUM_BUFFER_SIZE = 1024;

        /* Private members */
        private Socket _socket;
        private DuplexPipe _pipe;

        // Socket is connected
        public bool Connected { get; private set; } = true;

        public string IpAddress { get; }
        
        public NetworkClient() { }

        public NetworkClient(Socket socket)
        {
            _socket = socket;
            _pipe = new DuplexPipe(_socket);

            // Get IP Address
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            if (endPoint != null)
                IpAddress = endPoint.Address.ToString();
        }

        public async Task ConnectAsync(string ip, int port)
        {
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            while (Connected)
            {
                try
                {
                    await _socket.ConnectAsync(ip, port);
                    _pipe = new DuplexPipe(_socket);
                    break;
                }
                catch (Exception ex)
                {
                    Logger.LogError("Failed to connect: {0}", ex);
                    await Task.Delay(5000);
                }
            }
            await ProcessAsync();
        }

        public async Task ProcessAsync()
        {
            Task process = _pipe.ProcessAsync();
            Task reading = ReadAsync(_pipe.Input);
            Task init = Initialize();
            await Task.WhenAll(process, reading, init);
        }
        
        private async Task ReadAsync(PipeReader reader)
        {
            try
            {
                while (Connected)
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
                        await HandlePacket(raw);

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

        protected async Task SendPacketAsync(byte[] header, byte[] raw, int offset, int size)
        {
            await _pipe.Output.WriteAsync(new ReadOnlyMemory<byte>(header));
            await _pipe.Output.WriteAsync(new ReadOnlyMemory<byte>(raw, offset, size));
            await _pipe.Output.FlushAsync();
        }

        public void Disconnect()
        {
            Connected = false;
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }

        /* Abstract functions */
        protected abstract Task HandlePacket(byte[] raw);
        protected abstract Task Initialize();

    }
}
