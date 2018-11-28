using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Lattice.L2Core.Crypt
{
    public class L2KeyPair
    {
        public const uint PUBLIC_EXPONENT = 65537;

        private RSAParameters _parameters;

        private AsymmetricCipherKeyPair _keys;

        private RsaKeyParameters _publicKey;
        private RsaKeyParameters _privateKey;

        public byte[] ScrambledModulus { get; }

        public L2KeyPair()
        {
            RsaKeyPairGenerator r = new RsaKeyPairGenerator();
            r.Init(new KeyGenerationParameters(new SecureRandom(), 1024));
            _keys = r.GenerateKeyPair();
            _publicKey = _keys.Public as RsaKeyParameters;
            _privateKey = _keys.Private as RsaKeyParameters;

            
            ScrambledModulus = ScrambleModulus(_publicKey.Modulus.ToByteArray());
        }


        public byte[] Decrypt(byte[] raw)
        {
            RsaEngine eng = new RsaEngine();
            eng.Init(false, _privateKey);
            return eng.ProcessBlock(raw, 0, raw.Length);
        }

        public byte[] Decrypt2(byte[] raw)
        {
            BigInteger n = PrepareBigInteger(_parameters.Modulus);
            BigInteger d = PrepareBigInteger(_parameters.D);
            BigInteger msg = PrepareBigInteger(raw);
            BigInteger result = BigInteger.ModPow(msg, d, n);
            return result.ToByteArray();
        }
        
        private static BigInteger PrepareBigInteger(byte[] unsignedBigEndian)
        {
            // Leave an extra 0x00 byte so that the sign bit is clear
            byte[] tmp = new byte[unsignedBigEndian.Length + 1];
            Buffer.BlockCopy(unsignedBigEndian, 0, tmp, 1, unsignedBigEndian.Length);
            Array.Reverse(tmp);
            return new BigInteger(tmp);
        }

        private static byte[] ScrambleModulus(byte[] modulus)
        {
            byte[] scrambled = new byte[modulus.Length];
            Array.Copy(modulus, scrambled, modulus.Length);

            if ((scrambled.Length == 0x81) && (scrambled[0] == 0x00))
            {
                byte[] temp = new byte[0x80];
                Array.Copy(scrambled, 1, temp, 0, 0x80);
                scrambled = temp;
            }
            // step 1 : 0x4d-0x50 <-> 0x00-0x04
            for (int i = 0; i < 4; i++)
            {
                byte temp = scrambled[0x00 + i];
                scrambled[0x00 + i] = scrambled[0x4d + i];
                scrambled[0x4d + i] = temp;
            }
            // step 2 : xor first 0x40 bytes with last 0x40 bytes
            for (int i = 0; i < 0x40; i++)
            {
                scrambled[i] = (byte)(scrambled[i] ^ scrambled[0x40 + i]);
            }
            // step 3 : xor bytes 0x0d-0x10 with bytes 0x34-0x38
            for (int i = 0; i < 4; i++)
            {
                scrambled[0x0d + i] = (byte)(scrambled[0x0d + i] ^ scrambled[0x34 + i]);
            }
            // step 4 : xor last 0x40 bytes with first 0x40 bytes
            for (int i = 0; i < 0x40; i++)
            {
                scrambled[0x40 + i] = (byte)(scrambled[0x40 + i] ^ scrambled[i]);
            }

            return scrambled;
        }
    }
}
