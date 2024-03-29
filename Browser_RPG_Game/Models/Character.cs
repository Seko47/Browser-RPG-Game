﻿using Browser_RPG_Game.DAL;
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
        [Range(0, 100)]
        public int Intelligence { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Luck { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Gold { get; set; }
        public DateTime NextExpedition { get; set; }
        public DateTime NextArenaFight { get; set; }
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
                dmg += Strength / 5;
                return dmg;
            }
        }
        public int Defense
        {
            get
            {
                int dfn = defense;
                dfn += Dexterity / 5;
                if (Helmet != null) dfn += Helmet.Defense;
                if (Armor != null) dfn += Armor.Defense;
                if (Gloves != null) dfn += Gloves.Defense;
                if (Boots != null) dfn += Boots.Defense;
                if (Shield != null) dfn += Shield.Defense;
                return dfn;
            }
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public bool IsArenaAvailable()
        {
            return NextArenaFight <= DateTime.Now;
        }

        public bool IsExpeditionAvailable()
        {
            return NextExpedition <= DateTime.Now;
        }

        public Boolean HasItemById(int id)
        {
            return HasItemInInventoryById(id) || HasEquippedItemByID(id);
        }

        public Boolean HasItemInInventoryById(int id)
        {
            return Equipments.FirstOrDefault(i => i.Item.ID == id) != null;
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

        public void AddItemToEquipment(Item item)
        {
            if (HasItemInInventoryById(item.ID))
            {
                ++Equipments.Single(e => e.Item.ID == item.ID).Quantity;
            }
            else
            {
                Equipments.Add(new Equipment
                {
                    CharacterID = ID,
                    Item = item,
                    Quantity = 1
                });
            }
        }

        public Boolean RemoveItemFromEquipment(Item item)
        {
            if (!HasItemInInventoryById(item.ID))
            {
                return false;
            }

            if (Equipments.Single(e => e.Item.ID == item.ID).Quantity > 1)
            {
                --Equipments.Single(e => e.Item.ID == item.ID).Quantity;
            }
            else
            {
                GameContext db = new GameContext();

                if (db.Equipments.Any(e => e.CharacterID == ID && e.ItemID == item.ID))
                {
                    db.Equipments.Remove(db.Equipments.Single(e => e.CharacterID == ID && e.ItemID == item.ID));
                }

                db.SaveChanges();
            }

            return true;
        }

        private void LevelUp(int experience)
        {
            Experience += experience;

            while (Experience > ExperienceMax)
            {
                ++Level;
                Experience -= ExperienceMax;
                ExperienceMax = Level * 40;
                HealthMax += 5;
                Health = HealthMax;
            }
        }

        public void WinBattle(List<Item> items, int experience, int gold)
        {
            LevelUp(experience);
            Gold += gold;

            if (items != null)
            {
                items.ForEach(i => AddItemToEquipment(i));
            }
        }

        public Boolean TakeOffItem(int id)
        {
            if (HelmetID == id)
            {
                AddItemToEquipment(Helmet);
                Helmet = null;

                return true;
            }
            else if (WeaponID == id)
            {
                AddItemToEquipment(Weapon);
                Weapon = null;

                return true;
            }
            else if (ArmorID == id)
            {
                AddItemToEquipment(Armor);
                Armor = null;

                return true;
            }
            else if (GlovesID == id)
            {
                AddItemToEquipment(Gloves);
                Gloves = null;

                return true;
            }
            else if (BootsID == id)
            {
                AddItemToEquipment(Boots);
                Boots = null;

                return true;
            }
            else if (ShieldID == id)
            {
                AddItemToEquipment(Shield);
                Shield = null;

                return true;
            }

            return false;
        }

        public int Hurt(int enemyDamage)
        {
            int hp = Health - enemyDamage;
            if (hp < 0)
            {
                hp = 0;
            }
            Health = hp;

            return Health;
        }

        public Boolean PutOnItem(int id)
        {
            Item item = Equipments.Where(i => i.Item.ID == id).Select(i => i.Item).First();

            if (item == null)
            {
                return false;
            }

            if (item.ItemType.Name == "weapon")
            {
                if (Weapon != null)
                {
                    TakeOffItem(Weapon.ID);
                }

                Weapon = item;

                RemoveItemFromEquipment(item);

                return true;
            }
            else if (item.ItemType.Name == "shield")
            {
                if (Shield != null)
                {
                    TakeOffItem(Shield.ID);
                }

                Shield = item;

                RemoveItemFromEquipment(item);

                return true;
            }
            else if (item.ItemType.Name == "helmet")
            {
                if (Helmet != null)
                {
                    TakeOffItem(Helmet.ID);
                }

                Helmet = item;

                RemoveItemFromEquipment(item);

                return true;
            }
            else if (item.ItemType.Name == "armor")
            {
                if (Armor != null)
                {
                    TakeOffItem(Armor.ID);
                }

                Armor = item;

                RemoveItemFromEquipment(item);

                return true;
            }
            else if (item.ItemType.Name == "gloves")
            {
                if (Gloves != null)
                {
                    TakeOffItem(Gloves.ID);
                }

                Gloves = item;

                RemoveItemFromEquipment(item);

                return true;
            }
            else if (item.ItemType.Name == "boots")
            {
                if (Boots != null)
                {
                    TakeOffItem(Boots.ID);
                }

                Boots = item;

                RemoveItemFromEquipment(item);

                return true;
            }

            return false;
        }

        public void LoseBattle(int experience, int gold)
        {
            Experience -= experience;

            Gold -= gold;
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
        public virtual List<Equipment> Equipments { get; set; }
        public virtual Item Helmet { get; set; }
        public virtual Item Armor { get; set; }
        public virtual Item Gloves { get; set; }
        public virtual Item Boots { get; set; }
        public virtual Item Weapon { get; set; }
        public virtual Item Shield { get; set; }
        public virtual CharacterImage CharacterImage { get; set; }
    }
}
