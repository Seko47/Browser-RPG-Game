using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Equipment :IComparable<Equipment>
    {
        public int ID { get; set; }
        public int CharacterID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public virtual Item Item { get; set; }
        public virtual Character Character { get; set; }

        public int CompareTo(Equipment other)
        {
            return ItemID.CompareTo(other.ItemID);
        }
    }
}