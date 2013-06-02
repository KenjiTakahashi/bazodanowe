using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db;

namespace main.Controllers {
    public class UserAdminVModel {
        public IList<User> Users { get; set; }
        public IList<Admin> Admins { get; set; }
    }

    public class OverseerController : Controller {
        private Context db = new Context();

        //
        // GET: /Overseer/

        public ActionResult Index() {
            UserAdminVModel uavm = new UserAdminVModel();
            uavm.Users = db.Users.ToList();
            uavm.Admins = db.Admins.ToList();
            return View(uavm);
        }

        //
        // GET: /Overseer/Details/5

        public ActionResult Details(int id = 0) {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Overseer/Create

        public ActionResult Create() {
            return View();
        }

        //
        // POST: /Overseer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user) {
            if (ModelState.IsValid) {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /Overseer/Edit/5

        public ActionResult Edit(int id = 0) {
            User user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Overseer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user) {
            if (ModelState.IsValid) {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Overseer/Delete/5

        public ActionResult Delete(int id = 0) {
            User user = db.Users.Find(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Overseer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}