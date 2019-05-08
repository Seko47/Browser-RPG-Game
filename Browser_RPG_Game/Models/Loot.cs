using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Loot
    {
        public int ID { get; set; }
        public int Experience { get; set; } //MIN 0
        public int Money { get; set; } //MIN 0

        public virtual ICollection<ItemLoot> ItemLoots { get; set; }
    }
}