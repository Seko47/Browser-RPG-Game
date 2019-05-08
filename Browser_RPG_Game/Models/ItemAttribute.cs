using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class ItemAttribute
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public int AttributeID { get; set; }
        public int Value { get; set; }

        public virtual Item Item { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}