using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.L2Core.Crypt
{
    public class LoginCrypt
    {
        public static byte[] DEFAULT_KEY =
        {
            0x6b, 0x60, 0xcb, 0x5b, 0x82, 0xce, 0x90, 0xb1,
            0xcc, 0x2b, 0x6c, 0x55, 0x6c, 0x6c, 0x6c, 0x6c
        };
        
        private static L2Crypt DefaultCrypt { get; } = new L2Crypt(DEFAULT_KEY);
        private static Random Rnd { get; } = new Random();

        private L2Crypt _crypt = null;

        private bool _firstEncrypt = true;
        
        public LoginCrypt(byte[] key)
        {
            _crypt = new L2Crypt(key);
        }

        public int Encrypt(byte[] raw, int offset, int size)
        {
            // reserve checksum
            size += 4;

            if (_firstEncrypt)
            {
                // reserve for XOR "key"
                size += 4;

                // padding
                size += 8 - (size % 8);
                if ((offset + size) > raw.Length)
                    throw new IOException("Packet too long");

                L2Crypt.EncXORPass(raw, offset, size, Rnd.Next());
                DefaultCrypt.Encrypt(raw, offset, size);
                _firstEncrypt = false;
            }
            else
            {
                // padding
                size += 8 - (size % 8);
                if ((offset + size) > raw.Length)
                    throw new IOException("Packet too long");

                L2Crypt.AppendChecksum(raw, offset, size);
                _crypt.Encrypt(raw, offset, size);
            }

            return size;
        }

        public bool Decrypt(byte[] raw, int offset, int size)
        {
            if ((size % 8) != 0)
                throw new IOException("Size has to be multiple of 8");

            if ((offset + size) > raw.Length)
                throw new IOException("Index out of bounds");

            _crypt.Decrypt(raw, offset, size);
            return L2Crypt.VerifyChecksum(raw, offset, size);
        }
    }
}
