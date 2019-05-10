using Browser_RPG_Game.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.DAL
{
    public class GameInitializer : DropCreateDatabaseIfModelChanges<GameContext>
    {
        protected override void Seed(GameContext context)
        {
            base.Seed(context);

            var config = new List<Config>
            {
                new Config{Name="EXPERIENCE_MULTIPLIER", Value="1"},
                new Config{Name="LOOT_GOLD_DIFFERENCES", Value="5"}// lootgold = lootgold +- LOOT_GOLD_DIFFERENCES %
            };

            config.ForEach(c => context.Configs.Add(c));
            context.SaveChanges();

            var roleAdmin = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userAdmin = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleAdmin.Create(new IdentityRole("Admin"));
            var user = new ApplicationUser { UserName = "admin@game.com" };
            string password = "admin";

            userAdmin.Create(user, password);
            userAdmin.AddToRole(user.Id, "Admin");

            var profileTypes = new List<ProfileType>
            {
                new ProfileType{Name="admin"},
                new ProfileType{Name="user"}
            };

            profileTypes.ForEach(p => context.ProfileTypes.Add(p));
            context.SaveChanges();

            Profile profileAdmin = new Profile { Login = user.UserName, ProfileType = profileTypes[0] };

            context.Profiles.Add(profileAdmin);
            context.SaveChanges();

            var materials = new List<Material>
            {
                new Material{Name = "drewno", Value=5},
                new Material{Name = "kamień", Value=10},
                new Material{Name = "złoto", Value=20}
            };

            materials.ForEach(m => context.Materials.Add(m));
            context.SaveChanges();

            var buildings = new List<Building>
            {
                new Building{Name="tartak", Material=materials[0], LevelMax=10, IncreasePerMinute=10, IncreasePerMinuteAfterEachUpgrade=5, Value=500},
                new Building{Name="kamieniołom", Material=materials[1], LevelMax=10, IncreasePerMinute=5, IncreasePerMinuteAfterEachUpgrade=3, Value=1500},
                new Building{Name="kopalnia złota", Material=materials[2], LevelMax=10, IncreasePerMinute=3, IncreasePerMinuteAfterEachUpgrade=2, Value=3000}
            };

            buildings.ForEach(b => context.Buildings.Add(b));
            context.SaveChanges();

            var itemTypes = new List<ItemType>
            {
                new ItemType{Name="Broń"},
                new ItemType{Name="Tarcza"},
                new ItemType{Name="Hełm"},
                new ItemType{Name="Zbroja"},
                new ItemType{Name="Rękawice"},
                new ItemType{Name="Buty"},
                new ItemType{Name="Inne"}
            };

            itemTypes.ForEach(i => context.ItemTypes.Add(i));
            context.SaveChanges();

            var items = new List<Item>
            {
                //WEAPONS (0 - 20)
                new Item{Name="Uszczerbany miecz", ItemType=itemTypes[0], Level=1, Damage=1, Defense=0, Value=0},
                new Item{Name="Sztylet", ItemType=itemTypes[0], Level=2, Damage=3, Defense=0, Value=48},
                new Item{Name="Krótki miecz", ItemType=itemTypes[0], Level=2, Damage=5, Defense=0, Value=62},
                new Item{Name="Wyszczerbiony miedziany miecz", ItemType=itemTypes[0], Level=3, Damage=8, Defense=0, Value=91},
                new Item{Name="Miecz giermka", ItemType=itemTypes[0], Level=4, Damage=10, Defense=0, Value=143},
/*5*/           new Item{Name="Sztylet z brązu", ItemType=itemTypes[0], Level=5, Damage=12, Defense=0, Value=197 },
                new Item{Name="Zakrzywiony miecz rycerza", ItemType=itemTypes[0], Level=5, Damage=14, Defense=0, Value=226 },
                new Item{Name="Miecz opryszka", ItemType=itemTypes[0], Level=6, Damage=17, Defense=0, Value=281 },
                new Item{Name="Miecz wojownika", ItemType=itemTypes[0], Level=7, Damage=19, Defense=0, Value=367 },
                new Item{Name="Długi miecz", ItemType=itemTypes[0], Level=8, Damage=22, Defense=0, Value=438 },
/*10*/          new Item{Name="Szeroki miecz", ItemType=itemTypes[0], Level=9, Damage=24, Defense=0, Value=539 },
                new Item{Name="Szpada", ItemType=itemTypes[0], Level=10, Damage=27, Defense=0, Value=821 },
                new Item{Name="Prosty korbacz", ItemType=itemTypes[0], Level=12, Damage=33, Defense=0, Value=1142 },
                new Item{Name="Łom szabrownika", ItemType=itemTypes[0], Level=15, Damage=37, Defense=0, Value=1400 },
                new Item{Name="Rapier", ItemType=itemTypes[0], Level=18, Damage=42, Defense=0, Value=1538 },
/*15*/          new Item{Name="Sejmitar nieumarłego", ItemType=itemTypes[0], Level=19, Damage=44, Defense=0, Value=1700 },
                new Item{Name="Miecz Sir Galiena", ItemType=itemTypes[0], Level=20, Damage=47, Defense=0, Value=2381 },
                new Item{Name="Mała szabla paladyna", ItemType=itemTypes[0], Level=22, Damage=51, Defense=0, Value=2969 },
                new Item{Name="Morgensztern", ItemType=itemTypes[0], Level=23, Damage=53, Defense=0, Value=3666 },
                new Item{Name="Szabla", ItemType=itemTypes[0], Level=24, Damage=55, Defense=0, Value=4000 },
/*20*/          new Item{Name="Cep bojowy", ItemType=itemTypes[0], Level=25, Damage=57, Defense=0, Value=4409 },

                //SHIELDS (21 - 40)
                new Item{Name="Żelazna tarcza", ItemType=itemTypes[1], Level=1, Damage=0, Defense=1, Value=0},
                new Item{Name="Lekka drewniana tarcza", ItemType=itemTypes[1], Level=2, Damage=0, Defense=3, Value=53},
                new Item{Name="Puklerz", ItemType=itemTypes[1], Level=3, Damage=0, Defense=6, Value=98},
                new Item{Name="Okuta tarcza", ItemType=itemTypes[1], Level=4, Damage=0, Defense=9, Value=153},
/*25*/          new Item{Name="Pancerny puklerz", ItemType=itemTypes[1], Level=5, Damage=0, Defense=12, Value=222},
                new Item{Name="Drewniana tarcza", ItemType=itemTypes[1], Level=6, Damage=0, Defense=14, Value=302},
                new Item{Name="Stalowa tarcza", ItemType=itemTypes[1], Level=7, Damage=0, Defense=17, Value=393},
                new Item{Name="Średnia tarcza", ItemType=itemTypes[1], Level=10, Damage=0, Defense=24, Value=738},
                new Item{Name="Wzmocniona średnia tarcza", ItemType=itemTypes[1], Level=12, Damage=0, Defense=29, Value=1029},
/*30*/          new Item{Name="Magiczny puklerz", ItemType=itemTypes[1], Level=15, Damage=0, Defense=35, Value=1210},
                new Item{Name="Miedziana tarcza", ItemType=itemTypes[1], Level=16, Damage=0, Defense=40, Value=1590},
                new Item{Name="Tarcza dobrej jakości", ItemType=itemTypes[1], Level=17, Damage=0, Defense=43, Value=1778},
                new Item{Name="Mała tarcza paladyna", ItemType=itemTypes[1], Level=18, Damage=0, Defense=46, Value=1979},
                new Item{Name="Tarcza nieumarłego", ItemType=itemTypes[1], Level=19, Damage=0, Defense=49, Value=2300},
/*35*/          new Item{Name="Osłona z niedźwiedzich żeber", ItemType=itemTypes[1], Level=20, Damage=0, Defense=53, Value=2589},
                new Item{Name="Tarcza młodego paladyna", ItemType=itemTypes[1], Level=22, Damage=0, Defense=58, Value=3000},
                new Item{Name="Pawęż", ItemType=itemTypes[1], Level=23, Damage=0, Defense=61, Value=3302},
                new Item{Name="Lekka czerwona tarcza", ItemType=itemTypes[1], Level=24, Damage=0, Defense=64, Value=3759},
                new Item{Name="Mocarna tarcza rycerza", ItemType=itemTypes[1], Level=25, Damage=0, Defense=67, Value=4572},
/*40*/          new Item{Name="Tarcza smoczego jeźdźca", ItemType=itemTypes[1], Level=25, Damage=0, Defense=74, Value=5484},

                //HELMETS (41 - 53)
                new Item{Name="Skórzany hełm", ItemType=itemTypes[2], Level=1, Damage=0, Defense=1, Value=9},
                new Item{Name="Stalowa misiurka", ItemType=itemTypes[2], Level=2, Damage=0, Defense=2, Value=22},
                new Item{Name="Wzmocniona stalowa misiurka", ItemType=itemTypes[2], Level=3, Damage=0, Defense=3, Value=40},
                new Item{Name="Szyszak", ItemType=itemTypes[2], Level=4, Damage=0, Defense=4, Value=63},
/*45*/          new Item{Name="Hełm otwarty", ItemType=itemTypes[2], Level=5, Damage=0, Defense=5, Value=91},
                new Item{Name="Stalowa przyłbica", ItemType=itemTypes[2], Level=6, Damage=0, Defense=6, Value=124},
                new Item{Name="Czapka leśnego elfa", ItemType=itemTypes[2], Level=9, Damage=0, Defense=8, Value=240},
                new Item{Name="Zamknięty hełm", ItemType=itemTypes[2], Level=10, Damage=0, Defense=9, Value=414},
                new Item{Name="Czapka tropiciela", ItemType=itemTypes[2], Level=12, Damage=0, Defense=15, Value=444},
/*50*/          new Item{Name="Nadgryziony kapelusz czarodzieja", ItemType=itemTypes[2], Level=15, Damage=0, Defense=18, Value=608},
                new Item{Name="Hełm z przyłbicą", ItemType=itemTypes[2], Level=20, Damage=0, Defense=20, Value=1149},
                new Item{Name="Krótka szpilka władzy", ItemType=itemTypes[2], Level=23, Damage=0, Defense=23, Value=1500},
                new Item{Name="Zakuty łeb", ItemType=itemTypes[2], Level=25, Damage=0, Defense=26, Value=1890},

                //ARMORS (54 - 68)
                new Item{Name="Uszkodzona zbroja", ItemType=itemTypes[3], Level=1, Damage=0, Defense=3, Value=27},
/*55*/          new Item{Name="Lekka skórzana zbroja", ItemType=itemTypes[3], Level=2, Damage=0, Defense=6, Value=69},
                new Item{Name="Ciężka skórzana zbroja", ItemType=itemTypes[3], Level=3, Damage=0, Defense=9, Value=126},
                new Item{Name="Ciężka ćwiekowana zbroja", ItemType=itemTypes[3], Level=4, Damage=0, Defense=11, Value=171},
                new Item{Name="Lekka zbroja płytowa", ItemType=itemTypes[3], Level=5, Damage=0, Defense=16, Value=285},
                new Item{Name="Zbroja z platyny", ItemType=itemTypes[3], Level=6, Damage=0, Defense=19, Value=387},
/*60*/          new Item{Name="Zbroja z tytanu", ItemType=itemTypes[3], Level=7, Damage=0, Defense=22, Value=506},
                new Item{Name="Kolczuga", ItemType=itemTypes[3], Level=8, Damage=0, Defense=29, Value=595},
                new Item{Name="Zbroja paskowa", ItemType=itemTypes[3], Level=10, Damage=0, Defense=32, Value=861},
                new Item{Name="Zbroja łuskowa", ItemType=itemTypes[3], Level=13, Damage=0, Defense=43, Value=1389},
                new Item{Name="Zbroja segmentowa", ItemType=itemTypes[3], Level=15, Damage=0, Defense=50, Value=1809},
/*65*/          new Item{Name="Stalowy napierśnik", ItemType=itemTypes[3], Level=18, Damage=0, Defense=61, Value=2670},
                new Item{Name="Zbroja płytowa", ItemType=itemTypes[3], Level=20, Damage=0, Defense=69, Value=3102},
                new Item{Name="Płaszcz Mietka", ItemType=itemTypes[3], Level=23, Damage=0, Defense=79, Value=3999},
                new Item{Name="Zbroja smoczego jeźdźca", ItemType=itemTypes[3], Level=25, Damage=0, Defense=89, Value=4607},

                //GLOVES (69 - 80)
                new Item{Name="Rękawice", ItemType=itemTypes[4], Level=1, Damage=0, Defense=1, Value=11},
/*70*/          new Item{Name="Rękawice z jeleniej skóry", ItemType=itemTypes[4], Level=3, Damage=0, Defense=2, Value=48},
                new Item{Name="Wzmocnione rękawice", ItemType=itemTypes[4], Level=4, Damage=0, Defense=3, Value=71},
                new Item{Name="Rękawice jeźdźeckie", ItemType=itemTypes[4], Level=5, Damage=0, Defense=4, Value=149},
                new Item{Name="Rękawice kolcze", ItemType=itemTypes[4], Level=7, Damage=0, Defense=5, Value=194},
                new Item{Name="Skórzane rękawice", ItemType=itemTypes[4], Level=10, Damage=0, Defense=7, Value=304},
/*75*/          new Item{Name="Rękawice rycerskie", ItemType=itemTypes[4], Level=11, Damage=0, Defense=8, Value=491},
                new Item{Name="Złociste rękawice", ItemType=itemTypes[4], Level=15, Damage=0, Defense=10, Value=846},
                new Item{Name="Rękawice z wilczej skóry", ItemType=itemTypes[4], Level=17, Damage=0, Defense=12, Value=1000},
                new Item{Name="Rękawice szarego niedźwiedzia", ItemType=itemTypes[4], Level=20, Damage=0, Defense=15, Value=1572},
                new Item{Name="Rękawice młodego paladyna", ItemType=itemTypes[4], Level=22, Damage=0, Defense=17, Value=1724},
/*80*/          new Item{Name="Rękawice magiczne", ItemType=itemTypes[4], Level=25, Damage=0, Defense=20, Value=2588},

                //BOOTS (81 - 93)
                new Item{Name="Skórzane buty", ItemType=itemTypes[5], Level=1, Damage=0, Defense=1, Value=9},
                new Item{Name="Mocne skórzane buty", ItemType=itemTypes[5], Level=2, Damage=0, Defense=2, Value=22},
                new Item{Name="Buty z lisiej skóry", ItemType=itemTypes[5], Level=4, Damage=0, Defense=3, Value=63},
                new Item{Name="Wzmocnione buty", ItemType=itemTypes[5], Level=5, Damage=0, Defense=4, Value=91},
/*85*/          new Item{Name="Miedziane buty", ItemType=itemTypes[5], Level=6, Damage=0, Defense=5, Value=149},
                new Item{Name="Stalowe buty", ItemType=itemTypes[5], Level=7, Damage=0, Defense=6, Value=194},
                new Item{Name="Buty rycerskie", ItemType=itemTypes[5], Level=10, Damage=0, Defense=8, Value=434},
                new Item{Name="Buty z wilczej skóry", ItemType=itemTypes[5], Level=15, Damage=0, Defense=13, Value=846},
                new Item{Name="Uszkodzone buty Thowara", ItemType=itemTypes[5], Level=17, Damage=0, Defense=15, Value=1094},
/*90*/          new Item{Name="Dobre skórzane buty", ItemType=itemTypes[5], Level=18, Damage=0, Defense=17, Value=1724},
                new Item{Name="Ciężkie rycerskie buty", ItemType=itemTypes[5], Level=20, Damage=0, Defense=19, Value=2200},
                new Item{Name="Lekkie pantofle tyrana", ItemType=itemTypes[5], Level=23, Damage=0, Defense=21, Value=2785},
                new Item{Name="Żołnierskie buty", ItemType=itemTypes[5], Level=25, Damage=0, Defense=24, Value=3100},

                //OTHER (94 - )
                new Item{Name="Sierść pająka", Value=110, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
/*95*/          new Item{Name="Przędza pająka", Value=70, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Jad pająka", Value=30, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Oczy żaby", Value=100, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Żabie udka", Value=12, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Głowa szerszenia", Value=110, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
/*100*/         new Item{Name="Skrzydła szerszenia", Value=27, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Żądło", Value=50, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Lisi ogon", Value=90, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Ogon szczura", Value=200, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Mięso", Value=11, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
/*105*/         new Item{Name="Skóra Czarnej Wilczycy", Value=500, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Kły Czarnej Wilczycy", Value=200, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
                new Item{Name="Wilcze jagody", Value=56, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0},
            };

            items.ForEach(i => context.Items.Add(i));
            context.SaveChanges();

            var locations = new List<Location>
            {
                new Location{Name="Leśny Gąszcz" },
                new Location{Name="Zaginiony Matecznik" },
                new Location{Name="Rozstaje Dróg" }
                //new Location{Name="Dziewicza Knieja" },
                //new Location{Name="Głęboki Rozłóg" }
            };

            locations.ForEach(l => context.Locations.Add(l));
            context.SaveChanges();

            var enemies = new List<Enemy>
            {
                new Enemy
                {
                    Name ="Mysz", Level=1, Damage=1, Defense=0, HealthMax=3, Health=3,
                    Luck =0, Strength=1, Dexterity=1,
                    Location =locations[0],
                    Loot =new Loot
                    {
                        Money =0, Experience=5
                    }
                },
                new Enemy
                {
                    Name ="Pająk", Level=2, Damage=1, Defense=1, HealthMax=5, Health=5,
                    Luck =1, Strength=1, Dexterity=2,
                    Location =locations[0],
                    Loot =new Loot
                    {
                        Money =0, Experience=7,
                        ItemLoots =new List<ItemLoot>
                        {
                            new ItemLoot {Item=items[94], DropChance=10},
                            new ItemLoot {Item=items[95], DropChance=20},
                            new ItemLoot {Item=items[96], DropChance=30}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Ropucha", Level=3, Damage=3, Defense=2, HealthMax=8, Health=8,
                    Luck =1, Strength=2, Dexterity=2,
                    Location =locations[0],
                    Loot =new Loot
                    {
                        Money =0, Experience=10,
                        ItemLoots =new List<ItemLoot>
                        {
                            new ItemLoot {Item=items[97], DropChance=15},
                            new ItemLoot {Item=items[98], DropChance=50}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Nocny pająk", Level=4, Damage=4, Defense=3, HealthMax=12, Health=12,
                    Luck =2, Strength=2, Dexterity=2,
                    Location =locations[0],
                    Loot =new Loot
                    {
                        Money =0, Experience=14
                    }
                },
                new Enemy
                {
                    Name ="Szerszeń", Level=5, Damage=6, Defense=6, HealthMax=17, Health=17,
                    Luck =3, Strength=3, Dexterity=3,
                    Location =locations[0],
                    Loot =new Loot
                    {
                        Money =0, Experience=17,
                        ItemLoots = new List<ItemLoot>
                        {
                            new ItemLoot{Item=items[99], DropChance=30},
                            new ItemLoot{Item=items[100], DropChance=70},
                            new ItemLoot{Item=items[101], DropChance=50}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Lis", Level=6, Damage=8, Defense=7, HealthMax=20, Health=20,
                    Luck =5, Strength=5, Dexterity=6,
                    Location =locations[0],
                    Loot =new Loot
                    {
                        Money =0, Experience=20,
                        ItemLoots = new List<ItemLoot>
                        {
                            new ItemLoot{Item=items[102], DropChance=45}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Król szczurów", Level=7, Damage=9, Defense=9, HealthMax=25, Health=25,
                    Luck =6, Strength=7, Dexterity=6,
                    Location =locations[1],
                    Loot =new Loot
                    {
                        Money =0, Experience=25,
                        ItemLoots = new List<ItemLoot>
                        {
                            new ItemLoot{Item=items[103], DropChance=50}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Gacek", Level=8, Damage=10, Defense=10, HealthMax=20, Health=20,
                    Luck =9, Strength=6, Dexterity=8,
                    Location =locations[1],
                    Loot =new Loot
                    {
                        Money =0, Experience=30,
                        ItemLoots = new List<ItemLoot>
                        {
                            new ItemLoot{Item=items[104], DropChance=80}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Dziki pies", Level=9, Damage=12, Defense=10, HealthMax=35, Health=35,
                    Luck =5, Strength=8, Dexterity=5,
                    Location =locations[1],
                    Loot =new Loot
                    {
                        Money =0, Experience=35
                    }
                },
                new Enemy
                {
                    Name ="Czarna Wilczyca", Level=10, Damage=15, Defense=14, HealthMax=45, Health=45,
                    Luck =7, Strength=11, Dexterity=10,
                    Location =locations[1],
                    Loot =new Loot
                    {
                        Money =0, Experience=45,
                        ItemLoots = new List<ItemLoot>
                        {
                            new ItemLoot{Item=items[105], DropChance=10},
                            new ItemLoot{Item=items[106], DropChance=25},
                            new ItemLoot{Item=items[107], DropChance=80}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Ork Łaknikąpiel", Level=11, Damage=17, Defense=17, HealthMax=50, Health=50,
                    Luck =4, Strength=15, Dexterity=10,
                    Location =locations[2],
                    Loot =new Loot
                    {
                        Money =15, Experience=50,
                        ItemLoots = new List<ItemLoot>
                        {
                            new ItemLoot{Item=items[0], DropChance=80},
                            new ItemLoot{Item=items[4], DropChance=60},
                            new ItemLoot{Item=items[8], DropChance=40},
                            new ItemLoot{Item=items[11], DropChance=20}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Karciany rycerz", Level=12, Damage=17, Defense=19, HealthMax=55, Health=55,
                    Luck =10, Strength=14, Dexterity=17,
                    Location =locations[2],
                    Loot =new Loot
                    {
                        Money =30, Experience=55,
                        ItemLoots = new List<ItemLoot>
                        {
                            new ItemLoot{Item=items[12], DropChance=30},
                            new ItemLoot{Item=items[29], DropChance=30},
                            new ItemLoot{Item=items[49], DropChance=50},
                            new ItemLoot{Item=items[62], DropChance=25},
                            new ItemLoot{Item=items[75], DropChance=30},
                            new ItemLoot{Item=items[87], DropChance=20}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Bezgłowy jeździec", Level=14, Damage=19, Defense=21, HealthMax=65, Health=65,
                    Luck =3, Strength=18, Dexterity=10,
                    Location =locations[2],
                    Loot =new Loot
                    {
                        Money =50, Experience=65,
                        ItemLoots = new List<ItemLoot>
                        {
                            new ItemLoot{Item=items[13], DropChance=30},
                            new ItemLoot{Item=items[11], DropChance=40},
                            new ItemLoot{Item=items[30], DropChance=30},
                            new ItemLoot{Item=items[63], DropChance=30},
                            new ItemLoot{Item=items[76], DropChance=30},
                            new ItemLoot{Item=items[88], DropChance=20}
                        }
                    }
                },
                new Enemy
                {
                    Name ="Paladyński Apostata", Level=15, Damage=21, Defense=20, HealthMax=75, Health=75,
                    Luck =13, Strength=20, Dexterity=20,
                    Location =locations[2],
                    Loot =new Loot
                    {
                        Money =80, Experience=75,
                        ItemLoots = new List<ItemLoot>
                        {
                            new ItemLoot{Item=items[17], DropChance=20},
                            new ItemLoot{Item=items[33], DropChance=40},
                            new ItemLoot{Item=items[36], DropChance=15},
                            new ItemLoot{Item=items[79], DropChance=10}
                        }
                    }
                },
            };

            enemies.ForEach(e => context.Enemies.Add(e));
            context.SaveChanges();


        }
    }
}