using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Character
    {
        private int damage = 1;
        private int defense = 1;

        public int ID { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Login { get; set; }
        public int ProfileTypeID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = "Character name")]
        public string Name { get; set; }
        [Required]
        [Range(1, 1000)]
        public int Level { get; set; }
        [Required]
        [Range(0, 1000000)]
        public int Experience { get; set; }
        [Required]
        [Range(0, 1000000)]
        [Display(Name = "Experience max")]
        public int ExperienceMax { get; set; }
        [Required]
        [Range(0, 1000000)]
        public int Health { get; set; }
        [Required]
        [Range(1, 1000000)]
        [Display(Name = "Health max")]
        public int HealthMax { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Strength { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Dexterity { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Intelligence { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Luck { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Gold { get; set; }
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
            get
            {
                int dmg = damage;
                if (Weapon != null) dmg += Weapon.Damage;
                return dmg;
            }
            private set { damage = value; }
        }
        public int Defense
        {
            get
            {
                int dfn = defense;
                if (Helmet != null) dfn += Helmet.Defense;
                if (Armor != null) dfn += Armor.Defense;
                if (Gloves != null) dfn += Gloves.Defense;
                if (Boots != null) dfn += Boots.Defense;
                if (Shield != null) dfn += Shield.Defense;
                return dfn;
            }
            private set { defense = value; }
        }

        public virtual CharacterBuildings Sawmill { get; set; }
        public virtual CharacterBuildings Brickyard { get; set; }
        public virtual CharacterBuildings Ironworks { get; set; }
        [Display(Name = "Profile type")]
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