using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Item
    {
        public int ID { get; set; }
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public int Value { get; set; }

        public virtual ItemType ItemType { get; set; }
        public virtual ICollection<Character> CharactersArmors { get; set; }
        public virtual ICollection<Character> CharactersHelmets { get; set; }
        public virtual ICollection<Character> CharactersGloves { get; set; }
        public virtual ICollection<Character> CharactersBoots { get; set; }
        public virtual ICollection<Character> CharactersWeapons { get; set; }
        public virtual ICollection<Character> CharactersShields { get; set; }
    }
}