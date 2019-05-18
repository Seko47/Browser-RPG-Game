using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class ProfileType
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}