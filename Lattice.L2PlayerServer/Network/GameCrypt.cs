using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2PlayerServer.Network
{
    public class GameCrypt
    {
        private static Random Rnd { get; } = new Random();

        private byte[] _inKey = new byte[16];
        private byte[] _outKey = new byte[16];
        private bool _isEnabled;

        public byte[] Key { get; } = new byte[8];

        public GameCrypt(byte[] key)
        {
            Array.Copy(key, 0, _inKey, 0, 16);
            Array.Copy(key, 0, _outKey, 0, 16);
            Array.Copy(key, 0, Key, 0, 8);
        }


        public void Decrypt(byte[] raw, int offset, int size)
        {
            if (!_isEnabled)
            {
                return;
            }

            int temp = 0;
            for (int i = 0; i < size; i++)
            {
                int temp2 = raw[offset + i] & 0xFF;
                raw[offset + i] = (byte)(temp2 ^ _inKey[i & 15] ^ temp);
                temp = temp2;
            }
                       
            int old = _inKey[8] & 0xFF;
            old |= (_inKey[9] & 0xFF) << 0x08;
            old |= (_inKey[10] & 0xFF) << 0x10;
            old |= (_inKey[11] & 0xFF) << 0x18;

            old += size;

            _inKey[8] = (byte)(old & 0xff);
            _inKey[9] = (byte)((old >> 0x08) & 0xff);
            _inKey[10] = (byte)((old >> 0x10) & 0xff);
            _inKey[11] = (byte)((old >> 0x18) & 0xff);
        }

        public void Encrypt(byte[] raw, int offset, int size)
        {
            if (!_isEnabled)
            {
                _isEnabled = true;
                return;
            }

            int temp = 0;
            for (int i = 0; i < size; i++)
            {
                int temp2 = raw[offset + i] & 0xFF;
                temp = temp2 ^ _outKey[i & 15] ^ temp;
                raw[offset + i] = (byte)temp;
            }

            int old = _outKey[8] & 0xFF;
            old |= (_outKey[9] & 0xFF) << 0x08;
            old |= (_outKey[10] & 0xFF) << 0x10;
            old |= (_outKey[11] & 0xFF) << 0x18;

            old += size;

            _outKey[8] = (byte)(old & 0xff);
            _outKey[9] = (byte)((old >> 0x08) & 0xff);
            _outKey[10] = (byte)((old >> 0x10) & 0xff);
            _outKey[11] = (byte)((old >> 0x18) & 0xff);
        }

        public static byte[] GenerateKey()
        {
            byte[] key = new byte[16];
            // randomize the 8 first bytes
            for (int i = 0; i < 16; i++)
            {
                key[i] = (byte)Rnd.Next(255);
            }

            // the last 8 bytes are static
            key[8] = (byte)0xc8;
            key[9] = (byte)0x27;
            key[10] = (byte)0x93;
            key[11] = (byte)0x01;
            key[12] = (byte)0xa1;
            key[13] = (byte)0x6c;
            key[14] = (byte)0x31;
            key[15] = (byte)0x97;
            return key;
        }
    }
}
