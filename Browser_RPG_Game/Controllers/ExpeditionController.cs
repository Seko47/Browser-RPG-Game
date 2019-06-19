using Browser_RPG_Game.DAL;
using Browser_RPG_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Browser_RPG_Game.Controllers
{
    public class ExpeditionController : Controller
    {
        // GET: Expedition
        [Authorize]
        public ActionResult Index()
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            if (!character.IsExpeditionAvailable())
            {
                TimeSpan timeToTheNextExpedition = character.NextExpedition.Subtract(DateTime.Now);
                string time = (timeToTheNextExpedition.Hours < 10 ? "0" + timeToTheNextExpedition.Hours : timeToTheNextExpedition.Hours.ToString());
                time += ":" + (timeToTheNextExpedition.Minutes < 10 ? "0" + timeToTheNextExpedition.Minutes : timeToTheNextExpedition.Minutes.ToString());
                time += ":" + (timeToTheNextExpedition.Seconds < 10 ? "0" + timeToTheNextExpedition.Seconds : timeToTheNextExpedition.Seconds.ToString());

                ViewBag.TimeToTheNextExpedition = time.Trim();

                return View();
            }

            return View(db.Locations);
        }

        [Authorize]
        public ActionResult Attack(int id)
        {
            GameContext db = new GameContext();
            Random rand = new Random();

            int experienceMultiplier = Int32.Parse(db.Configs.Single(c => c.Name == "EXPERIENCE_MULTIPLIER").Value);
            int lootGoldDifferences = Int32.Parse(db.Configs.Single(c => c.Name == "LOOT_GOLD_DIFFERENCES").Value);
            int timeIntervalBetweenExpeditions = Int32.Parse(db.Configs.Single(c => c.Name == "TIME_INTERVAL_BETWEEN_EXPEDITIONS").Value);
            int loserExperience = Int32.Parse(db.Configs.Single(c => c.Name == "LOSER_EXPERIENCE").Value);
            int loserMoney = Int32.Parse(db.Configs.Single(c => c.Name == "LOSER_MONEY").Value);

            int goldDifferences = rand.Next(lootGoldDifferences * 2) - lootGoldDifferences;

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            if (!character.IsExpeditionAvailable())
            {
                return RedirectToAction("Index");
            }

            Enemy enemy = db.Enemies.Single(e => e.ID == id);

            BattleReport<Character, Enemy> battleReport = new BattleReport<Character, Enemy>
            {
                Character = character,
                Enemy = enemy,
                CharacterWin = false,
                EnemyWin = false,
                Gold = enemy.Money + goldDifferences,
                Experience = enemy.Experience * experienceMultiplier,
                Battles = new List<Battle>(),
                Loots = enemy.ItemLoots.Where(il => il.DropChance >= rand.Next(101)).Select(il => il.Item).Distinct().ToList()
            };

            if (battleReport.Gold < 0)
            {
                battleReport.Gold = enemy.Money;
            }

            //Walka i wyświetlenie wyników
            int strengthSum = character.Strength + enemy.Strength;
            int dexteritySum = character.Dexterity + enemy.Dexterity;
            int luckSum = character.Luck + enemy.Luck;

            int characterDoubleAttackChance = character.Luck * 100 / luckSum;
            int enemyDoubleAttackChance = 100 - characterDoubleAttackChance;

            int characterDefenseChance = character.Dexterity * 100 / dexteritySum;
            int enemyDefenseChance = 100 - characterDefenseChance;

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
                        EnemyHP = enemy.Health
                    };

                    //Pierwszy atakuje gracz
                    //pierwszy atak
                    //obliczenie szansy na obronę przeciwnika
                    if (enemyDefenseChance < rand.Next(101))
                    {
                        battle.DefendedAttack = false;
                        battle.BrokenDefense = false;

                        battle.InflictedDamage = characterDamage;
                        battle.EnemyHP = enemy.Hurt(characterDamage);

                        if (characterDoubleAttackChance >= rand.Next(101))
                        {
                            battle.DoubleAttack = true;
                            battle.EnemyHP = enemy.Hurt(characterDamage);
                        }
                        else
                        {
                            battle.DoubleAttack = false;
                        }
                    }
                    else
                    {
                        battle.DefendedAttack = true;

                        if (characterChanceToBreakEnemyDefense >= rand.Next(101))
                        {
                            battle.BrokenDefense = true;

                            battle.InflictedDamage = characterDamage / 2;
                            battle.EnemyHP = enemy.Hurt(characterDamage / 2);

                            if (characterDoubleAttackChance >= rand.Next(101))
                            {
                                battle.DoubleAttack = true;
                                battle.EnemyHP = enemy.Hurt(characterDamage / 2);
                            }
                            else
                            {
                                battle.DoubleAttack = false;
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
                        EnemyHP = enemy.Health
                    };

                    //Drugi atakuje przeciwnik
                    //pierwszy atak
                    //obliczenie szansy na obronę gracza
                    if (characterDefenseChance < rand.Next(101))
                    {
                        battle.DefendedAttack = false;
                        battle.BrokenDefense = false;

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
                    else
                    {
                        battle.DefendedAttack = true;

                        if (enemyChanceToBreakCharacterDefense >= rand.Next(101))
                        {
                            battle.BrokenDefense = true;

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

            character.NextExpedition = DateTime.Now.AddSeconds(timeIntervalBetweenExpeditions);

            db.SaveChanges();

            return View(battleReport);
        }
    }
}