using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Browser_RPG_Game.DAL;
using Browser_RPG_Game.Models;

namespace Browser_RPG_Game.Controllers
{
    public class CharactersController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Characters
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            var characters = db.Characters.Include(c => c.Armor).Include(c => c.Boots).Include(c => c.Brickyard).Include(c => c.CharacterImage).Include(c => c.Gloves).Include(c => c.Helmet).Include(c => c.Ironworks).Include(c => c.ProfileType).Include(c => c.Sawmill).Include(c => c.Shield).Include(c => c.Weapon);
            return View(characters.ToList());
        }

        // GET: Characters/Details/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // GET: Characters/Create
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            Item blankItem = new Item { ID = -1, Name = "Brak" };
            List<Item> armorsItems = db.Items.Where(i => i.ItemType.Name == "armor").ToList();
            armorsItems.Insert(0, blankItem);
            List<Item> bootsItems = db.Items.Where(i => i.ItemType.Name == "boots").ToList();
            bootsItems.Insert(0, blankItem);
            List<Item> glovesItems = db.Items.Where(i => i.ItemType.Name == "gloves").ToList();
            glovesItems.Insert(0, blankItem);
            List<Item> helmetsItems = db.Items.Where(i => i.ItemType.Name == "helmet").ToList();
            helmetsItems.Insert(0, blankItem);
            List<Item> shieldsItems = db.Items.Where(i => i.ItemType.Name == "shield").ToList();
            shieldsItems.Insert(0, blankItem);
            List<Item> weaponsItems = db.Items.Where(i => i.ItemType.Name == "weapon").ToList();
            weaponsItems.Insert(0, blankItem);
            ViewBag.ArmorID = new SelectList(armorsItems, "ID", "Name");
            ViewBag.BootsID = new SelectList(bootsItems, "ID", "Name");
            ViewBag.BrickyardID = new SelectList(db.CharacterBuildings, "ID", "ID");
            ViewBag.CharacterImageID = new SelectList(db.CharacterImages, "ID", "PathToImage");
            ViewBag.GlovesID = new SelectList(glovesItems, "ID", "Name");
            ViewBag.HelmetID = new SelectList(helmetsItems, "ID", "Name");
            ViewBag.IronworksID = new SelectList(db.CharacterBuildings, "ID", "ID");
            ViewBag.ProfileTypeID = new SelectList(db.ProfileTypes, "ID", "Name");
            ViewBag.SawmillID = new SelectList(db.CharacterBuildings, "ID", "ID");
            ViewBag.ShieldID = new SelectList(shieldsItems, "ID", "Name");
            ViewBag.WeaponID = new SelectList(weaponsItems, "ID", "Name");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Login,ProfileTypeID,Name,Level,Experience,ExperienceMax,Health,HealthMax,Strength,Dexterity,Intelligence,Luck,HelmetID,ArmorID,GlovesID,BootsID,WeaponID,ShieldID,CharacterImageID,SawmillID,BrickyardID,IronworksID,Damage,Defense")] Character character)
        {
            if (ModelState.IsValid)
            {
                if (character.HelmetID == -1)
                {
                    character.HelmetID = null;
                }
                if (character.ArmorID == -1)
                {
                    character.ArmorID = null;
                }
                if (character.GlovesID == -1)
                {
                    character.GlovesID = null;
                }
                if (character.BootsID == -1)
                {
                    character.BootsID = null;
                }
                if (character.WeaponID == -1)
                {
                    character.WeaponID = null;
                }
                if (character.ShieldID == -1)
                {
                    character.ShieldID = null;
                }
                db.Characters.Add(character);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArmorID = new SelectList(db.Items, "ID", "Name", character.ArmorID);
            ViewBag.BootsID = new SelectList(db.Items, "ID", "Name", character.BootsID);
            ViewBag.BrickyardID = new SelectList(db.CharacterBuildings, "ID", "ID", character.BrickyardID);
            ViewBag.CharacterImageID = new SelectList(db.CharacterImages, "ID", "PathToImage", character.CharacterImageID);
            ViewBag.GlovesID = new SelectList(db.Items, "ID", "Name", character.GlovesID);
            ViewBag.HelmetID = new SelectList(db.Items, "ID", "Name", character.HelmetID);
            ViewBag.IronworksID = new SelectList(db.CharacterBuildings, "ID", "ID", character.IronworksID);
            ViewBag.ProfileTypeID = new SelectList(db.ProfileTypes, "ID", "Name", character.ProfileTypeID);
            ViewBag.SawmillID = new SelectList(db.CharacterBuildings, "ID", "ID", character.SawmillID);
            ViewBag.ShieldID = new SelectList(db.Items, "ID", "Name", character.ShieldID);
            ViewBag.WeaponID = new SelectList(db.Items, "ID", "Name", character.WeaponID);
            return View(character);
        }

        // GET: Characters/Edit/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }

            Item blankItem = new Item { ID = -1, Name = "Brak" };
            List<Item> armorsItems = db.Items.Where(i => i.ItemType.Name == "armor").ToList();
            armorsItems.Insert(0, blankItem);
            List<Item> bootsItems = db.Items.Where(i => i.ItemType.Name == "boots").ToList();
            bootsItems.Insert(0, blankItem);
            List<Item> glovesItems = db.Items.Where(i => i.ItemType.Name == "gloves").ToList();
            glovesItems.Insert(0, blankItem);
            List<Item> helmetsItems = db.Items.Where(i => i.ItemType.Name == "helmet").ToList();
            helmetsItems.Insert(0, blankItem);
            List<Item> shieldsItems = db.Items.Where(i => i.ItemType.Name == "shield").ToList();
            shieldsItems.Insert(0, blankItem);
            List<Item> weaponsItems = db.Items.Where(i => i.ItemType.Name == "weapon").ToList();
            weaponsItems.Insert(0, blankItem);

            ViewBag.ArmorID = new SelectList(armorsItems, "ID", "Name", character.ArmorID);
            ViewBag.BootsID = new SelectList(bootsItems, "ID", "Name", character.BootsID);
            ViewBag.BrickyardID = new SelectList(db.CharacterBuildings, "ID", "ID", character.BrickyardID);
            ViewBag.CharacterImageID = new SelectList(db.CharacterImages, "ID", "PathToImage", character.CharacterImageID);
            ViewBag.GlovesID = new SelectList(glovesItems, "ID", "Name", character.GlovesID);
            ViewBag.HelmetID = new SelectList(helmetsItems, "ID", "Name", character.HelmetID);
            ViewBag.IronworksID = new SelectList(db.CharacterBuildings, "ID", "ID", character.IronworksID);
            ViewBag.ProfileTypeID = new SelectList(db.ProfileTypes, "ID", "Name", character.ProfileTypeID);
            ViewBag.SawmillID = new SelectList(db.CharacterBuildings, "ID", "ID", character.SawmillID);
            ViewBag.ShieldID = new SelectList(shieldsItems, "ID", "Name", character.ShieldID);
            ViewBag.WeaponID = new SelectList(weaponsItems, "ID", "Name", character.WeaponID);
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            Character character = db.Characters.Find(id);
            if (ModelState.IsValid)
            {
                UpdateModel(character);
                if (character.HelmetID == -1)
                {
                    character.HelmetID = null;
                }
                if (character.ArmorID == -1)
                {
                    character.ArmorID = null;
                }
                if (character.GlovesID == -1)
                {
                    character.GlovesID = null;
                }
                if (character.BootsID == -1)
                {
                    character.BootsID = null;
                }
                if (character.WeaponID == -1)
                {
                    character.WeaponID = null;
                }
                if (character.ShieldID == -1)
                {
                    character.ShieldID = null;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArmorID = new SelectList(db.Items, "ID", "Name", character.ArmorID);
            ViewBag.BootsID = new SelectList(db.Items, "ID", "Name", character.BootsID);
            ViewBag.BrickyardID = new SelectList(db.CharacterBuildings, "ID", "ID", character.BrickyardID);
            ViewBag.CharacterImageID = new SelectList(db.CharacterImages, "ID", "PathToImage", character.CharacterImageID);
            ViewBag.GlovesID = new SelectList(db.Items, "ID", "Name", character.GlovesID);
            ViewBag.HelmetID = new SelectList(db.Items, "ID", "Name", character.HelmetID);
            ViewBag.IronworksID = new SelectList(db.CharacterBuildings, "ID", "ID", character.IronworksID);
            ViewBag.ProfileTypeID = new SelectList(db.ProfileTypes, "ID", "Name", character.ProfileTypeID);
            ViewBag.SawmillID = new SelectList(db.CharacterBuildings, "ID", "ID", character.SawmillID);
            ViewBag.ShieldID = new SelectList(db.Items, "ID", "Name", character.ShieldID);
            ViewBag.WeaponID = new SelectList(db.Items, "ID", "Name", character.WeaponID);
            return View(character);
        }

        // GET: Characters/Delete/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Characters/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Character character = db.Characters.Find(id);
            db.Characters.Remove(character);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
