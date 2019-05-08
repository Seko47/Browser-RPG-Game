using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class BuildingProperty
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public int BuildingID { get; set; }
        public int Level { get; set; }
        public int Storage { get; set; }
        public int StorageMax { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual Property Property { get; set; }
        public virtual Building Building { get; set; }
    }
}