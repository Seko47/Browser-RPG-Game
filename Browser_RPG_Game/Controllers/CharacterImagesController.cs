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
    public class CharacterImagesController : Controller
    {
        private GameContext db = new GameContext();

        // GET: CharacterImages
        public ActionResult Index()
        {
            return View(db.CharacterImages.ToList());
        }

        // GET: CharacterImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterImage characterImage = db.CharacterImages.Find(id);
            if (characterImage == null)
            {
                return HttpNotFound();
            }
            return View(characterImage);
        }

        // GET: CharacterImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CharacterImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PathToImage")] CharacterImage characterImage)
        {
            if (ModelState.IsValid)
            {
                db.CharacterImages.Add(characterImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(characterImage);
        }

        // GET: CharacterImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterImage characterImage = db.CharacterImages.Find(id);
            if (characterImage == null)
            {
                return HttpNotFound();
            }
            return View(characterImage);
        }

        // POST: CharacterImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            CharacterImage characterImage = db.CharacterImages.Find(id);
            if (ModelState.IsValid)
            {
                UpdateModel(characterImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(characterImage);
        }

        // GET: CharacterImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CharacterImage characterImage = db.CharacterImages.Find(id);
            if (characterImage == null)
            {
                return HttpNotFound();
            }
            return View(characterImage);
        }

        // POST: CharacterImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterImage characterImage = db.CharacterImages.Find(id);
            db.CharacterImages.Remove(characterImage);
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
