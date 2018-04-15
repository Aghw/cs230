using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BirthdayCard.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult BirthdayCard()
        {
            return View();
        }


        [HttpPost]
        public ActionResult BirthdayCard(Models.BirthdayCard birthday)
        {
            //return View();
            if (ModelState.IsValid)
            {
                return View("Happy", birthday);
            }
            else
            {
                return View();
            }
        }

    }
}