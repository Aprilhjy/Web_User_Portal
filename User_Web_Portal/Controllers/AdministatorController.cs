using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace User_Web_Portal.Controllers
{
    [Authorize]
    public class AdministatorController : Controller
    {
        // GET: Administer
        public ActionResult Index()
        {
            return View();
        }
    }
}