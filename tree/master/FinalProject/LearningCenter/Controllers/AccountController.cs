using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearningCenter.Models;

namespace LearningCenter.Controllers
{
    //public class AccountController : Controller
    //{
    //    // GET: Account
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }
    //}

    ////public class AccountController : Controller
    ////{
    ////    // Inject IUserRepository into the constructor of this controller
    ////    private IUserRepository userRepository;

    ////    public AccountController(IUserRepository userRepository)
    ////    {
    ////        this.userRepository = userRepository;
    ////    }

    ////    // Logout
    ////    public ActionResult LogOut()
    ////    {
    ////        Session["User"] = null;
    ////        System.Web.Security.FormsAuthentication.SignOut();
    ////        return Redirect("~/");
    ////    }

    ////    // Logon
    ////    public ActionResult LogOn()
    ////    {
    ////        return View();
    ////    }

    ////    // HttpPost Logon
    ////    [HttpPost]
    ////    public ActionResult LogOn(LogOnModel model, string returnUrl)
    ////    {
    ////        if (ModelState.IsValid)
    ////        {
    ////            //System.Web.Security.FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
    ////            //return Redirect(returnUrl);

    ////            var user = userRepository.LogIn(model.UserName, model.Password);

    ////            if (user != null)
    ////            {
    ////                Session["User"] = user;
    ////                System.Web.Security.FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
    ////                return Redirect(returnUrl);
    ////            }

    ////            ModelState.AddModelError("", "The user name or password provided is incorrect.");
    ////        }

    ////        return View(model);
    ////    }
    ////}




}