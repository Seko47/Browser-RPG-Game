using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class CharacterBuildings
    {
        public int ID { get; set; }
        [Display(Name = "Building")]
        public int BuildingID { get; set; }
        [Range(0, 50)]
        public int Level { get; set; }
        [Range(0, 10000)]
        public int Storage { get; set; }
        [Display(Name = "Storage max")]
        [Range(100, 10000)]
        public int StorageMax { get; set; }
        [Display(Name = "Last update")]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<Character> CharacterSawmill { get; set; }
        public virtual ICollection<Character> CharacterBrickyard { get; set; }
        public virtual ICollection<Character> CharacterIronworks { get; set; }
        public virtual Building Building { get; set; }
    }
}