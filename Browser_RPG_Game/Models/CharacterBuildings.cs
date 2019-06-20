using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class CharacterBuildings
    {
        public int ID { get; set; }
        [Display(Name = "Building")]
        public int BuildingID { get; set; }
        [Range(0, 50)]
        public int Level { get; set; }
        [Range(0, 10000)]
        public int Storage { get; set; }
        [Display(Name = "Storage max")]
        [Range(100, 10000)]
        public int StorageMax { get; set; }
        [Display(Name = "Last update")]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }

        public void UpdateStorage()
        {
            if (Level > 0)
            {
                TimeSpan increaseTime = DateTime.Now - LastUpdate;
                double timeInMinutes = increaseTime.TotalMinutes;

                int quantity = (int)((Building.InitialIncreasePerMinute + (Building.IncreasePerMinuteAfterEachUpgrade * (Level - 1))) * timeInMinutes);

                if (quantity > 0)
                {
                    Storage += quantity;
                    if (Storage > StorageMax)
                    {
                        Storage = StorageMax;
                    }

                    LastUpdate = DateTime.Now;
                }
            }
            else
            {
                LastUpdate = DateTime.Now;
            }
        }

        public int UpdateCost()
        {
            return (Level + 1) * Building.Value;
        }

        public void Update()
        {
            UpdateStorage();
            if (Level < Building.LevelMax)
            {
                ++Level;

                StorageMax *= 2;
            }
        }

        public int Sell()
        {
            UpdateStorage();

            int gold = Storage * Building.Material.Value;

            Storage = 0;

            return gold;
        }

        public virtual ICollection<Character> CharacterSawmill { get; set; }
        public virtual ICollection<Character> CharacterBrickyard { get; set; }
        public virtual ICollection<Character> CharacterIronworks { get; set; }
        public virtual Building Building { get; set; }
    }
}