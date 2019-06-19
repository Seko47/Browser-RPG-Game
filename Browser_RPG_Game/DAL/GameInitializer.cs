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

            var roleAdmin = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userAdmin = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleAdmin.Create(new IdentityRole("ADMIN"));
            var user = new ApplicationUser { UserName = "admin@game.com" };
            string password = "zaq1@WSX";

            userAdmin.Create(user, password);
            userAdmin.AddToRole(user.Id, "ADMIN");

            var config = new List<Config>
            {
                new Config{Name="EXPERIENCE_MULTIPLIER", Value="1"},
                new Config{Name="LOOT_GOLD_DIFFERENCES", Value="5"},// lootgold = lootgold +- LOOT_GOLD_DIFFERENCES %
                new Config{Name="TIME_INTERVAL_BETWEEN_EXPEDITIONS", Value="10"}, //in seconds
                new Config{Name="TIME_INTERVAL_BETWEEN_ARENA", Value="20"}, //in seconds
                new Config{Name="LOSER_EXPERIENCE", Value="20"}, // experience -= LOSER_EXPERIENCE %
                new Config{Name="LOSER_MONEY", Value="20"} // gold -= LOSER_MONEY %
            };

            config.ForEach(c => context.Configs.Add(c));
            context.SaveChanges();

            var profileTypes = new List<ProfileType>
            {
                new ProfileType{Name="admin"},
                new ProfileType{Name="user"}
            };

            profileTypes.ForEach(p => context.ProfileTypes.Add(p));
            context.SaveChanges();

            var characterImages = new List<CharacterImage>
            {
                new CharacterImage { PathToImage = "http://emargo.pl/margonem/obrazki/npc/mez/tuz-zlodziej.gif" },
                new CharacterImage { PathToImage = "https://www.margonem.pl/obrazki/other/png/outfit-wojownik.png" },
                new CharacterImage { PathToImage = "https://www.margonem.pl/obrazki/other/png/outfit-tancerz.png" },
                new CharacterImage { PathToImage = "https://www.margonem.pl/obrazki/other/png/outfit-lowca.png" },
                new CharacterImage { PathToImage = "https://www.margonem.pl/obrazki/other/png/outfit-paladyn.png" },
                new CharacterImage { PathToImage = "https://www.margonem.pl/obrazki/other/png/outfit-tropiciel.png" },
            };

            characterImages.ForEach(c => context.CharacterImages.Add(c));
            context.SaveChanges();

            var materials = new List<Material>
            {
                new Material{Name = "drewno", Value=5, PathToImage="https://help.plemiona.pl/images/8/88/Drewno.png"},
                new Material{Name = "glina", Value=10, PathToImage="https://help.plemiona.pl/images/8/89/Glina.png"},
                new Material{Name = "żelazo", Value=20, PathToImage="https://help.plemiona.pl/images/8/8b/Zelazo.png"}
            };

            materials.ForEach(m => context.Materials.Add(m));
            context.SaveChanges();


            var buildings = new List<Building>
            {
                new Building{Name="tartak", Material=materials[0], LevelMax=10, InitialIncreasePerMinute=10, IncreasePerMinuteAfterEachUpgrade=5, Value=500, PathToImage="https://help.plemiona.pl/images/6/6d/Tartak10.png"},
                new Building{Name="cegielnia", Material=materials[1], LevelMax=10, InitialIncreasePerMinute=5, IncreasePerMinuteAfterEachUpgrade=3, Value=1500, PathToImage="https://help.plemiona.pl/images/4/4e/Glina20.png"},
                new Building{Name="huta żelaza", Material=materials[2], LevelMax=10, InitialIncreasePerMinute=3, IncreasePerMinuteAfterEachUpgrade=2, Value=3000, PathToImage="https://help.plemiona.pl/images/3/38/Huta10.png"}
            };

            buildings.ForEach(b => context.Buildings.Add(b));
            context.SaveChanges();

            var itemTypes = new List<ItemType>
            {
                new ItemType{Name="weapon"},
                new ItemType{Name="shield"},
                new ItemType{Name="helmet"},
                new ItemType{Name="armor"},
                new ItemType{Name="gloves"},
                new ItemType{Name="boots"},
                new ItemType{Name="other"}
            };

            itemTypes.ForEach(i => context.ItemTypes.Add(i));
            context.SaveChanges();


            var items = new List<Item>
            {
                //WEAPONS (0 - 20)
                new Item{Name="Uszczerbany miecz", ItemType=itemTypes[0], Level=1, Damage=1, Defense=0, Value=0, PathToImage="http://www.naszemargo.pl/g/i3975.gif"},
                new Item{Name="Sztylet", ItemType=itemTypes[0], Level=2, Damage=3, Defense=0, Value=48, PathToImage="http://www.naszemargo.pl/g/i137.gif"},
                new Item{Name="Krótki miecz", ItemType=itemTypes[0], Level=2, Damage=5, Defense=0, Value=62, PathToImage="http://www.naszemargo.pl/g/i138.gif"},
                new Item{Name="Wyszczerbiony miedziany miecz", ItemType=itemTypes[0], Level=3, Damage=8, Defense=0, Value=91, PathToImage="http://www.naszemargo.pl/g/i3976.gif"},
                new Item{Name="Miecz giermka", ItemType=itemTypes[0], Level=4, Damage=10, Defense=0, Value=143, PathToImage="http://www.naszemargo.pl/g/i3977.gif"},
/*5*/           new Item{Name="Sztylet z brązu", ItemType=itemTypes[0], Level=5, Damage=12, Defense=0, Value=197, PathToImage="http://www.naszemargo.pl/g/i5658.gif" },
                new Item{Name="Zakrzywiony miecz rycerza", ItemType=itemTypes[0], Level=5, Damage=14, Defense=0, Value=226, PathToImage="http://www.naszemargo.pl/g/i3978.gif" },
                new Item{Name="Miecz opryszka", ItemType=itemTypes[0], Level=6, Damage=17, Defense=0, Value=281, PathToImage="http://www.naszemargo.pl/g/i3979.gif" },
                new Item{Name="Miecz wojownika", ItemType=itemTypes[0], Level=7, Damage=19, Defense=0, Value=367, PathToImage="http://www.naszemargo.pl/g/i3980.gif" },
                new Item{Name="Długi miecz", ItemType=itemTypes[0], Level=8, Damage=22, Defense=0, Value=438, PathToImage="http://www.naszemargo.pl/g/i141.gif" },
/*10*/          new Item{Name="Szeroki miecz", ItemType=itemTypes[0], Level=9, Damage=24, Defense=0, Value=539, PathToImage="http://www.naszemargo.pl/g/i142.gif"},
                new Item{Name="Szpada", ItemType=itemTypes[0], Level=10, Damage=27, Defense=0, Value=821, PathToImage="http://www.naszemargo.pl/g/i1962.gif" },
                new Item{Name="Prosty korbacz", ItemType=itemTypes[0], Level=12, Damage=33, Defense=0, Value=1142, PathToImage="http://www.naszemargo.pl/g/i1891.gif" },
                new Item{Name="Łom szabrownika", ItemType=itemTypes[0], Level=15, Damage=37, Defense=0, Value=1400, PathToImage="http://www.naszemargo.pl/g/i7044.gif" },
                new Item{Name="Rapier", ItemType=itemTypes[0], Level=18, Damage=42, Defense=0, Value=1538, PathToImage="http://www.naszemargo.pl/g/i3141.gif" },
/*15*/          new Item{Name="Sejmitar nieumarłego", ItemType=itemTypes[0], Level=19, Damage=44, Defense=0, Value=1700, PathToImage="http://www.naszemargo.pl/g/i4453.gif" },
                new Item{Name="Miecz Sir Galiena", ItemType=itemTypes[0], Level=20, Damage=47, Defense=0, Value=2381, PathToImage="http://www.naszemargo.pl/g/i3745.gif" },
                new Item{Name="Mała szabla paladyna", ItemType=itemTypes[0], Level=22, Damage=51, Defense=0, Value=2969, PathToImage="http://www.naszemargo.pl/g/i2594.gif" },
                new Item{Name="Morgensztern", ItemType=itemTypes[0], Level=23, Damage=53, Defense=0, Value=3666, PathToImage="http://www.naszemargo.pl/g/i1960.gif" },
                new Item{Name="Szabla", ItemType=itemTypes[0], Level=24, Damage=55, Defense=0, Value=4000, PathToImage="http://www.naszemargo.pl/g/i5659.gif" },
/*20*/          new Item{Name="Cep bojowy", ItemType=itemTypes[0], Level=25, Damage=57, Defense=0, Value=4409, PathToImage="http://www.naszemargo.pl/g/i1961.gif" },

                //SHIELDS (21 - 40)
                new Item{Name="Żelazna tarcza", ItemType=itemTypes[1], Level=1, Damage=0, Defense=1, Value=0, PathToImage="http://www.naszemargo.pl/g/i3989.gif" },
                new Item{Name="Lekka drewniana tarcza", ItemType=itemTypes[1], Level=2, Damage=0, Defense=3, Value=53, PathToImage="http://www.naszemargo.pl/g/i3990.gif" },
                new Item{Name="Puklerz", ItemType=itemTypes[1], Level=3, Damage=0, Defense=6, Value=98, PathToImage="http://www.naszemargo.pl/g/i143.gif" },
                new Item{Name="Okuta tarcza", ItemType=itemTypes[1], Level=4, Damage=0, Defense=9, Value=153, PathToImage="http://www.naszemargo.pl/g/i3991.gif" },
/*25*/          new Item{Name="Pancerny puklerz", ItemType=itemTypes[1], Level=5, Damage=0, Defense=12, Value=222, PathToImage="http://www.naszemargo.pl/g/i144.gif" },
                new Item{Name="Drewniana tarcza", ItemType=itemTypes[1], Level=6, Damage=0, Defense=14, Value=302, PathToImage="http://www.naszemargo.pl/g/i145.gif" },
                new Item{Name="Stalowa tarcza", ItemType=itemTypes[1], Level=7, Damage=0, Defense=17, Value=393, PathToImage="http://www.naszemargo.pl/g/i146.gif" },
                new Item{Name="Średnia tarcza", ItemType=itemTypes[1], Level=10, Damage=0, Defense=24, Value=738, PathToImage="http://www.naszemargo.pl/g/i2273.gif" },
                new Item{Name="Wzmocniona średnia tarcza", ItemType=itemTypes[1], Level=12, Damage=0, Defense=29, Value=1029, PathToImage="http://www.naszemargo.pl/g/i2274.gif" },
/*30*/          new Item{Name="Magiczny puklerz", ItemType=itemTypes[1], Level=15, Damage=0, Defense=35, Value=1210, PathToImage="https://www.naszemargo.pl/g/i2192.gif" },
                new Item{Name="Miedziana tarcza", ItemType=itemTypes[1], Level=16, Damage=0, Defense=40, Value=1590, PathToImage="https://www.naszemargo.pl/g/i7046.gif" },
                new Item{Name="Tarcza dobrej jakości", ItemType=itemTypes[1], Level=17, Damage=0, Defense=43, Value=1778, PathToImage="https://www.naszemargo.pl/g/i2565.gif" },
                new Item{Name="Mała tarcza paladyna", ItemType=itemTypes[1], Level=18, Damage=0, Defense=46, Value=1979, PathToImage="https://www.naszemargo.pl/g/i2552.gif" },
                new Item{Name="Tarcza nieumarłego", ItemType=itemTypes[1], Level=19, Damage=0, Defense=49, Value=2300, PathToImage="https://www.naszemargo.pl/g/i4443.gif" },
/*35*/          new Item{Name="Osłona z niedźwiedzich żeber", ItemType=itemTypes[1], Level=20, Damage=0, Defense=53, Value=2589, PathToImage="https://www.naszemargo.pl/g/i9529.gif" },
                new Item{Name="Tarcza młodego paladyna", ItemType=itemTypes[1], Level=22, Damage=0, Defense=58, Value=3000, PathToImage="https://www.naszemargo.pl/g/i2597.gif" },
                new Item{Name="Pawęż", ItemType=itemTypes[1], Level=23, Damage=0, Defense=61, Value=3302, PathToImage="https://www.naszemargo.pl/g/i2566.gif" },
                new Item{Name="Lekka czerwona tarcza", ItemType=itemTypes[1], Level=24, Damage=0, Defense=64, Value=3759, PathToImage="https://www.naszemargo.pl/g/i2567.gif" },
                new Item{Name="Mocarna tarcza rycerza", ItemType=itemTypes[1], Level=25, Damage=0, Defense=67, Value=4572, PathToImage="https://www.naszemargo.pl/g/i7735.gif" },
/*40*/          new Item{Name="Tarcza smoczego jeźdźca", ItemType=itemTypes[1], Level=25, Damage=0, Defense=74, Value=5484, PathToImage="https://www.naszemargo.pl/g/i7171.gif" },

                //HELMETS (41 - 53)
                new Item{Name="Skórzany hełm", ItemType=itemTypes[2], Level=1, Damage=0, Defense=1, Value=9, PathToImage="http://www.naszemargo.pl/g/i152.gif" },
                new Item{Name="Stalowa misiurka", ItemType=itemTypes[2], Level=2, Damage=0, Defense=2, Value=22, PathToImage="http://www.naszemargo.pl/g/i3898.gif" },
                new Item{Name="Wzmocniona stalowa misiurka", ItemType=itemTypes[2], Level=3, Damage=0, Defense=3, Value=40, PathToImage="http://www.naszemargo.pl/g/i3899.gif" },
                new Item{Name="Szyszak", ItemType=itemTypes[2], Level=4, Damage=0, Defense=4, Value=63, PathToImage="http://www.naszemargo.pl/g/i154.gif" },
/*45*/          new Item{Name="Hełm otwarty", ItemType=itemTypes[2], Level=5, Damage=0, Defense=5, Value=91, PathToImage="http://www.naszemargo.pl/g/i155.gif" },
                new Item{Name="Stalowa przyłbica", ItemType=itemTypes[2], Level=6, Damage=0, Defense=6, Value=124, PathToImage="http://www.naszemargo.pl/g/i3900.gif" },
                new Item{Name="Czapka leśnego elfa", ItemType=itemTypes[2], Level=9, Damage=0, Defense=8, Value=240, PathToImage="http://www.naszemargo.pl/g/i219.gif" },
                new Item{Name="Zamknięty hełm", ItemType=itemTypes[2], Level=10, Damage=0, Defense=9, Value=414, PathToImage="http://www.naszemargo.pl/g/i1905.gif" },
                new Item{Name="Czapka tropiciela", ItemType=itemTypes[2], Level=12, Damage=0, Defense=15, Value=444, PathToImage="http://www.naszemargo.pl/g/i3810.gif" },
/*50*/          new Item{Name="Nadgryziony kapelusz czarodzieja", ItemType=itemTypes[2], Level=15, Damage=0, Defense=18, Value=608, PathToImage="http://www.naszemargo.pl/g/i2221.gif" },
                new Item{Name="Hełm z przyłbicą", ItemType=itemTypes[2], Level=20, Damage=0, Defense=20, Value=1149, PathToImage="http://www.naszemargo.pl/g/i3387.gif" },
                new Item{Name="Krótka szpilka władzy", ItemType=itemTypes[2], Level=23, Damage=0, Defense=23, Value=1500, PathToImage="http://www.naszemargo.pl/g/i6507.gif" },
                new Item{Name="Zakuty łeb", ItemType=itemTypes[2], Level=25, Damage=0, Defense=26, Value=1890, PathToImage="http://www.naszemargo.pl/g/i3661.gif" },

                //ARMORS (54 - 68)
                new Item{Name="Uszkodzona zbroja", ItemType=itemTypes[3], Level=1, Damage=0, Defense=3, Value=27, PathToImage="https://www.naszemargo.pl/g/i3992.gif" },
/*55*/          new Item{Name="Lekka skórzana zbroja", ItemType=itemTypes[3], Level=2, Damage=0, Defense=6, Value=69, PathToImage="https://www.naszemargo.pl/g/i147.gif" },
                new Item{Name="Ciężka skórzana zbroja", ItemType=itemTypes[3], Level=3, Damage=0, Defense=9, Value=126, PathToImage="https://www.naszemargo.pl/g/i148.gif" },
                new Item{Name="Ciężka ćwiekowana zbroja", ItemType=itemTypes[3], Level=4, Damage=0, Defense=11, Value=171, PathToImage="https://www.naszemargo.pl/g/i1897.gif" },
                new Item{Name="Lekka zbroja płytowa", ItemType=itemTypes[3], Level=5, Damage=0, Defense=16, Value=285, PathToImage="https://www.naszemargo.pl/g/i3993.gif" },
                new Item{Name="Zbroja z platyny", ItemType=itemTypes[3], Level=6, Damage=0, Defense=19, Value=387, PathToImage="https://www.naszemargo.pl/g/i3994.gif" },
/*60*/          new Item{Name="Zbroja z tytanu", ItemType=itemTypes[3], Level=7, Damage=0, Defense=22, Value=506, PathToImage="https://www.naszemargo.pl/g/i3995.gif" },
                new Item{Name="Kolczuga", ItemType=itemTypes[3], Level=8, Damage=0, Defense=29, Value=595, PathToImage="https://www.naszemargo.pl/g/i150.gif" },
                new Item{Name="Zbroja paskowa", ItemType=itemTypes[3], Level=10, Damage=0, Defense=32, Value=861, PathToImage="https://www.naszemargo.pl/g/i1898.gif" },
                new Item{Name="Zbroja łuskowa", ItemType=itemTypes[3], Level=13, Damage=0, Defense=43, Value=1389, PathToImage="https://www.naszemargo.pl/g/i1899.gif" },
                new Item{Name="Zbroja segmentowa", ItemType=itemTypes[3], Level=15, Damage=0, Defense=50, Value=1809, PathToImage="https://www.naszemargo.pl/g/i1900.gif" },
/*65*/          new Item{Name="Stalowy napierśnik", ItemType=itemTypes[3], Level=18, Damage=0, Defense=61, Value=2670, PathToImage="https://www.naszemargo.pl/g/i1901.gif" },
                new Item{Name="Zbroja płytowa", ItemType=itemTypes[3], Level=20, Damage=0, Defense=69, Value=3102, PathToImage="https://www.naszemargo.pl/g/i1902.gif" },
                new Item{Name="Płaszcz Mietka", ItemType=itemTypes[3], Level=23, Damage=0, Defense=79, Value=3999, PathToImage="http://www.naszemargo.pl/g/i35.gif" },
                new Item{Name="Zbroja smoczego jeźdźca", ItemType=itemTypes[3], Level=25, Damage=0, Defense=89, Value=4607, PathToImage="https://www.naszemargo.pl/g/i7429.gif" },

                //GLOVES (69 - 80)
                new Item{Name="Rękawice", ItemType=itemTypes[4], Level=1, Damage=0, Defense=1, Value=11, PathToImage="http://www.naszemargo.pl/g/i3890.gif" },
/*70*/          new Item{Name="Rękawice z jeleniej skóry", ItemType=itemTypes[4], Level=3, Damage=0, Defense=2, Value=48, PathToImage="http://www.naszemargo.pl/g/i3891.gif" },
                new Item{Name="Wzmocnione rękawice", ItemType=itemTypes[4], Level=4, Damage=0, Defense=3, Value=71, PathToImage="http://www.naszemargo.pl/g/i160.gif" },
                new Item{Name="Rękawice jeźdźeckie", ItemType=itemTypes[4], Level=5, Damage=0, Defense=4, Value=149, PathToImage="http://www.naszemargo.pl/g/i3892.gif" },
                new Item{Name="Rękawice kolcze", ItemType=itemTypes[4], Level=7, Damage=0, Defense=5, Value=194, PathToImage="http://www.naszemargo.pl/g/i161.gif" },
                new Item{Name="Skórzane rękawice", ItemType=itemTypes[4], Level=10, Damage=0, Defense=7, Value=304, PathToImage="http://www.naszemargo.pl/g/i224.gif" },
/*75*/          new Item{Name="Rękawice rycerskie", ItemType=itemTypes[4], Level=11, Damage=0, Defense=8, Value=491, PathToImage="http://www.naszemargo.pl/g/i1908.gif" },
                new Item{Name="Złociste rękawice", ItemType=itemTypes[4], Level=15, Damage=0, Defense=10, Value=846, PathToImage="http://www.naszemargo.pl/g/i5767.gif" },
                new Item{Name="Rękawice z wilczej skóry", ItemType=itemTypes[4], Level=17, Damage=0, Defense=12, Value=1000, PathToImage="http://www.naszemargo.pl/g/i6801.gif" },
                new Item{Name="Rękawice szarego niedźwiedzia", ItemType=itemTypes[4], Level=20, Damage=0, Defense=15, Value=1572, PathToImage="http://www.naszemargo.pl/g/i3160.gif" },
                new Item{Name="Rękawice młodego paladyna", ItemType=itemTypes[4], Level=22, Damage=0, Defense=17, Value=1724, PathToImage="http://www.naszemargo.pl/g/i2599.gif" },
/*80*/          new Item{Name="Rękawice magiczne", ItemType=itemTypes[4], Level=25, Damage=0, Defense=20, Value=2588, PathToImage="http://www.naszemargo.pl/g/i2559.gif" },

                //BOOTS (81 - 93)
                new Item{Name="Skórzane buty", ItemType=itemTypes[5], Level=1, Damage=0, Defense=1, Value=9, PathToImage="http://www.naszemargo.pl/g/i156.gif" },
                new Item{Name="Mocne skórzane buty", ItemType=itemTypes[5], Level=2, Damage=0, Defense=2, Value=22, PathToImage="http://www.naszemargo.pl/g/i3894.gif" },
                new Item{Name="Buty z lisiej skóry", ItemType=itemTypes[5], Level=4, Damage=0, Defense=3, Value=63, PathToImage="http://www.naszemargo.pl/g/i3895.gif" },
                new Item{Name="Wzmocnione buty", ItemType=itemTypes[5], Level=5, Damage=0, Defense=4, Value=91, PathToImage="http://www.naszemargo.pl/g/i158.gif" },
/*85*/          new Item{Name="Miedziane buty", ItemType=itemTypes[5], Level=6, Damage=0, Defense=5, Value=149, PathToImage="http://www.naszemargo.pl/g/i3896.gif" },
                new Item{Name="Stalowe buty", ItemType=itemTypes[5], Level=7, Damage=0, Defense=6, Value=194, PathToImage="http://www.naszemargo.pl/g/i3897.gif" },
                new Item{Name="Buty rycerskie", ItemType=itemTypes[5], Level=10, Damage=0, Defense=8, Value=434, PathToImage="http://www.naszemargo.pl/g/i1907.gif" },
                new Item{Name="Buty z wilczej skóry", ItemType=itemTypes[5], Level=15, Damage=0, Defense=13, Value=846, PathToImage="http://www.naszemargo.pl/g/i6800.gif" },
                new Item{Name="Uszkodzone buty Thowara", ItemType=itemTypes[5], Level=17, Damage=0, Defense=15, Value=1094, PathToImage="http://www.naszemargo.pl/g/i2639.gif" },
/*90*/          new Item{Name="Dobre skórzane buty", ItemType=itemTypes[5], Level=18, Damage=0, Defense=17, Value=1724, PathToImage="http://www.naszemargo.pl/g/i3165.gif" },
                new Item{Name="Ciężkie rycerskie buty", ItemType=itemTypes[5], Level=20, Damage=0, Defense=19, Value=2200, PathToImage="http://www.naszemargo.pl/g/i6442.gif" },
                new Item{Name="Lekkie pantofle tyrana", ItemType=itemTypes[5], Level=23, Damage=0, Defense=21, Value=2785, PathToImage="http://www.naszemargo.pl/g/i6505.gif" },
                new Item{Name="Żołnierskie buty", ItemType=itemTypes[5], Level=25, Damage=0, Defense=24, Value=3100, PathToImage="http://www.naszemargo.pl/g/i6455.gif" },

                //OTHER (94 - 107)
                new Item{Name="Sierść pająka", Value=110, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://www.naszemargo.pl/g/i4681.gif" },
/*95*/          new Item{Name="Przędza pająka", Value=70, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/sur/spider_spun.gif" },
                new Item{Name="Jad pająka", Value=30, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/pot/venom01.gif" },
                new Item{Name="Oczy żaby", Value=100, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/eve/ev-oczyzaby.gif" },
                new Item{Name="Żabie udka", Value=12, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/sur/zabie-udka.gif" },
                new Item{Name="Głowa szerszenia", Value=110, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/sur/hornet_head.gif" },
/*100*/         new Item{Name="Skrzydła szerszenia", Value=27, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/sur/hornet_wing.gif" },
                new Item{Name="Żądło", Value=50, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/sur/sur23.gif" },
                new Item{Name="Lisi ogon", Value=90, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="https://www.naszemargo.pl/g/i1910.gif" },
                new Item{Name="Ogon szczura", Value=200, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/eve/ev-rattail.gif" },
                new Item{Name="Mięso", Value=11, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/kon/sur30.gif" },
/*105*/         new Item{Name="Skóra Czarnej Wilczycy", Value=500, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/sur/su30.gif" },
                new Item{Name="Kły Czarnej Wilczycy", Value=200, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/sur/su31.gif" },
                new Item{Name="Wilcze jagody", Value=56, ItemType=itemTypes[6], Level=0, Damage=0, Defense=0, PathToImage="http://emargo.pl/margonem/obrazki/itemy/neu/wilcze_jagody.gif" }
            };

            items.ForEach(i => context.Items.Add(i));
            context.SaveChanges();

            Character profileAdmin = new Character
            {
                Login = user.UserName,
                ProfileType = profileTypes[0],
                Name = "Admin",
                Level = 100,
                Health = 1000,
                HealthMax = 1000,
                Strength = 1000,
                Dexterity = 1000,
                Intelligence = 100,
                Luck = 1000,
                CharacterImage = characterImages[0],
                Gold = 0,
                NextExpedition = DateTime.Now.Subtract(TimeSpan.FromDays(1)),
                NextArenaFight = DateTime.Now.Subtract(TimeSpan.FromDays(1)),
                Armor = items[68],
                Boots = items[93],
                Gloves = items[80],
                Helmet = items[53],
                Shield = items[40],
                Weapon = items[20],
                Sawmill = new CharacterBuildings
                {
                    Building = buildings[0],
                    LastUpdate = DateTime.Now,
                    Level = 0,
                    Storage = 0,
                    StorageMax = 100
                },
                Brickyard = new CharacterBuildings
                {
                    Building = buildings[1],
                    LastUpdate = DateTime.Now,
                    Level = 0,
                    Storage = 0,
                    StorageMax = 100
                },
                Ironworks = new CharacterBuildings
                {
                    Building = buildings[2],
                    LastUpdate = DateTime.Now,
                    Level = 0,
                    Storage = 0,
                    StorageMax = 100
                }
            };

            context.Characters.Add(profileAdmin);
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
                    Location = locations[0],
                    Money =3, Experience=5,
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/zwi/mysz.gif"
                },
                new Enemy
                {
                    Name ="Pająk", Level=2, Damage=1, Defense=1, HealthMax=5, Health=5,
                    Luck =1, Strength=1, Dexterity=2,
                    Location =locations[0],
                    Money =5, Experience=7,
                    ItemLoots =new List<ItemLoot>
                    {
                        new ItemLoot {Item=items[94], DropChance=10},
                        new ItemLoot {Item=items[95], DropChance=20},
                        new ItemLoot {Item=items[96], DropChance=30}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/zwi/pajak_z1.gif"
                },
                new Enemy
                {
                    Name ="Ropucha", Level=3, Damage=3, Defense=2, HealthMax=8, Health=8,
                    Luck =1, Strength=2, Dexterity=2,
                    Location =locations[0],
                    Money =8, Experience=10,
                    ItemLoots =new List<ItemLoot>
                    {
                        new ItemLoot {Item=items[97], DropChance=15},
                        new ItemLoot {Item=items[98], DropChance=50}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/zwi/zaba01.gif"
                },
                new Enemy
                {
                    Name ="Nocny pająk", Level=4, Damage=4, Defense=3, HealthMax=12, Health=12,
                    Luck =2, Strength=2, Dexterity=2,
                    Location =locations[0],
                    Money =10, Experience=14,
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/zwi/pajak_nocny1.gif"
                },
                new Enemy
                {
                    Name ="Szerszeń", Level=5, Damage=6, Defense=6, HealthMax=17, Health=17,
                    Luck =3, Strength=3, Dexterity=3,
                    Location =locations[0],
                    Money =12, Experience=17,
                    ItemLoots = new List<ItemLoot>
                    {
                        new ItemLoot{Item=items[99], DropChance=30},
                        new ItemLoot{Item=items[100], DropChance=70},
                        new ItemLoot{Item=items[101], DropChance=50}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/zwi/bestia152.gif"
                },
                new Enemy
                {
                    Name ="Lis", Level=6, Damage=8, Defense=7, HealthMax=20, Health=20,
                    Luck =5, Strength=5, Dexterity=6,
                    Location =locations[0],
                    Money =15, Experience=20,
                    ItemLoots = new List<ItemLoot>
                    {
                        new ItemLoot{Item=items[102], DropChance=45}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/zwi/lis.gif"
                },
                new Enemy
                {
                    Name ="Król szczurów", Level=7, Damage=9, Defense=9, HealthMax=25, Health=25,
                    Luck =6, Strength=7, Dexterity=6,
                    Location =locations[1],
                    Money =20, Experience=25,
                    ItemLoots = new List<ItemLoot>
                    {
                        new ItemLoot{Item=items[103], DropChance=50}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/zwi/szczur2.gif"
                },
                new Enemy
                {
                    Name ="Gacek", Level=8, Damage=10, Defense=10, HealthMax=20, Health=20,
                    Luck =9, Strength=6, Dexterity=8,
                    Location =locations[1],
                    Money =23, Experience=30,
                    ItemLoots = new List<ItemLoot>
                    {
                        new ItemLoot{Item=items[104], DropChance=80}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/zwi/bestia86.gif"
                },
                new Enemy
                {
                    Name ="Dziki pies", Level=9, Damage=12, Defense=10, HealthMax=35, Health=35,
                    Luck =5, Strength=8, Dexterity=5,
                    Location =locations[1],
                    Money =26, Experience=35,
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/zwi/pies_czarny1.gif"
                },
                new Enemy
                {
                    Name ="Czarna Wilczyca", Level=10, Damage=15, Defense=14, HealthMax=45, Health=45,
                    Luck =7, Strength=11, Dexterity=10,
                    Location =locations[1],
                    Money =40, Experience=45,
                    ItemLoots = new List<ItemLoot>
                    {
                        new ItemLoot{Item=items[105], DropChance=10},
                        new ItemLoot{Item=items[106], DropChance=25},
                        new ItemLoot{Item=items[107], DropChance=80}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/pot/st-wilczyca.gif"
                },
                new Enemy
                {
                    Name ="Ork Łaknikąpiel", Level=11, Damage=17, Defense=17, HealthMax=50, Health=50,
                    Luck =4, Strength=15, Dexterity=10,
                    Location =locations[2],
                    Money =70, Experience=50,
                    ItemLoots = new List<ItemLoot>
                    {
                        new ItemLoot{Item=items[0], DropChance=80},
                        new ItemLoot{Item=items[4], DropChance=60},
                        new ItemLoot{Item=items[8], DropChance=40},
                        new ItemLoot{Item=items[11], DropChance=20}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/hum/ork4.gif"
                },
                new Enemy
                {
                    Name ="Karciany rycerz", Level=12, Damage=17, Defense=19, HealthMax=55, Health=55,
                    Luck =10, Strength=14, Dexterity=17,
                    Location =locations[2],
                    Money =85, Experience=55,
                    ItemLoots = new List<ItemLoot>
                    {
                        new ItemLoot{Item=items[12], DropChance=30},
                        new ItemLoot{Item=items[29], DropChance=30},
                        new ItemLoot{Item=items[49], DropChance=50},
                        new ItemLoot{Item=items[62], DropChance=25},
                        new ItemLoot{Item=items[75], DropChance=30},
                        new ItemLoot{Item=items[87], DropChance=20}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/hum/redknight3.gif"
                },
                new Enemy
                {
                    Name ="Bezgłowy jeździec", Level=14, Damage=19, Defense=21, HealthMax=65, Health=65,
                    Luck =3, Strength=18, Dexterity=10,
                    Location =locations[2],
                    Money =100, Experience=65,
                    ItemLoots = new List<ItemLoot>
                    {
                        new ItemLoot{Item=items[13], DropChance=30},
                        new ItemLoot{Item=items[11], DropChance=40},
                        new ItemLoot{Item=items[30], DropChance=30},
                        new ItemLoot{Item=items[63], DropChance=30},
                        new ItemLoot{Item=items[76], DropChance=30},
                        new ItemLoot{Item=items[88], DropChance=20}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/hum/bezglowy_jezdziec.gif"
                },
                new Enemy
                {
                    Name ="Paladyński Apostata", Level=15, Damage=21, Defense=20, HealthMax=75, Health=75,
                    Luck =13, Strength=20, Dexterity=20,
                    Location =locations[2],
                    Money =150, Experience=75,
                    ItemLoots = new List<ItemLoot>
                    {
                        new ItemLoot{Item=items[17], DropChance=20},
                        new ItemLoot{Item=items[33], DropChance=40},
                        new ItemLoot{Item=items[36], DropChance=15},
                        new ItemLoot{Item=items[79], DropChance=10}
                    },
                    PathToImage="http://emargo.pl/margonem/obrazki/npc/woj/apostata.gif"
                }
            };

            enemies.ForEach(e => context.Enemies.Add(e));
            context.SaveChanges();

        }
    }
}