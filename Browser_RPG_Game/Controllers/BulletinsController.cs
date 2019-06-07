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
    public class BulletinsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Bulletins
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            var bulletins = db.Bulletins.Include(b => b.Character);
            return View(bulletins.ToList());
        }

        // GET: Bulletins/Details/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bulletin bulletin = db.Bulletins.Find(id);
            if (bulletin == null)
            {
                return HttpNotFound();
            }
            return View(bulletin);
        }

        // GET: Bulletins/Create
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.CharacterID = new SelectList(db.Characters, "ID", "Login");
            return View();
        }

        // POST: Bulletins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CharacterID,Title,Contents,Date")] Bulletin bulletin)
        {
            if (ModelState.IsValid)
            {
                bulletin.Date = DateTime.Now;
                db.Bulletins.Add(bulletin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CharacterID = new SelectList(db.Characters, "ID", "Login", bulletin.CharacterID);
            return View(bulletin);
        }

        // GET: Bulletins/Edit/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bulletin bulletin = db.Bulletins.Find(id);
            if (bulletin == null)
            {
                return HttpNotFound();
            }
            ViewBag.CharacterID = new SelectList(db.Characters, "ID", "Login", bulletin.CharacterID);
            return View(bulletin);
        }

        // POST: Bulletins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            Bulletin bulletin = db.Bulletins.Find(id);
            if (ModelState.IsValid)
            {
                UpdateModel(bulletin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CharacterID = new SelectList(db.Characters, "ID", "Login", bulletin.CharacterID);
            return View(bulletin);
        }

        // GET: Bulletins/Delete/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bulletin bulletin = db.Bulletins.Find(id);
            if (bulletin == null)
            {
                return HttpNotFound();
            }
            return View(bulletin);
        }

        // POST: Bulletins/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bulletin bulletin = db.Bulletins.Find(id);
            db.Bulletins.Remove(bulletin);
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
