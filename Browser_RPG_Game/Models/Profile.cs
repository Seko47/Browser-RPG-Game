using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public int ProfileTypeID { get; set; }
        public int? CharacterID { get; set; }

        public virtual ProfileType ProfileType { get; set; }
        public virtual Character Character { get; set; }
    }
}