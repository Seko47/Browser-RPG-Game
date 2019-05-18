using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Item
    {
        public int ID { get; set; }
        [Display(Name="Item type")]
        public int ItemTypeID { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, 1000)]
        public int Level { get; set; }
        [Range(0, 1000)]
        public int Damage { get; set; }
        [Range(0, 1000)]
        public int Defense { get; set; }
        [Range(0, 1000000)]
        public int Value { get; set; }
        [Required]
        [Display(Name="Path to image")]
        public string PathToImage { get; set; }

        [Display(Name="Item type")]
        public virtual ItemType ItemType { get; set; }
        public virtual ICollection<Character> CharactersArmors { get; set; }
        public virtual ICollection<Character> CharactersHelmets { get; set; }
        public virtual ICollection<Character> CharactersGloves { get; set; }
        public virtual ICollection<Character> CharactersBoots { get; set; }
        public virtual ICollection<Character> CharactersWeapons { get; set; }
        public virtual ICollection<Character> CharactersShields { get; set; }
    }
}