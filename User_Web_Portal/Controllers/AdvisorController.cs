﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace User_Web_Portal.Controllers
{
    [Authorize]
    public class AdvisorController : Controller
    {
        // GET: Advisor
        public ActionResult Index()
        {
            return View();
        }
    }
}