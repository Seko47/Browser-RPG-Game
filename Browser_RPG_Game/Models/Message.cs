using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Message
    {
        public int ID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Readed { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name ="Send date")]
        public DateTime SendDate { get; set; }

        public virtual Character Sender { get; set; }
        public virtual Character Receiver { get; set; }
    }
}