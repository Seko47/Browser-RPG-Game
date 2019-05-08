using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.Models
{
    public class Attribute
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
/*
    name:'strength', desc:'A measure of how physically strong a character is.',
    name:'dexterity', desc:'A measure of how agile a character is.',
    name:'intelligence', desc:'A measure of a character''s problem-solving ability.',
    name:'luck', desc:'A measure of a character having chance to favor him or her.',
    name:'damage', desc: 'A measure to inflict damage.'
    name:'defense', desc: 'A measure to resistance.'
*/
