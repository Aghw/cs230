using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;



namespace LearningCenter
{
    public class AuthorizeIPAddressAttribute : ActionFilterAttribute
    {
        // Called by the ASP.NET MVC framework BEFORE the action method executes.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentRequest = filterContext.HttpContext.Request;
            //ipAddress = HttpContext.Current.Request.UserHostAddress();

            // check if current request contains the ip address
            // that we are looking for
            if (currentRequest.UserHostAddress == "::1")
            {
                // throw a new exception if forbidden
                throw new Exception();

                //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            base.OnActionExecuting(filterContext);
        }

    }
}