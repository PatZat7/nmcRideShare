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
        public ActionResult Index()
        {
            return View();
        }

        //httppost throws routing error
        public ActionResult Login(Register r)
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