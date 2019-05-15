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
        public string Login { get; set; }
        public int ProfileTypeID { get; set; }
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
        public int? BootsID { get; set; }
        public int? WeaponID { get; set; }
        public int? ShieldID { get; set; }
        public int CharacterImageID { get; set; }
        public int SawmillID { get; set; }
        public int BrickyardID { get; set; }
        public int IronworksID { get; set; }

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

        public virtual CharacterBuildings Sawmill { get; set; }
        public virtual CharacterBuildings Brickyard { get; set; }
        public virtual CharacterBuildings Ironworks { get; set; }
        public virtual ProfileType ProfileType { get; set; }
        public virtual ICollection<Message> SendedMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual Item Helmet { get; set; }
        public virtual Item Armor { get; set; }
        public virtual Item Gloves { get; set; }
        public virtual Item Boots { get; set; }
        public virtual Item Weapon { get; set; }
        public virtual Item Shield { get; set; }
        public virtual CharacterImage CharacterImage { get; set; }
    }
}