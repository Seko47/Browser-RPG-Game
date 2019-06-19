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
    public class EnemiesController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Enemies
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            var enemies = db.Enemies.Include(e => e.Location);
            return View(enemies.OrderBy(e=>e.LocationID).ThenBy(e=>e.Level));
        }

        // GET: Enemies/Details/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enemy enemy = db.Enemies.Find(id);
            if (enemy == null)
            {
                return HttpNotFound();
            }
            return View(enemy);
        }

        // GET: Enemies/Create
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name");
            ViewBag.ItemsID = db.Items.ToList();
            return View();
        }

        // POST: Enemies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Level,Health,HealthMax,Strength,Dexterity,Intelligence,Luck,Damage,Defense,Experience,Money,LocationID,PathToImage")] Enemy enemy, int[] ItemsID)
        {
            if (ModelState.IsValid)
            {
                enemy.ItemLoots = new List<ItemLoot>();
                for (int i = 0; i < ItemsID.Count(); ++i)
                {
                    enemy.ItemLoots.Add(new ItemLoot { Item = db.Items.Find(ItemsID[i]), DropChance = Int32.Parse(Request["item" + ItemsID[i]]) });
                }

                db.Enemies.Add(enemy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", enemy.LocationID);
            return View(enemy);
        }

        // GET: Enemies/Edit/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enemy enemy = db.Enemies.Find(id);
            if (enemy == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", enemy.LocationID);
            ViewBag.ItemsID = db.Items.ToList();
            return View(enemy);
        }

        // POST: Enemies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Level,Health,HealthMax,Strength,Dexterity,Intelligence,Luck,Damage,Defense,Experience,Money,LocationID,PathToImage")] Enemy enemy, int[] ItemsID)
        {
            if (ModelState.IsValid)
            {
                List<ItemLoot> itemLoots = db.ItemLoots.Where(i => i.EnemyID == enemy.ID).ToList();
                itemLoots.ForEach(i => db.ItemLoots.Remove(i));
                db.SaveChanges();

                enemy.ItemLoots = new List<ItemLoot>();
                for (int i = 0; i < ItemsID.Count(); ++i)
                {
                    ItemLoot itemLoot = new ItemLoot { Item = db.Items.Find(ItemsID[i]), DropChance = Int32.Parse(Request["item" + ItemsID[i]]), Enemy = enemy };
                    db.ItemLoots.Add(itemLoot);
                    enemy.ItemLoots.Add(itemLoot);
                }

                db.Entry(enemy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", enemy.LocationID);
            return View(enemy);
        }

        // GET: Enemies/Delete/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enemy enemy = db.Enemies.Find(id);
            if (enemy == null)
            {
                return HttpNotFound();
            }
            return View(enemy);
        }

        // POST: Enemies/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enemy enemy = db.Enemies.Find(id);
            db.Enemies.Remove(enemy);
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
