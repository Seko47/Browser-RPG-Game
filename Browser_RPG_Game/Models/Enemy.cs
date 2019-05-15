using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Enemy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int HealthMax { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Luck { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public int LootID { get; set; }
        public int LocationID { get; set; }
        public string PathToImage { get; set; }

        public virtual Loot Loot { get; set; }
        public virtual Location Location { get; set; }
    }
}