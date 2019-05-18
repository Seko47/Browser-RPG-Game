using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class ItemLoot
    {
        public int ID { get; set; }
        [Display(Name ="Enemy")]
        public int EnemyID { get; set; }
        [Display(Name ="Item")]
        public int ItemID { get; set; }
        [Display(Name = "Drop chance")]
        [Range(1, 100)]
        public int DropChance { get; set; } //MIN 1% MAX 100%

        public virtual Item Item { get; set; }
        public virtual Enemy Enemy { get; set; }
        //public virtual ICollection<Enemy> Enemies { get; set; }
    }
}