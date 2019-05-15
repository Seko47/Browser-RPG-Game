using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class ItemLoot
    {
        public int ID { get; set; }
        public int LootID { get; set; }
        public int ItemID { get; set; }
        public int DropChance { get; set; } //MIN 1% MAX 100%

        public virtual Loot Loot { get; set; }
        public virtual Item Item { get; set; }
    }
}