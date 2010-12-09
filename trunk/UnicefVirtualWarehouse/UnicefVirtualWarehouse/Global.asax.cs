using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UnicefVirtualWarehouse
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		private static readonly string unicefContext = "UnicefContext";

        [ThreadStatic]
	    private static UnicefContext currentContext;
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
		}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            currentContext = new UnicefContext();
            //uc.Database.Connection.ConnectionString("Data Source=.\SQLEXPRESS;Initial Catalog=UnicefVirtualWarehouse;Integrated Security=SSPI;");    
            currentContext.Database.Connection.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=UnicefVirtualWarehouse.UnicefContext;Integrated Security=SSPI;";
			currentContext.Database.CreateIfNotExists();
		}

		protected void Application_EndRequest(object sender, EventArgs e)
		{
		    currentContext.Dispose();
		}

	}
}