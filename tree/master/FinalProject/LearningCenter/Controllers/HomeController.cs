using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.UI;
using Business;
using LearningCenter.Models;

namespace LearningCenter.Controllers
{
    //[Logging]
    //[AuthorizeIPAddress]
    public class HomeController : Controller
    {
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;

        public HomeController(IClassManager classManager,
                              IUserManager userManager)
        {
            this.classManager = classManager;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        //[IsAdministrator]
        public ActionResult Notes()
        {
            return View();
        }

        public PartialViewResult IncrementCount()
        {
            int count = 0;

            // Check if MyCount exists
            if (Session["MyCount"] != null)
            {
                count = (int)Session["MyCount"];
                count++;
            }

            // Create the MyCount session variable
            Session["MyCount"] = count;

            return new PartialViewResult();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        public ActionResult SetCookie()
        {
            // Name the cookie as MyCookie for later retrieval
            var cookie = new HttpCookie("MyCookie")
            {
                // This cookie will expire about one minute, depends on the browser
                Expires = DateTime.Now.AddMinutes(1),

                // This cookie will have a simple string value of myUserName
                // but it can be any kind of object.
                Value = "myUserName"
            };

            // Add the cookie to the response to send it to the browser
            HttpContext.Response.Cookies.Add(cookie);

            return View(cookie);
        }

        public ActionResult GetCookie()
        {
            var cookie = HttpContext.Request.Cookies["MyCookie"];
            return View(cookie);
        }

        public PartialViewResult DisplayLoginName()
        {
            return new PartialViewResult();
        }

        //[HttpGet]
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.Email, registerModel.Password);

                return Redirect("~/");
            }

            return View();
        }


        [HttpGet]
        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult ClassDetail() => View();

        [HttpGet]
        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult ClassList()
        {
            var classes_offered = classManager.Classes
                                              .Select(a => new LearningCenter.Models.ClassModel
                                                (a.Id, a.Name, a.Description, a.Price)).ToArray();
            var model = new ClassViewModel
            {
                Classes = (LearningCenter.Models.ClassModel[]) classes_offered
            };
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult Enroll()
        {
            var classes = classManager.Classes
                                      .Select(a => new LearningCenter.Models.ClassModel
                                       (a.Id, a.Name, a.Description, a.Price)).ToArray();
            var model = new ClassViewModel
            {
                Classes = (LearningCenter.Models.ClassModel[])classes
            };
            return View(model);
        }

        [HttpPost]
        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult Enroll(LearningCenter.Models.ClassModel classModel)
        {
            if (ModelState.IsValid)
            {
                if (Session["User"] != null && Request.Form["Id"] != null)
                {
                    // get current user
                    var user = (LearningCenter.Models.UserModel)Session["User"];

                    // add class to user
                    userManager.AddClass(user.Id, classModel.Id);
                }

                return Redirect("~/Home/Enroll");
            }
            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult LoginToContinue() => View();

       

        [HttpGet]
        [OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult EnrolledClasses(int userId)
        {
            var classes_offered = classManager.UserClassList(userId)
                                              .Select(a => new LearningCenter.Models.ClassModel
                                              (a.Id, a.Name, a.Description, a.Price )).ToArray();
            var model = new ClassViewModel
            {
                Classes = classes_offered
            };
            return View(model);
        }
    }
}