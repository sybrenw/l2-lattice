using Lattice.L2Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lattice.L2Common.Database
{
    public class CharacterContext : DbContext
    {
        public DbSet<GameAccount> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=characters.db");
        }

        public List<Character> FindCharactersByAccountId(int accountId)
        {
            return Characters.Where(c => c.AccountId == accountId).ToList();
        }

        public GameAccount GetAccount(int accountId)
        {
            GameAccount account = Accounts.FirstOrDefault(a => a.AccountId == accountId);
            account.Characters = FindCharactersByAccountId(accountId);
            return null;
        }
    }
}
