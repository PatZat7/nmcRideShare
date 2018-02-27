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
            //Adds submitted user data to database
            if (ModelState.IsValid)
            {
                db.AddNewUser(user.lastName, user.firstName, user.username, user.phoneNumber, user.isDriver , user.nmcEmail, user.password, user.gender);
                db.SaveChanges();

               
            }
            return View("Success");
        }

        //User Login and Registration Page
        [HttpGet]
        public ActionResult Login(User r)
        {
            //int login = 0;
            ViewBag.Message = "Create a new account or log in to an existing one.";
            return View(r);
        }
    }
}