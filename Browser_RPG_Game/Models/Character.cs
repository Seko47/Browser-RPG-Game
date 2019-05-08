using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Character
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceMax { get; set; }
        public int Health { get; set; }
        public int HealthMax { get; set; }
        protected int StoreID { get; set; }

        protected virtual Store Store { get; set; }
        public virtual ICollection<CharacterAttribute> CharacterAttributes { get; set; }
        public virtual ICollection<CharacterItem> CharacterItems { get; set; }
        public virtual ICollection<CharacterEquipment> CharacterEquipments { get; set; }
    }
}