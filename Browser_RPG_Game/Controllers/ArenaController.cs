using Browser_RPG_Game.DAL;
using Browser_RPG_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Browser_RPG_Game.Controllers
{
    public class ArenaController : Controller
    {
        // GET: Arena
        [Authorize]
        public ActionResult Index()
        {
            GameContext db = new GameContext();
            Random rand = new Random();
            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            if (!character.IsArenaAvailable())
            {
                TimeSpan timeToTheNextArenaFight = character.NextArenaFight.Subtract(DateTime.Now);
                string time = (timeToTheNextArenaFight.Hours < 10 ? "0" + timeToTheNextArenaFight.Hours : timeToTheNextArenaFight.Hours.ToString());
                time += ":" + (timeToTheNextArenaFight.Minutes < 10 ? "0" + timeToTheNextArenaFight.Minutes : timeToTheNextArenaFight.Minutes.ToString());
                time += ":" + (timeToTheNextArenaFight.Seconds < 10 ? "0" + timeToTheNextArenaFight.Seconds : timeToTheNextArenaFight.Seconds.ToString());

                ViewBag.TimeToTheNextFight = time.Trim();

                return View();
            }

            List<Character> weakerEnemies = db.Characters.Where(c => c.Level < character.Level - 1 && c.Level >= character.Level - 6 && c.ProfileType.Name != "admin" && c.NextArenaFight <= DateTime.Now).ToList();
            List<Character> normalEnemies = db.Characters.Where(c => c.Level >= character.Level - 1 && c.Level <= character.Level + 1 && c.ID != character.ID && c.ProfileType.Name != "admin" && c.NextArenaFight <= DateTime.Now).ToList();
            List<Character> strongerEnemies = db.Characters.Where(c => c.Level > character.Level + 1 && c.Level <= character.Level + 6 && c.ProfileType.Name != "admin" && c.NextArenaFight <= DateTime.Now).ToList();

            Character weakerEnemy = null;
            if (weakerEnemies.Count > 0)
            {
                weakerEnemy = weakerEnemies[rand.Next(weakerEnemies.Count)];
            }

            Character normalEnemy = null;
            if (normalEnemies.Count > 0)
            {
                normalEnemy = normalEnemies[rand.Next(normalEnemies.Count)];
            }

            Character strongerEnemy = null;
            if (strongerEnemies.Count > 0)
            {
                strongerEnemy = strongerEnemies[rand.Next(strongerEnemies.Count)];
            }

            List<Character> enemies = new List<Character>
            {
                weakerEnemy,
                normalEnemy,
                strongerEnemy
            };

            return View(enemies);
        }

        [Authorize]
        public ActionResult Attack(int id)
        {
            GameContext db = new GameContext();
            Random rand = new Random();

            int timeIntervalBetweenArena = Int32.Parse(db.Configs.Single(c => c.Name == "TIME_INTERVAL_BETWEEN_ARENA").Value);
            int loserExperience = Int32.Parse(db.Configs.Single(c => c.Name == "LOSER_EXPERIENCE").Value);
            int loserMoney = Int32.Parse(db.Configs.Single(c => c.Name == "LOSER_MONEY").Value);

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            Character enemy = db.Characters.Single(e => e.ID == id);

            if (!character.IsArenaAvailable() || !enemy.IsArenaAvailable() || character.ID == enemy.ID || enemy.ProfileType.Name == "admin")
            {
                return RedirectToAction("Index");
            }

            BattleReport<Character, Character> battleReport = new BattleReport<Character, Character>
            {
                Character = character,
                Enemy = enemy,
                CharacterWin = false,
                EnemyWin = false,
                Gold = (int)((double)enemy.Gold * (double)((double)loserMoney / (double)100)),
                Experience = (int)((double)enemy.Experience * (double)((double)loserExperience / (double)100)),
                Battles = new List<Battle>(),
                Loots = null
            };

            if (battleReport.Gold < 0)
            {
                battleReport.Gold = enemy.Gold;
            }

            //Walka i wyświetlenie wyników
            int strengthSum = character.Strength + enemy.Strength;
            int dexteritySum = character.Dexterity + enemy.Dexterity;
            int luckSum = character.Luck + enemy.Luck;

            int characterDoubleAttackChance = character.Luck * 100 / luckSum;
            int enemyDoubleAttackChance = enemy.Luck * 100 / luckSum;

            int characterDefenseChance = character.Dexterity * 100 / dexteritySum;
            int enemyDefenseChance = enemy.Dexterity * 100 / dexteritySum;

            int characterChanceToBreakEnemyDefense = character.Strength * 100 / strengthSum;
            int enemyChanceToBreakCharacterDefense = enemy.Strength * 100 / strengthSum;

            int characterDamage = character.Damage - enemy.Defense;
            if (characterDamage < 0)
            {
                characterDamage = 0;
            }
            int enemyDamage = enemy.Damage - character.Defense;
            if (enemyDamage < 0)
            {
                enemyDamage = 0;
            }

            if (characterDamage != 0 || enemyDamage != 0)
            {
                do
                {
                    Battle battle = new Battle
                    {
                        CharacterIsAttacker = true,
                        CharacterHP = character.Health,
                        EnemyHP = enemy.Health,
                        CriticalAttack = false
                    };

                    //Pierwszy atakuje gracz
                    //pierwszy atak
                    //obliczenie szansy na obronę przeciwnika
                    if (enemyDefenseChance < rand.Next(101))
                    {
                        battle.DefendedAttack = false;
                        battle.BrokenDefense = false;

                        if (characterDamage > 0)
                        {
                            battle.InflictedDamage = characterDamage;
                            battle.EnemyHP = enemy.Hurt(battle.InflictedDamage);

                            if (characterDoubleAttackChance >= rand.Next(101))
                            {
                                battle.DoubleAttack = true;
                                battle.EnemyHP = enemy.Hurt(battle.InflictedDamage);
                            }
                            else
                            {
                                battle.DoubleAttack = false;
                            }
                        }
                        else //Jeśli obrażenia == 0 ale pomoże szczęście to
                        {
                            if (characterDoubleAttackChance >= rand.Next(101))
                            {
                                battle.CriticalAttack = true;
                                battle.InflictedDamage = character.Damage;
                                battle.EnemyHP = enemy.Hurt(battle.InflictedDamage);
                            }
                            else
                            {
                                battle.InflictedDamage = characterDamage;
                                battle.EnemyHP = enemy.Hurt(battle.InflictedDamage);
                            }
                        }
                    }
                    else
                    {
                        battle.DefendedAttack = true;

                        if (characterChanceToBreakEnemyDefense >= rand.Next(101))
                        {
                            battle.BrokenDefense = true;

                            if (characterDamage > 0)
                            {
                                battle.InflictedDamage = (int)((double)characterDamage / (double)2);
                                battle.EnemyHP = enemy.Hurt(battle.InflictedDamage);

                                if (characterDoubleAttackChance >= rand.Next(101))
                                {
                                    battle.DoubleAttack = true;
                                    battle.EnemyHP = enemy.Hurt(battle.InflictedDamage);
                                }
                                else
                                {
                                    battle.DoubleAttack = false;
                                }
                            }
                            else //Jeśli obrażenia == 0 ale pomoże szczęście to
                            {
                                if (characterDoubleAttackChance >= rand.Next(101))
                                {
                                    battle.CriticalAttack = true;
                                    battle.InflictedDamage = (int)((double)character.Damage / (double)2);
                                    battle.EnemyHP = enemy.Hurt(battle.InflictedDamage);
                                }
                                else
                                {
                                    battle.InflictedDamage = (int)((double)characterDamage / (double)2);
                                    battle.EnemyHP = enemy.Hurt(battle.InflictedDamage);
                                }
                            }
                        }
                        else
                        {
                            battle.BrokenDefense = false;
                        }
                    }

                    if (!enemy.IsAlive())
                    {
                        battleReport.CharacterWin = true;

                        character.WinBattle(battleReport.Loots, battleReport.Experience, battleReport.Gold);
                        enemy.LoseBattle(battleReport.Experience, battleReport.Gold);

                        battleReport.Add(battle);

                        break;
                    }
                    else
                    {
                        battleReport.Add(battle);
                    }

                    //Drugi atakuje przeciwnik
                    battle = new Battle
                    {
                        CharacterIsAttacker = false,
                        CharacterHP = character.Health,
                        EnemyHP = enemy.Health,
                        CriticalAttack = false
                    };

                    //Drugi atakuje przeciwnik
                    //pierwszy atak
                    //obliczenie szansy na obronę gracza
                    if (characterDefenseChance < rand.Next(101))
                    {
                        battle.DefendedAttack = false;
                        battle.BrokenDefense = false;

                        if (enemyDamage > 0)
                        {
                            battle.InflictedDamage = enemyDamage;
                            battle.CharacterHP = character.Hurt(enemyDamage);

                            if (enemyDoubleAttackChance >= rand.Next(101))
                            {
                                battle.DoubleAttack = true;
                                battle.CharacterHP = character.Hurt(enemyDamage);
                            }
                            else
                            {
                                battle.DoubleAttack = false;
                            }
                        }
                        else //Jeśli obrażenia == 0 ale pomoże szczęście to
                        {
                            if (enemyDoubleAttackChance >= rand.Next(101))
                            {
                                battle.CriticalAttack = true;
                                battle.InflictedDamage = enemy.Damage;
                                battle.CharacterHP = character.Hurt(enemy.Damage);
                            }
                            else
                            {
                                battle.InflictedDamage = enemyDamage;
                                battle.CharacterHP = character.Hurt(enemyDamage);
                            }
                        }
                    }
                    else
                    {
                        battle.DefendedAttack = true;

                        if (enemyChanceToBreakCharacterDefense >= rand.Next(101))
                        {
                            battle.BrokenDefense = true;

                            if (enemyDamage > 0)
                            {
                                battle.InflictedDamage = enemyDamage / 2;
                                battle.CharacterHP = character.Hurt(enemyDamage / 2);

                                if (enemyDoubleAttackChance >= rand.Next(101))
                                {
                                    battle.DoubleAttack = true;
                                    battle.CharacterHP = character.Hurt(enemyDamage / 2);
                                }
                                else
                                {
                                    battle.DoubleAttack = false;
                                }
                            }
                            else //Jeśli obrażenia == 0 ale pomoże szczęście to
                            {
                                if (enemyDoubleAttackChance >= rand.Next(101))
                                {
                                    battle.CriticalAttack = true;
                                    battle.InflictedDamage = enemy.Damage / 2;
                                    battle.CharacterHP = character.Hurt(enemy.Damage / 2);
                                }
                                else
                                {
                                    battle.InflictedDamage = enemyDamage / 2;
                                    battle.CharacterHP = character.Hurt(enemyDamage / 2);
                                }
                            }
                        }
                        else
                        {
                            battle.BrokenDefense = false;
                        }
                    }

                    if (!character.IsAlive())
                    {
                        battleReport.Experience = (int)((double)character.Experience * (double)((double)loserExperience / (double)100));
                        if (battleReport.Experience < 0)
                        {
                            battleReport.Experience = 0;
                        }
                        battleReport.Gold = (int)((double)character.Gold * (double)((double)loserMoney / (double)100));
                        if (battleReport.Gold < 0)
                        {
                            battleReport.Gold = 0;
                        }

                        battleReport.EnemyWin = true;
                        character.LoseBattle(battleReport.Experience, battleReport.Gold);
                        enemy.WinBattle(null, battleReport.Experience, battleReport.Gold);

                        battleReport.Add(battle);

                        break;
                    }
                    else
                    {
                        battleReport.Add(battle);
                    }

                } while (character.IsAlive() && enemy.IsAlive());
            }

            character.Health = character.HealthMax;
            enemy.Health = enemy.HealthMax;

            character.NextArenaFight = DateTime.Now.AddSeconds(timeIntervalBetweenArena);
            enemy.NextArenaFight = DateTime.Now.AddSeconds(timeIntervalBetweenArena);

            string message = $"Wynik walki: WYGRAŁEŚ. Zyskałeś {battleReport.Gold} złota i {battleReport.Experience} pkt. doś."; ;

            if (!battleReport.CharacterWin && !battleReport.EnemyWin)
            {
                message = "Wynik walki: REMIS";
            }
            else if (battleReport.CharacterWin)
            {
                message = $"Wynik walki: PRZEGRAŁEŚ. Straciłeś {battleReport.Gold} złota i {battleReport.Experience} pkt. doś.";
            }

            Utils.MailUtils.SendMail(enemy.Login, $"Zostałeś zaatakowany przez {character.Name}", message);

            db.SaveChanges();

            return View(battleReport);
        }
    }
}