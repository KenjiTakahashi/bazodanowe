using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db;

namespace main.Controllers
{
    public class ShelvesController : ApplicationController
    {
        private Context db = new Context();

        //
        // GET: /Shelves/

        public ActionResult Index(int? userId = null, int? bookId = null) {
            if(userId.HasValue)
                return View(db.Users.Find(userId).Shelves);
            else if(bookId.HasValue)
                return View(db.Books.Find(bookId).Shelves);
            else
                return View(db.Shelves.ToList());
        }

        public ActionResult Show(int userId, int id) {
            return View(db.Users.Find(userId).Shelves.Select(c => c.ID == id));
        }

        //
        // GET: /Shelves/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Shelves/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Shelf shelf)
        {
            if (ModelState.IsValid)
            {
                db.Shelves.Add(shelf);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shelf);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForUser(int userId, FormCollection form) {
            var user = db.Users.Find(userId);
            var shelf = new db.Shelf { Name = form["New Shelf"], Users = new List<User> { user } };
            db.Shelves.Add(shelf);
            db.SaveChanges();
            Response.Redirect("~");
            return View();
        }

        //
        // GET: /Shelves/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Shelf shelf = db.Shelves.Find(id);
            if (shelf == null)
            {
                return HttpNotFound();
            }
            return View(shelf);
        }

        //
        // POST: /Shelves/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Shelf shelf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shelf).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shelf);
        }

        //
        // GET: /Shelves/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Shelf shelf = db.Shelves.Find(id);
            if (shelf == null)
            {
                return HttpNotFound();
            }
            return View(shelf);
        }

        //
        // POST: /Shelves/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shelf shelf = db.Shelves.Find(id);
            db.Shelves.Remove(shelf);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}