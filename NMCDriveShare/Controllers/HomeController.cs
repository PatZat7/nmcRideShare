using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMCDriveShare.Models;

namespace NMCDriveShare.Controllers
{
    public class HomeController : Controller
    {
        DriveShareEntities3 db = new DriveShareEntities3();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                db.AddNewUser(user.lastName, user.phoneNumber, user.nmcEmail, user.password, user.gender);
                db.SaveChanges();

               
            }
            return View("Success");
        }

        //httppost throws routing error
        public ActionResult Login(User r)
        {
            ViewBag.Message = "Create a new account or log in to an existing one.";

            return View(r);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}