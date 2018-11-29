using Lattice.L2PlayerServer.Network;
using Lattice.L2Common.Database;
using Lattice.L2Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Lattice.L2PlayerServer.Service
{
    public class CharacterService
    {
        private static CharacterService _instance;

        public static CharacterService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CharacterService();

                return _instance;
            }
        }

        private ConcurrentBag<Character> _characters = new ConcurrentBag<Character>();
                       
        private CharacterService()
        {
            using (var db = new CharacterContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public List<Character> GetCharacters(int accountId)
        {
            using (var db = new CharacterContext())
            {
                try
                {
                    return db.FindCharactersByAccountId(accountId);
                }
                catch(Exception ex)
                {

                }
            }

            return null;
        }

        public void CreateCharacter(Character character)
        {
            using (var db = new CharacterContext())
            {
                db.Characters.Add(character);
                db.SaveChanges();
            }
        }

    }
}
