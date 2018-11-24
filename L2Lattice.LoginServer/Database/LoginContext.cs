using L2Lattice.LoginServer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.LoginServer.Database
{
    internal class LoginContext : DbContext
    {
        internal DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=login.db");
        }

        public async Task<Account> GetAccount(string username)
        {
            return await Accounts.FirstOrDefaultAsync<Account>(p => p.Username == username);
        }
    }
}
