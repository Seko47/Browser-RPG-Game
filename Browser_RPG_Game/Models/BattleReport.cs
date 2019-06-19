using System.Collections.Generic;

namespace Browser_RPG_Game.Models
{
    public class BattleReport<T1, T2>
    {
        public T1 Character { get; set; }
        public T2 Enemy { get; set; }
        public bool CharacterWin { get; set; }
        public bool EnemyWin { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }
        public List<Battle> Battles { get; set; }
        public List<Item> Loots { get; set; }

        public void Add(Battle battle)
        {
            Battles.Add(battle);
        }
    }
}