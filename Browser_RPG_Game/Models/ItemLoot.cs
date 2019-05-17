using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class ItemLoot
    {
        public int ID { get; set; }
        public int EnemyID { get; set; }
        public int ItemID { get; set; }
        public int DropChance { get; set; } //MIN 1% MAX 100%

        public virtual Item Item { get; set; }
        public virtual Enemy Enemy { get; set; }
        //public virtual ICollection<Enemy> Enemies { get; set; }
    }
}