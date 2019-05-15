using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class CharacterImage
    {
        public int ID { get; set; }
        public string PathToImage { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}