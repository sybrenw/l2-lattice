using Lattice.L2Common.Enum;
using Lattice.L2Common.Interfaces;
using Lattice.L2Common.Model.Stats;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace Lattice.L2Common.Model
{
    public class Character : L2Object, INotifyObject
    {
        // Base account
        public int AccountId { get; set; }
        // Character index
        public int CharacterIndex { get; set; }
        // Character name
        public string Name { get; set; }

        // Appearance
        public byte Face { get; set; }
        public byte HairColor { get; set; }
        public byte HairStyle { get; set; }
        public Sex Sex { get; set; }
        public Race Race { get; set; }
        public int VisibleClassId { get; set; }

        // Active (sub)class
        public int ActiveClassId { get; set; }

        // Level etc
        public byte Level { get; set; }

        // Stats
        [NotMapped]
        public CharStats Stats { get; set; } = CharStats.CreateCharStats();

        // The one that control this character (could be player, npc, AI, etc)
        [NotMapped]
        public IController Controller { get; set; }

        /* INotifyObject */
        public event EventHandler<ISendableMessage> Broadcasting;

        public void Broadcast(ISendableMessage msg)
        {
            Broadcasting?.Invoke(this, msg);
        }
    }
}
