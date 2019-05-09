using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Character
    {
        private int damage;
        private int defense;

        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceMax { get; set; }
        public int Health { get; set; }
        public int HealthMax { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Luck { get; set; }
        public int? HelmetID { get; set; }
        public int? ArmorID { get; set; }
        public int? GlovesID { get; set; }
        public int? LegsID { get; set; }
        public int? FeetID { get; set; }
        public int? MainHandID { get; set; }
        public int? OffHandID { get; set; }

        public int Damage
        {
            get { return damage; }
            private set { damage = value; }
        }
        public int Defense
        {
            get { return defense; }
            private set { defense = value; }
        }

        public virtual ICollection<Message> SendedMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual Item Helmet { get; set; }
        public virtual Item Armor { get; set; }
        public virtual Item Gloves { get; set; }
        public virtual Item Legs { get; set; }
        public virtual Item Feet { get; set; }
        public virtual Item MainHand { get; set; }
        public virtual Item OffHand { get; set; }
    }
}