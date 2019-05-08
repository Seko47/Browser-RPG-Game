using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Enemy : Character
    {
        public int LootID { get; set; }

        public virtual Loot Loot { get; set; }
    }
}