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
        public ActionResult Index()
        {
            var enemies = db.Enemies.Include(e => e.Location).Include(e => e.Loot);
            return View(enemies.ToList());
        }

        // GET: Enemies/Details/5
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
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name");
            ViewBag.LootID = new SelectList(db.Loots, "ID", "ID");
            return View();
        }

        // POST: Enemies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Level,Health,HealthMax,Strength,Dexterity,Intelligence,Luck,Damage,Defense,LootID,LocationID,PathToImage")] Enemy enemy)
        {
            if (ModelState.IsValid)
            {
                db.Enemies.Add(enemy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", enemy.LocationID);
            ViewBag.LootID = new SelectList(db.Loots, "ID", "ID", enemy.LootID);
            return View(enemy);
        }

        // GET: Enemies/Edit/5
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
            ViewBag.LootID = new SelectList(db.Loots, "ID", "ID", enemy.LootID);
            return View(enemy);
        }

        // POST: Enemies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Level,Health,HealthMax,Strength,Dexterity,Intelligence,Luck,Damage,Defense,LootID,LocationID,PathToImage")] Enemy enemy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enemy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", enemy.LocationID);
            ViewBag.LootID = new SelectList(db.Loots, "ID", "ID", enemy.LootID);
            return View(enemy);
        }

        // GET: Enemies/Delete/5
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
