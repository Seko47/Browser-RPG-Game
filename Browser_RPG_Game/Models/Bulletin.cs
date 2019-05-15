using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Bulletin
    {
        public int ID { get; set; }
        public int CharacterID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public DateTime Date { get; set; }

        public virtual Character Character { get; set; }
    }
}