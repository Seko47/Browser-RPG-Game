using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Item
    {
        public int ID { get; set; }
        public int ItemTypeID { get; set; }
        public string Name { get; set; }

        public virtual ItemType ItemType { get; set; }
        public virtual ICollection<ItemAttribute> ItemAttributes { get; set; }
    }
}