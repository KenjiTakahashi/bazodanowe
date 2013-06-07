using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db;
using WebMatrix.WebData;

namespace main.Controllers
{
    public class UsersController : ApplicationController
    {
        private Context db = new Context();

        //
        // GET: /Users/

        public ActionResult Index(int? shelfId = null)
        {
            if(shelfId.HasValue)
                return View(db.Shelves.Find(shelfId).Users);
            else
                return View(db.Users.ToList());
        }

        //
        // GET: /Users/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Users/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [ChildActionOnly]
        public ActionResult Register() {
            if(!WebSecurity.Initialized) {
                WebSecurity.InitializeDatabaseConnection("Context", "Users", "ID", "Nickname", true);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form) {
            WebSecurity.CreateUserAndAccount(form["nickname"], form["password"], new {
                Name=form["name"],
                Surname=form["surname"],
                Email=form["email"]
            });
            Response.Redirect("~/Home/Index");
            return View();
        }

        [ChildActionOnly]
        public ActionResult Login() {
            if(!WebSecurity.Initialized) {
                WebSecurity.InitializeDatabaseConnection("Context", "Users", "ID", "Nickname", true);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form) {
            bool success = WebSecurity.Login(form["username"], form["password"], false);
            if(success) {
                string returnUrl = Request.QueryString["ReturnUrl"];
                if(returnUrl == null) {
                    Response.Redirect("~/Home/Index");
                } else {
                    Response.Redirect(returnUrl);
                }
            }
            return View();
        }

        public ActionResult Logout() {
            WebSecurity.Logout();
            Response.Redirect("~/Home/Index");
            return View();
        }
    }
}