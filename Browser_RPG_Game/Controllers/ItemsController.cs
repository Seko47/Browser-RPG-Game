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
using PagedList;

namespace Browser_RPG_Game.Controllers
{
    public class ItemsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Items
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.LevelSortParam = sortOrder == "Level" ? "level_desc" : "Level";
            ViewBag.DamageSortParam = sortOrder == "Damage" ? "damage_desc" : "Damage";
            ViewBag.DefenseSortParam = sortOrder == "Defense" ? "defense_desc" : "Defense";
            ViewBag.ValueSortParam = sortOrder == "Value" ? "value_desc" : "Value";
            ViewBag.ItemTypeSortParam = sortOrder == "ItemType" ? "itemtype_desc" : "ItemType";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = db.Items.Include(i => i.ItemType);

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Name.Contains(searchString) || i.ItemType.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(i => i.Name);
                    break;
                case "Level":
                    items = items.OrderBy(i => i.Level);
                    break;
                case "level_desc":
                    items = items.OrderByDescending(i => i.Level);
                    break;
                case "Damage":
                    items = items.OrderBy(i => i.Damage);
                    break;
                case "damage_desc":
                    items = items.OrderByDescending(i => i.Damage);
                    break;
                case "Defense":
                    items = items.OrderBy(i => i.Defense);
                    break;
                case "defense_desc":
                    items = items.OrderByDescending(i => i.Defense);
                    break;
                case "Value":
                    items = items.OrderBy(i => i.Value);
                    break;
                case "value_desc":
                    items = items.OrderByDescending(i => i.Value);
                    break;
                case "ItemType":
                    items = items.OrderBy(i => i.ItemType.Name);
                    break;
                case "itemtype_desc":
                    items = items.OrderByDescending(i => i.ItemType.Name);
                    break;
                default:
                    items = items.OrderBy(i => i.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ID", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ItemTypeID,Name,Level,Damage,Defense,Value,PathToImage")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ID", "Name", item.ItemTypeID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ID", "Name", item.ItemTypeID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ItemTypeID,Name,Level,Damage,Defense,Value,PathToImage")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ID", "Name", item.ItemTypeID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
