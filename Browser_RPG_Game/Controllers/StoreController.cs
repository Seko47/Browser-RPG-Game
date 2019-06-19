using Browser_RPG_Game.DAL;
using Browser_RPG_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Browser_RPG_Game.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        [Authorize]
        public ActionResult Index()
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            ViewBag.Character = character;

            List<Item> items = db.Items.Where(i => i.Level <= character.Level && i.ItemType.Name != "other").OrderBy(i => i.ItemTypeID).ToList();

            return View(items);
        }

        [Authorize]
        public ActionResult Buy(int id)
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            Item item = db.Items.FirstOrDefault(i => i.ID == id);

            if (item != null && item.Level <= character.Level && item.ItemType.Name != "other")
            {
                int value = (int)((double)item.Value * ((double)2 - (double)((double)character.Intelligence / (double)100)));
                if (character.Gold >= value)
                {
                    character.Gold -= value;

                    character.AddItemToEquipment(item);

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Sell(int id)
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            Item item = db.Items.FirstOrDefault(i => i.ID == id);

            if (item != null)
            {
                int value = (int)((double)item.Value / ((double)2 - (double)((double)character.Intelligence / (double)100)));
                if (character.HasItemInInventoryById(id))
                {
                    character.RemoveItemFromEquipment(item);

                    character.Gold += value;
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult SellAll()
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            List<Equipment> equipmentTmp = character.Equipments.ToList();

            for (int i = 0; i < equipmentTmp.Count; ++i)
            {
                Equipment tmp = equipmentTmp[i];

                int value = (int)((double)((double)tmp.Item.Value * (double)tmp.Quantity) / ((double)2 - (double)((double)character.Intelligence / (double)100)));

                if (db.Equipments.Any(e => e.CharacterID == character.ID && e.ItemID == tmp.Item.ID))
                {
                    db.Equipments.RemoveRange(db.Equipments.Where(e => e.CharacterID == character.ID && e.ItemID == tmp.Item.ID));
                }

                character.Gold += value;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}