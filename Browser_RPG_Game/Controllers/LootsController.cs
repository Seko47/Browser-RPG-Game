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
    public class LootsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Loots
        public ActionResult Index()
        {
            return View(db.Loots.ToList());
        }

        // GET: Loots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loot loot = db.Loots.Find(id);
            if (loot == null)
            {
                return HttpNotFound();
            }
            return View(loot);
        }

        // GET: Loots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Loots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Experience,Money")] Loot loot)
        {
            if (ModelState.IsValid)
            {
                db.Loots.Add(loot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loot);
        }

        // GET: Loots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loot loot = db.Loots.Find(id);
            if (loot == null)
            {
                return HttpNotFound();
            }
            return View(loot);
        }

        // POST: Loots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Experience,Money")] Loot loot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loot);
        }

        // GET: Loots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loot loot = db.Loots.Find(id);
            if (loot == null)
            {
                return HttpNotFound();
            }
            return View(loot);
        }

        // POST: Loots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loot loot = db.Loots.Find(id);
            db.Loots.Remove(loot);
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
