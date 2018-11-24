using L2Lattice.L2Core.IdFactory;
using L2Lattice.LoginServer.Database;
using L2Lattice.LoginServer.Enum;
using L2Lattice.LoginServer.Model;
using L2Lattice.LoginServer.Network;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.LoginServer.Service
{
    internal class LoginService
    {

        private readonly Random Rnd = new Random(new DateTime().Millisecond);

        private SimpleIdFactory _idFactory;


        public LoginService()
        {
            _idFactory = new SimpleIdFactory();

            // Initialize database
            using(var db = new LoginContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public Session CreateSession()
        {
            Session session = new Session(_idFactory.Create())
            {
                LoginAuthKey = Rnd.Next()
            };
            return session;
        }

        public async Task<int> Login(string username, string password, string ipAddress)
        {
            Account account= null;
            using (var db = new LoginContext())
            {
                try
                {
                    account = await db.GetAccount(username);
                }
                catch(Exception ex)
                {

                }

                if (account == null)
                {
                    account = CreateAccount(username, password);
                    account.LastIp = ipAddress;
                    await db.AddAsync(account);
                    await db.SaveChangesAsync();
                    return account.Id;
                }
                else if (account.AccessLevel < 0)
                {
                    return (int)LoginResult.Banned;
                }
                else
                {
                    string hashed = HashPassword(password, account.Salt);

                    if (account.Password == hashed)
                    {
                        account.LastIp = ipAddress;                        
                        await db.SaveChangesAsync();
                        return account.Id;
                    }
                }
            }

            return (int)LoginResult.LoginFail;
        }

        private static Account CreateAccount(string username, string password)
        {
            Account account = new Account() { Username = username };
            
            // Create salt and hash password
            account.Salt = CreateSalt();
            account.Password = HashPassword(password, account.Salt);

            return account;
        }


        private static string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        private static string CreateSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }

    }
}
