using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Material
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 10000)]
        public int Value { get; set; }
        [Required]
        [Display(Name="Path to image")]
        public string PathToImage { get; set; }
    }
}