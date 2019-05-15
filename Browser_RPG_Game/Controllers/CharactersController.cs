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
        public ActionResult Index()
        {
            var characters = db.Characters.Include(c => c.Armor).Include(c => c.Boots).Include(c => c.Brickyard).Include(c => c.CharacterImage).Include(c => c.Gloves).Include(c => c.Helmet).Include(c => c.Ironworks).Include(c => c.ProfileType).Include(c => c.Sawmill).Include(c => c.Shield).Include(c => c.Weapon);
            return View(characters.ToList());
        }

        // GET: Characters/Details/5
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
        public ActionResult Create()
        {
            ViewBag.ArmorID = new SelectList(db.Items, "ID", "Name");
            ViewBag.BootsID = new SelectList(db.Items, "ID", "Name");
            ViewBag.BrickyardID = new SelectList(db.CharacterBuildings, "ID", "ID");
            ViewBag.CharacterImageID = new SelectList(db.CharacterImages, "ID", "PathToImage");
            ViewBag.GlovesID = new SelectList(db.Items, "ID", "Name");
            ViewBag.HelmetID = new SelectList(db.Items, "ID", "Name");
            ViewBag.IronworksID = new SelectList(db.CharacterBuildings, "ID", "ID");
            ViewBag.ProfileTypeID = new SelectList(db.ProfileTypes, "ID", "Name");
            ViewBag.SawmillID = new SelectList(db.CharacterBuildings, "ID", "ID");
            ViewBag.ShieldID = new SelectList(db.Items, "ID", "Name");
            ViewBag.WeaponID = new SelectList(db.Items, "ID", "Name");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Login,ProfileTypeID,Name,Level,Experience,ExperienceMax,Health,HealthMax,Strength,Dexterity,Intelligence,Luck,HelmetID,ArmorID,GlovesID,BootsID,WeaponID,ShieldID,CharacterImageID,SawmillID,BrickyardID,IronworksID,Damage,Defense")] Character character)
        {
            if (ModelState.IsValid)
            {
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

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Login,ProfileTypeID,Name,Level,Experience,ExperienceMax,Health,HealthMax,Strength,Dexterity,Intelligence,Luck,HelmetID,ArmorID,GlovesID,BootsID,WeaponID,ShieldID,CharacterImageID,SawmillID,BrickyardID,IronworksID,Damage,Defense")] Character character)
        {
            if (ModelState.IsValid)
            {
                db.Entry(character).State = EntityState.Modified;
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
