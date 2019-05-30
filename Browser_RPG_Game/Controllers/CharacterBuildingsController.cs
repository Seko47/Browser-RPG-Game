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
    public class CharacterBuildingsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: CharacterBuildings
        public ActionResult Index()
        {
            var properties = db.CharacterBuildings.Include(c => c.Building);
            return View(properties.ToList());
        }

        // GET: CharacterBuildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterBuildings characterBuildings = db.CharacterBuildings.Find(id);
            if (characterBuildings == null)
            {
                return HttpNotFound();
            }
            return View(characterBuildings);
        }

        // GET: CharacterBuildings/Create
        public ActionResult Create()
        {
            ViewBag.BuildingID = new SelectList(db.Buildings, "ID", "Name");
            return View();
        }

        // POST: CharacterBuildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BuildingID,Level,Storage,StorageMax,LastUpdate")] CharacterBuildings characterBuildings)
        {
            if (ModelState.IsValid)
            {
                characterBuildings.LastUpdate = DateTime.Now;
                db.CharacterBuildings.Add(characterBuildings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuildingID = new SelectList(db.Buildings, "ID", "Name", characterBuildings.BuildingID);
            return View(characterBuildings);
        }

        // GET: CharacterBuildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterBuildings characterBuildings = db.CharacterBuildings.Find(id);
            if (characterBuildings == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingID = new SelectList(db.Buildings, "ID", "Name", characterBuildings.BuildingID);
            return View(characterBuildings);
        }

        // POST: CharacterBuildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            CharacterBuildings characterBuildings = db.CharacterBuildings.Find(id);
            if (ModelState.IsValid)
            {
                UpdateModel(characterBuildings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildingID = new SelectList(db.Buildings, "ID", "Name", characterBuildings.BuildingID);
            return View(characterBuildings);
        }

        // GET: CharacterBuildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterBuildings characterBuildings = db.CharacterBuildings.Find(id);
            if (characterBuildings == null)
            {
                return HttpNotFound();
            }
            return View(characterBuildings);
        }

        // POST: CharacterBuildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterBuildings characterBuildings = db.CharacterBuildings.Find(id);
            db.CharacterBuildings.Remove(characterBuildings);
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
