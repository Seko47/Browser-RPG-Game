using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Enemy
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [Range(0, 100)]
        public int Level { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Health { get; set; }
        [Required]
        [Range(1, 1000)]
        [Display(Name = "Health max")]
        public int HealthMax { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Strength { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Dexterity { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Luck { get; set; }
        [Required]
        [Range(0, 100)]
        public int Damage { get; set; }
        [Required]
        [Range(0, 100)]
        public int Defense { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Experience { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Money { get; set; }
        [Display(Name = "Location")]
        public int LocationID { get; set; }
        [Required]
        [Display(Name = "Path to image")]
        public string PathToImage { get; set; }

        public bool IsAlive()
        {
            return Health > 0;
        }

        [Display(Name = "Loot")]
        public virtual List<ItemLoot> ItemLoots { get; set; }
        public virtual Location Location { get; set; }

        public int Hurt(int characterDamage)
        {
            int hp = Health - characterDamage;
            if (hp < 0)
            {
                hp = 0;
            }
            Health = hp;

            return Health;
        }
    }
}