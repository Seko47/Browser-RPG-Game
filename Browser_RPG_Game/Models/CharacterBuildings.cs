using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class CharacterBuildings
    {
        public int ID { get; set; }
        public int BuildingID { get; set; }
        public int Level { get; set; }
        public int Storage { get; set; }
        public int StorageMax { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<Character> CharacterSawmill { get; set; }
        public virtual ICollection<Character> CharacterBrickyard { get; set; }
        public virtual ICollection<Character> CharacterIronworks { get; set; }
        public virtual Building Building { get; set; }
    }
}