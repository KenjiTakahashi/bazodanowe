using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db;
using WebMatrix.WebData;

namespace main.Controllers
{
    public class HomeController : ApplicationController
    {
        private Context db = new Context();
        //
        // GET: /Home/

        public ActionResult Index() {
            if(WebSecurity.IsAuthenticated)
                return View("Home", db.Users.Find(WebSecurity.CurrentUserId));
            return View();
        }

    }
}
