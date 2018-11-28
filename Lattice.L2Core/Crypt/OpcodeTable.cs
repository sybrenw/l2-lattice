using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2Core.Crypt
{
    public class OpcodeTable
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<OpcodeTable>();

        private bool _initialized;
        private long _seed;

        private byte[] _encodeTable1;
        private byte[] _decodeTable1;

        private ushort[] _encodeTable2;
        private ushort[] _decodeTable2;

        private OpcodeTableConfig _config;




        public OpcodeTable(OpcodeTableConfig config)
        {
            _config = config;

            // Initialize tables
            _decodeTable1 = new byte[_config.PrimaryCount + 1];
            _encodeTable1 = new byte[_config.PrimaryCount + 1];
            _decodeTable2 = new ushort[_config.SecondaryCount + 1];
            _encodeTable2 = new ushort[_config.SecondaryCount + 1];
        }
        
        public void Init(long seed)
        {
            // Set seed
            _seed = seed;

            // Fill decode tables
            for (int i = 0; i < _decodeTable1.Length; i++)
                _decodeTable1[i] = (byte)i;

            for (int i = 0; i < _decodeTable2.Length; i++)
                _decodeTable2[i] = (ushort)i;

            // Do stuff
            if (_seed != 0)
            {


                // Fix constant primary opcodes
                foreach(byte opcode in _config.PrimaryConstant)
                {
                    int cpos = 0;
                    while (_decodeTable1[cpos] != opcode)
                        cpos++;
                    _decodeTable1[cpos] = _decodeTable1[opcode];
                    _decodeTable1[opcode] = opcode;
                }

                // Fix constant secondary opcodes
                foreach (ushort opcode in _config.SecondaryConstant)
                {
                    int cpos = 0;
                    while (_decodeTable2[cpos] != opcode)
                        cpos++;
                    _decodeTable2[cpos] = _decodeTable2[opcode];
                    _decodeTable2[opcode] = opcode;
                }
            }

            // Fill encode tables
            for (int i = 0; i < _decodeTable1.Length; i++)
            {
                int idx = _encodeTable1[i] & 0xFF;
                _decodeTable1[idx] = (byte)i;
            }

            for (int i = 0; i < _decodeTable2.Length; i++)
            {
                int idx = _encodeTable2[i] & 0xFFFF;
                _decodeTable2[idx] = (ushort)i;
            }


            _initialized = true;
        }

        public byte EncodePrimary(byte value)
        {
            if (value > _encodeTable1.Length)
            {
                Logger.LogWarning("Increase primary opcode table size to at least: " + value);
                return value;
            }

            return _encodeTable1[value];
        }

        public byte DecodePrimary(byte value)
        {
            if (value > _decodeTable1.Length)
            {
                Logger.LogWarning("Increase primary opcode table size to at least: " + value);
                return value;
            }

            return _decodeTable1[value];
        }

        public ushort EncodeSecondary(byte[] raw, int offset)
        {
            ushort value = BitConverter.ToUInt16(raw, offset);

            if (value > _encodeTable2.Length)
            {
                Logger.LogWarning("Increase secondary opcode table size to at least: " + value);
                return value;
            }

            return _encodeTable2[value];
        }

        public ushort DecodeSecondary(byte[] raw, int offset)
        {
            ushort value = BitConverter.ToUInt16(raw, offset);

            if (value > _decodeTable2.Length)
            {
                Logger.LogWarning("Increase secondary opcode table size to at least: " + value);
                return value;
            }

            return _decodeTable2[value];
        }

        public class OpcodeTableConfig
        {
            public byte[] PrimaryConstant = new byte[]
            {
                0x11, 0x12, 0xB1, 0xD0
            };

            public ushort[] SecondaryConstant = new ushort[]
            {
                0x70, 0x71
            };

            public int PrimaryCount = 128;
            public int SecondaryCount = 353;
        }
    }
}
