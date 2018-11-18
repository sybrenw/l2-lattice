using System;
using System.Collections.Generic;
using System.Text;

namespace L2Lattice.L2Core.Crypt
{
    public class L2Crypt
    {
        private byte[] _key;
        private BlowfishCipher _cipher;

        public L2Crypt(byte[] key)
        {
            _key = key;
            _cipher = new BlowfishCipher(key);
        }

        public L2Crypt(string key) : this(Encoding.ASCII.GetBytes(key)) { }

        public bool VerifyChecksum(byte[] raw)
        {
            return VerifyChecksum(raw, 0, raw.Length);
        }

        public byte[] AppendChecksum(byte[] raw)
        {
            return AppendChecksum(raw, 0, raw.Length);
        }

        public byte[] EncXORPass(byte[] raw, int key)
        {
            return EncXORPass(raw, 0, raw.Length, key);
        }

        public void Encrypt(byte[] raw, int offset, int size)
        {
            for (int i = offset; i < (offset + size); i += 8)
            {
                _cipher.Encipher(raw, i);
            }
        }

        public void Decrypt(byte[] raw, int offset, int size)
        {
            for (int i = offset; i < (offset + size); i += 8)
            {
                _cipher.Decipher(raw, i);
            }
        }

        public static bool VerifyChecksum(byte[] raw, int offset, int size)
        {
            // check if size is multiple of 4 and if there is more then only the checksum
            if (((size & 3) != 0) || (size <= 4))
                return false;

            long chksum = 0;
            int count = size - 4;
            long check = -1;
            int i;

            for (i = offset; i < count; i += 4)
            {
                check = raw[i] & 0xff;
                check |= (uint)(raw[i + 1] << 8) & 0xff00;
                check |= (uint)(raw[i + 2] << 0x10) & 0xff0000;
                check |= (uint)(raw[i + 3] << 0x18) & 0xff000000;

                chksum ^= check;
            }

            check = raw[i] & 0xff;
            check |= (uint)(raw[i + 1] << 8) & 0xff00;
            check |= (uint)(raw[i + 2] << 0x10) & 0xff0000;
            check |= (uint)(raw[i + 3] << 0x18) & 0xff000000;

            return check == chksum;
        }

        public static byte[] AppendChecksum(byte[] raw, int offset, int size)
        {
            long chksum = 0;
            int count = size - 4;
            long ecx;
            int i;

            for (i = offset; i < count; i += 4)
            {
                ecx = raw[i] & 0xff;
                ecx |= (uint)(raw[i + 1] << 8) & 0xff00;
                ecx |= (uint)(raw[i + 2] << 0x10) & 0xff0000;
                ecx |= (uint)(raw[i + 3] << 0x18) & 0xff000000;

                chksum ^= ecx;
            }

            ecx = raw[i] & 0xff;
            ecx |= (uint)(raw[i + 1] << 8) & 0xff00;
            ecx |= (uint)(raw[i + 2] << 0x10) & 0xff0000;
            ecx |= (uint)(raw[i + 3] << 0x18) & 0xff000000;

            raw[i] = (byte)(chksum & 0xff);
            raw[i + 1] = (byte)((chksum >> 0x08) & 0xff);
            raw[i + 2] = (byte)((chksum >> 0x10) & 0xff);
            raw[i + 3] = (byte)((chksum >> 0x18) & 0xff);
            return raw;
        }

        public static byte[] EncXORPass(byte[] raw, int offset, int size, int key)
        {
            int stop = size - 8;
            int pos = 4 + offset;
            int edx;
            int ecx = key; // Initial xor key

            while (pos < stop)
            {
                edx = (raw[pos] & 0xFF);
                edx |= (raw[pos + 1] & 0xFF) << 8;
                edx |= (raw[pos + 2] & 0xFF) << 16;
                edx |= (raw[pos + 3] & 0xFF) << 24;

                ecx += edx;

                edx ^= ecx;

                raw[pos++] = (byte)(edx & 0xFF);
                raw[pos++] = (byte)((edx >> 8) & 0xFF);
                raw[pos++] = (byte)((edx >> 16) & 0xFF);
                raw[pos++] = (byte)((edx >> 24) & 0xFF);
            }

            raw[pos++] = (byte)(ecx & 0xFF);
            raw[pos++] = (byte)((ecx >> 8) & 0xFF);
            raw[pos++] = (byte)((ecx >> 16) & 0xFF);
            raw[pos++] = (byte)((ecx >> 24) & 0xFF);
            return raw;
        }
    }
}
