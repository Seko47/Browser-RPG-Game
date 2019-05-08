using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class CharacterEquipment
    {
        public int ID { get; set; }
        public int EquipmentSlotID { get; set; }
        public int CharacterID { get; set; }
        public int ItemID { get; set; }

        public virtual EquipmentSlot EquipmentSlot { get; set; }
        public virtual Character Character { get; set; }
        public virtual Item Item { get; set; }
    }
}