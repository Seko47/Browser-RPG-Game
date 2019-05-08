using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Store
    {
        public int ID { get; set; }
        public string CharacterID { get; set; }
        public DateTime Date { get; set; }

        public Character Character { get; set; }
        public virtual ICollection<ItemStore> ItemStores { get; set; }
    }
}