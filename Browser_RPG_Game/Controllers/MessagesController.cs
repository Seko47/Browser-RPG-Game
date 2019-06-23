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
    public class MessagesController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Messages
        [Authorize]
        public ActionResult Index(int? receivedPage, int? sendedPage)
        {
            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);
            var messages = db.Messages.Include(m => m.Receiver).Include(m => m.Sender);

            var receivedMessages = character.ReceivedMessages.OrderBy(m => !m.Readed).ThenByDescending(m => m.SendDate);
            var sendedMessages = character.SendedMessages.OrderByDescending(m => m.SendDate);

            int pageSize = 5;
            int receivedPageNumber = (receivedPage ?? 1);
            int sendedPageNumber = (sendedPage ?? 1);

            ViewBag.ReceivedPage = receivedPageNumber;
            ViewBag.SendedPage = sendedPageNumber;

            return View(new Tuple<IPagedList<Message>, IPagedList<Message>>(receivedMessages.ToPagedList(receivedPageNumber, pageSize), sendedMessages.ToPagedList(sendedPageNumber, pageSize)));
        }

        // GET: Messages/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);
            if (message == null && message.ReceiverID != character.ID && message.SenderID != character.ID)
            {
                return HttpNotFound();
            }

            if (message.ReceiverID == character.ID && !message.Readed)
            {
                message.Readed = true;

                db.SaveChanges();
            }

            return View(message);
        }

        // GET: Messages/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Message message)
        {

            message.Sender = db.Characters.Single(c => c.Login == User.Identity.Name);
            message.SenderID = message.Sender.ID;
            message.Receiver = db.Characters.Single(c => c.Name == message.Receiver.Name);
            message.ReceiverID = message.Receiver.ID;
            message.Readed = false;
            message.SendDate = DateTime.Now;

            if (message.Sender != null && message.Receiver != null && message.Title != null && message.Title.Trim().Count() > 0 && message.Content != null && message.Content.Trim().Count() > 0)
            {
                db.Characters.Single(c => c.ID == message.SenderID).SendedMessages.Add(message);
                db.Characters.Single(c => c.ID == message.ReceiverID).ReceivedMessages.Add(message);
                db.Messages.Add(message);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
        }

        // GET: Messages/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            Character character = db.Characters.Single(c => c.Login == User.Identity.Name);
            if (message == null && message.ReceiverID != character.ID && message.SenderID != character.ID)
            {
                return HttpNotFound();
            }

            if (message.Sender.Login == User.Identity.Name)
            {
                return RedirectToAction("Details", new { ID = id });
            }

            Character receiver = message.Sender;
            message.Sender = message.Receiver;
            message.Receiver = receiver;

            int receiverID = message.SenderID;
            message.SenderID = message.ReceiverID;
            message.ReceiverID = receiverID;

            message.Title = "RE: " + message.Title;
            message.Content = "";

            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SenderID,ReceiverID,Title,Content,Readed")] Message messageOld)
        {
            if (ModelState.IsValid)
            {
                Message message = new Message
                {
                    SenderID = messageOld.SenderID,
                    Sender = messageOld.Sender,
                    ReceiverID = messageOld.ReceiverID,
                    Receiver = messageOld.Receiver,
                    Title = messageOld.Title,
                    Content = messageOld.Content,
                    Readed = false,
                    SendDate = DateTime.Now
                };

                db.Characters.Single(c => c.ID == message.SenderID).SendedMessages.Add(message);
                db.Characters.Single(c => c.ID == message.ReceiverID).ReceivedMessages.Add(message);
                db.Messages.Add(message);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            messageOld = db.Messages.Find(messageOld.ID);

            return View(messageOld);
        }

        // GET: Messages/Delete/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
