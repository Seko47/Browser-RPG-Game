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
    public class ItemLootsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: ItemLoots
        public ActionResult Index()
        {
            var itemLoots = db.ItemLoots.Include(i => i.Item).Include(i => i.Loot);
            return View(itemLoots.ToList());
        }

        // GET: ItemLoots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLoot itemLoot = db.ItemLoots.Find(id);
            if (itemLoot == null)
            {
                return HttpNotFound();
            }
            return View(itemLoot);
        }

        // GET: ItemLoots/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name");
            ViewBag.LootID = new SelectList(db.Loots, "ID", "ID");
            return View();
        }

        // POST: ItemLoots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LootID,ItemID,DropChance")] ItemLoot itemLoot)
        {
            if (ModelState.IsValid)
            {
                db.ItemLoots.Add(itemLoot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", itemLoot.ItemID);
            ViewBag.LootID = new SelectList(db.Loots, "ID", "ID", itemLoot.LootID);
            return View(itemLoot);
        }

        // GET: ItemLoots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLoot itemLoot = db.ItemLoots.Find(id);
            if (itemLoot == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", itemLoot.ItemID);
            ViewBag.LootID = new SelectList(db.Loots, "ID", "ID", itemLoot.LootID);
            return View(itemLoot);
        }

        // POST: ItemLoots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LootID,ItemID,DropChance")] ItemLoot itemLoot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemLoot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", itemLoot.ItemID);
            ViewBag.LootID = new SelectList(db.Loots, "ID", "ID", itemLoot.LootID);
            return View(itemLoot);
        }

        // GET: ItemLoots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLoot itemLoot = db.ItemLoots.Find(id);
            if (itemLoot == null)
            {
                return HttpNotFound();
            }
            return View(itemLoot);
        }

        // POST: ItemLoots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemLoot itemLoot = db.ItemLoots.Find(id);
            db.ItemLoots.Remove(itemLoot);
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
