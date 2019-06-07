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
    public class ProfileTypesController : Controller
    {
        private GameContext db = new GameContext();

        // GET: ProfileTypes
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            return View(db.ProfileTypes.ToList());
        }

        // GET: ProfileTypes/Details/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileType profileType = db.ProfileTypes.Find(id);
            if (profileType == null)
            {
                return HttpNotFound();
            }
            return View(profileType);
        }

        // GET: ProfileTypes/Create
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] ProfileType profileType)
        {
            if (ModelState.IsValid)
            {
                db.ProfileTypes.Add(profileType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profileType);
        }

        // GET: ProfileTypes/Edit/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileType profileType = db.ProfileTypes.Find(id);
            if (profileType == null)
            {
                return HttpNotFound();
            }
            return View(profileType);
        }

        // POST: ProfileTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] ProfileType profileType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profileType);
        }

        // GET: ProfileTypes/Delete/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileType profileType = db.ProfileTypes.Find(id);
            if (profileType == null)
            {
                return HttpNotFound();
            }
            return View(profileType);
        }

        // POST: ProfileTypes/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfileType profileType = db.ProfileTypes.Find(id);
            db.ProfileTypes.Remove(profileType);
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
