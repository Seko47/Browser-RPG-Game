using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Building
    {
        public int ID { get; set; }
        [Display(Name = "Material")]
        public int MaterialID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [Display(Name="Path to image")]
        public string PathToImage { get; set; }
        [Range(1, 50)]
        [Display(Name="Level max")]
        public int LevelMax { get; set; }
        [Range(100, 100000)]
        public int Value { get; set; }
        [Range(1, 100)]
        [Display(Name="Initial increase per minute")]
        public int InitialIncreasePerMinute { get; set; }
        [Range(1, 100)]
        [Display(Name="Increase per minute after each upgrade")]
        public int IncreasePerMinuteAfterEachUpgrade { get; set; }

        public virtual Material Material { get; set; }
    }
}