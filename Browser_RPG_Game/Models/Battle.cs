using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Battle
    {
        public int CharacterHP { get; set; }
        public int EnemyHP { get; set; }
        public bool CharacterIsAttacker { get; set; }
        public int InflictedDamage { get; set; } //Attacker.Damage - Victim.Defense
        public bool DoubleAttack { get; set; }
        public bool DefendedAttack { get; set; }
        public bool BrokenDefense { get; set; }
    }
}