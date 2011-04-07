using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using NLog;
using UnicefVirtualWarehouse.Models;

namespace UnicefVirtualWarehouse
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		private static readonly string unicefContext = "UnicefContext";
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [ThreadStatic]
	    protected static UnicefContext currentContext;
		public static UnicefContext CurrentUnicefContext
		{
            get { return currentContext; }
		}

	    public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "ProductCategory", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);

            StartDatabaseContext();
		}

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                var exception = Server.GetLastError();
                logger.Error("{0} : {1}", exception.Message, exception.StackTrace);
            }            
            catch (Exception)
            {
                // failed to record exception
            }
        }

	    protected void StartDatabaseContext()
	    {
            Database.SetInitializer<UnicefContext>(null);
            var ctx = new UnicefContext();
            ctx.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["unicefvirtualwarehouse"].ConnectionString;
	    }

	    protected void Application_BeginRequest(object sender, EventArgs e)
        {
            currentContext = new UnicefContext();
            currentContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["UnicefVirtualWarehouse"].ConnectionString; 
        }

		protected void Application_EndRequest(object sender, EventArgs e)
		{
		    currentContext.Dispose();
		}

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                //Extract the forms authentication cookie
                FormsAuthenticationTicket authTicket =
                       FormsAuthentication.Decrypt(authCookie.Value);
                // Create an Identity object
                //CustomIdentity implements System.Web.Security.IIdentity
                var id = new GenericIdentity(authTicket.Name);
                //CustomPrincipal implements System.Web.Security.IPrincipal
                var theUser = new GenericPrincipal(id, new [] { authTicket.UserData } );
                Context.User = theUser;
            }
        }
	}
}