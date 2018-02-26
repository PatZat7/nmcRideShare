using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NMCDriveShare.Controllers
{
    public class PortalController : Controller
    {
        // Users display portal
        public ActionResult Index()
        {
            return View();
        }
    }
}