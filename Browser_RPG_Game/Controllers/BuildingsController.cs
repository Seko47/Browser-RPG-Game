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
    public class BuildingsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Buildings
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            var buildings = db.Buildings.Include(b => b.Material);
            return View(buildings.ToList());
        }

        // GET: Buildings/Details/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Buildings/Create
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.MaterialID = new SelectList(db.Materials, "ID", "Name");
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaterialID,Name,PathToImage,LevelMax,Value,InitialIncreasePerMinute,IncreasePerMinuteAfterEachUpgrade")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Buildings.Add(building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaterialID = new SelectList(db.Materials, "ID", "Name", building.MaterialID);
            return View(building);
        }

        // GET: Buildings/Edit/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaterialID = new SelectList(db.Materials, "ID", "Name", building.MaterialID);
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            Building building = db.Buildings.Find(id);
            if (ModelState.IsValid)
            {
                UpdateModel(building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaterialID = new SelectList(db.Materials, "ID", "Name", building.MaterialID);
            return View(building);
        }

        // GET: Buildings/Delete/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = db.Buildings.Find(id);
            db.Buildings.Remove(building);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Estate()
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            character.Sawmill.UpdateStorage();
            character.Brickyard.UpdateStorage();
            character.Ironworks.UpdateStorage();

            db.SaveChanges();

            return View(character);
        }

        [Authorize]
        public ActionResult Update(int id)
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            CharacterBuildings characterBuildings = db.CharacterBuildings.Where(cb => cb.ID == id).FirstOrDefault();

            if (characterBuildings != null
                && (characterBuildings.ID == character.Sawmill.ID
                    || characterBuildings.ID == character.Brickyard.ID
                    || characterBuildings.ID == character.Ironworks.ID))
            {
                if (character.Gold >= characterBuildings.UpdateCost())
                {
                    character.Gold -= characterBuildings.UpdateCost();

                    characterBuildings.Update();

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Estate");
        }

        [Authorize]
        public ActionResult Sell(int id)
        {
            GameContext db = new GameContext();

            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);

            CharacterBuildings characterBuildings = db.CharacterBuildings.Where(cb => cb.ID == id).FirstOrDefault();

            if (characterBuildings != null
                && (characterBuildings.ID == character.Sawmill.ID
                    || characterBuildings.ID == character.Brickyard.ID
                    || characterBuildings.ID == character.Ironworks.ID))
            {

                character.Gold += characterBuildings.Sell();

                db.SaveChanges();
            }

            return RedirectToAction("Estate");
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
