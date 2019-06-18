using Browser_RPG_Game.DAL;
using Browser_RPG_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Browser_RPG_Game.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                GameContext db = new GameContext();
                Character character = db.Characters.Single(p => p.Login == User.Identity.Name);
                ViewBag.Character = character;
            }

            return View();
        }

        [Authorize]
        public ActionResult NextAvatar()
        {
            if (User.IsInRole("ADMIN"))
            {
                GameContext db = new GameContext();

                Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

                CharacterImage characterImage = db.CharacterImages.Single(ci => ci.ID == (((character.CharacterImageID + 1) % (db.CharacterImages.Count() + 1)) == 0 ? 1 : ((character.CharacterImageID + 1) % (db.CharacterImages.Count() + 1))));

                character.CharacterImage = characterImage;
                db.SaveChanges();
            }
            else
            {
                GameContext db = new GameContext();

                Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

                CharacterImage characterImage = db.CharacterImages.Single(ci => ci.ID == (((character.CharacterImageID + 1) % (db.CharacterImages.Count() + 1)) == 0 ? 2 : ((character.CharacterImageID + 1) % (db.CharacterImages.Count() + 1))));

                character.CharacterImage = characterImage;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult PreviouseAvatar()
        {
            if (User.IsInRole("ADMIN"))
            {
                GameContext db = new GameContext();

                Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

                CharacterImage characterImage = db.CharacterImages.Single(ci => ci.ID == (((character.CharacterImageID - 1) % (db.CharacterImages.Count() + 1)) == 0 ? 6 : ((character.CharacterImageID - 1) % (db.CharacterImages.Count() + 1))));

                character.CharacterImage = characterImage;
                db.SaveChanges();
            }
            else
            {
                GameContext db = new GameContext();

                Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

                CharacterImage characterImage = db.CharacterImages.Single(ci => ci.ID == (((character.CharacterImageID - 1) % (db.CharacterImages.Count() + 1)) == 1 ? 6 : ((character.CharacterImageID - 1) % (db.CharacterImages.Count() + 1))));

                character.CharacterImage = characterImage;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult TakeOffTheItem(int id)
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            if (character.HasEquippedItemByID(id))
            {
                character.TakeOffItem(id);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult PutOnTheItem(int id)
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            if (character.HasItemInInventoryById(id))
            {
                character.PutOnItem(id);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult IncreaseStrength()
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            if (character.Gold >= character.StrengthCost)
            {
                character.Gold -= character.StrengthCost;
                ++character.Strength;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult IncreaseDexterity()
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            if (character.Gold >= character.DexterityCost)
            {
                character.Gold -= character.DexterityCost;
                ++character.Dexterity;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult IncreaseIntelligence()
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            if (character.Gold >= character.IntelligenceCost)
            {
                character.Gold -= character.IntelligenceCost;
                ++character.Intelligence;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult IncreaseLuck()
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            if (character.Gold >= character.LuckCost)
            {
                character.Gold -= character.LuckCost;
                ++character.Luck;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}