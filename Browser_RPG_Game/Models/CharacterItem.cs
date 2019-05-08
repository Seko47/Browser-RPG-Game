using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class CharacterItem
    {
        public int ID { get; set; }
        public int CharacterID { get; set; }
        public int ItemID { get; set; }

        public virtual Character Character { get; set; }
        public virtual Item Item { get; set; }
    }
}