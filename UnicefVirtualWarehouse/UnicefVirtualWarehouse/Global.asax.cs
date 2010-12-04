using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;

namespace UnicefVirtualWarehouse
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private ISessionFactory nhibernateSessionFactory;

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            nhibernateSessionFactory = SessionHelper.GetNHibernateSessionFactory();

            BeginRequest += BeginRequestHandler;
            EndRequest += EndRequestHandler;
        }

        private void EndRequestHandler(object sender, EventArgs e)
        {
			nhibernateSessionFactory.GetCurrentSession().Close();
        }

        private void BeginRequestHandler(object sender, EventArgs e)
        {
			nhibernateSessionFactory.OpenSession();
        }        
    }
}