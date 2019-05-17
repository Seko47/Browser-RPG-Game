using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Bulletin
    {
        public int ID { get; set; }
        public int CharacterID { get; set; }
        [Required]
        [StringLength(10000, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(100000, MinimumLength = 2)]
        public string Contents { get; set; }
        public DateTime Date { get; set; }

        public virtual Character Character { get; set; }
    }
}