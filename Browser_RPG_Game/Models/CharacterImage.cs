using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class CharacterImage
    {
        public int ID { get; set; }
        [Required]
        [Display(Name ="Path to image")]
        public string PathToImage { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}