using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class EnemyLocation
    {
        public int ID { get; set; }
        public int LocationID { get; set; }
        public int EnemyID { get; set; }

        public virtual Location Location { get; set; }
    }
}