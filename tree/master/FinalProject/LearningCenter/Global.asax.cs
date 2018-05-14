using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using System.Net;
using System.Net.Mail;

namespace LearningCenter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterAutofac();
        }

        //
        // This method gets called to register autofac

        private void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            RegisterAssemblyTypes(builder, Assembly.GetExecutingAssembly());

            var container = builder.Build();

            // Configure dependency resolver.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private void RegisterAssemblyTypes(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly).AsSelf().AsImplementedInterfaces();

            var assemblyNames = assembly.GetReferencedAssemblies();

            foreach (var assemblyName in assemblyNames)
            {
                if (assemblyName.FullName.ToLower().Contains("business") ||
                    assemblyName.FullName.ToLower().Contains("repository") ||
                    assemblyName.FullName.ToLower().Contains("learningcenter") ||
                    assemblyName.FullName.ToLower().Contains("enrollmentdatabase"))
                {
                    assembly = Assembly.Load(assemblyName);
                    RegisterAssemblyTypes(builder, assembly);
                }
            }
        }


        // this procedure should give a nice error page when there is an error,
        // but also, it should send email about the error to the dev-team.
        protected void xxxApplication_Error()
        {
            var exception = Server.GetLastError();

            Server.ClearError();

            var routeData = new RouteData();
            //routeData.Values.Add("controller", "Home");
            routeData.Values.Add("controller", "Error");

            routeData.Values.Add("action", "Error");

            //Email any error to the dev-team
            //var mailMessage = new MailMessage { From = new MailAddress("sam.asmerom@outlook.com") };

            //mailMessage.To.Add(new MailAddress("sam.asmerom@outlook.com"));

            //mailMessage.Subject = "ASP.NET MVC app Learning Center Error";
            //mailMessage.IsBodyHtml = true;
            //mailMessage.Body = "<p>There has been an error in this app.</p>";

            //var client = new SmtpClient();
            //// Bad Practice because this can expose username and password to public
            ////client.Host = "smtp.mailserver.com";
            ////client.Credentials = new NetworkCredential("userName", "password");
            //client.Send(mailMessage);

            //IController errorController = new Controllers.HomeController();
            IController errorController = new Controllers.ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}
