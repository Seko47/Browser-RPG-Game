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
        }

        public Boolean HasItemById(int id)
        {
            return HasItemInInventoryById(id) || HasEquippedItemByID(id);
        }

        public Boolean HasItemInInventoryById(int id)
        {
            return Items.FirstOrDefault(i => i.ID == id) != null;
        }

        public Boolean HasEquippedItemByID(int id)
        {
            return HelmetID == id
                || WeaponID == id
                || ArmorID == id
                || GlovesID == id
                || BootsID == id
                || ShieldID == id;
        }

        public Boolean TakeOffItem(int id)
        {
            if (HelmetID == id)
            {
                Items.Add(Helmet);
                Helmet = null;

                return true;
            }
            else if (WeaponID == id)
            {
                Items.Add(Weapon);
                Weapon = null;

                return true;
            }
            else if (ArmorID == id)
            {
                Items.Add(Armor);
                Armor = null;

                return true;
            }
            else if (GlovesID == id)
            {
                Items.Add(Gloves);
                Gloves = null;

                return true;
            }
            else if (BootsID == id)
            {
                Items.Add(Boots);
                Boots = null;

                return true;
            }
            else if (ShieldID == id)
            {
                Items.Add(Shield);
                Shield = null;

                return true;
            }

            return false;
        }

        public Boolean PutOnItem(int id)
        {
            Item item = Items.FirstOrDefault(i => i.ID == id);
            if(item == null)
            {
                return false;
            }

            if(item.ItemType.Name == "weapon")
            {
                if(Weapon != null)
                {
                    TakeOffItem(Weapon.ID);
                }

                Weapon = item;

                Items.Remove(item);

                return true;
            }
            else if (item.ItemType.Name == "shield")
            {
                if (Shield != null)
                {
                    TakeOffItem(Shield.ID);
                }

                Shield = item;

                Items.Remove(item);

                return true;
            }
            else if (item.ItemType.Name == "helmet")
            {
                if (Helmet != null)
                {
                    TakeOffItem(Helmet.ID);
                }

                Helmet = item;

                Items.Remove(item);

                return true;
            }
            else if (item.ItemType.Name == "armor")
            {
                if (Armor != null)
                {
                    TakeOffItem(Armor.ID);
                }

                Armor = item;

                Items.Remove(item);

                return true;
            }
            else if (item.ItemType.Name == "gloves")
            {
                if (Gloves != null)
                {
                    TakeOffItem(Gloves.ID);
                }

                Gloves = item;

                Items.Remove(item);

                return true;
            }
            else if (item.ItemType.Name == "boots")
            {
                if (Boots != null)
                {
                    TakeOffItem(Boots.ID);
                }

                Boots = item;

                Items.Remove(item);

                return true;
            }

            return false;
        }

        public int StrengthCost
        {
            get
            {
                return (int)((Strength + Level) * 3.5);
            }
        }
        
        public int DexterityCost
        {
            get
            {
                return (int)((Dexterity + Level) * 2.9);
            }
        }
        
        public int IntelligenceCost
        {
            get
            {
                return (int)((Intelligence + Level) * 2.3);
            }
        }
        
        public int LuckCost
        {
            get
            {
                return (int)((Luck + Level) * 1.7);
            }
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
 