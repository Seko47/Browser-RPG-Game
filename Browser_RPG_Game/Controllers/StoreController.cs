﻿using Browser_RPG_Game.DAL;
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
                int value = item.Value * (2 - (character.Intelligence / 1000));
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
                int value = item.Value / (2 - (character.Intelligence / 1000));

                if (character.HasItemInInventoryById(id))
                {
                    character.RemoveItemFromEquipment(item);

                    character.Gold += value;
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}