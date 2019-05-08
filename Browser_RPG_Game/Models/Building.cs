using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Building
    {
        public int ID { get; set; }
        public int MaterialID { get; set; }
        public string Name { get; set; }
        public int LevelMax { get; set; }
        public int Value { get; set; }
        public int IncreasePerMinute { get; set; }
        public int IncreasePerMinuteAfterEachUpgrade { get; set; }

        public virtual Material Material { get; set; }
    }
}