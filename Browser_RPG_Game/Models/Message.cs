using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Message
    {
        public int ID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Readed { get; set; }
        public DateTime SendDate { get; set; }

        public virtual Character Sender { get; set; }
        public virtual Character Receiver { get; set; }
    }
}