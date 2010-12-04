using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using System.Configuration;
using System.Reflection;

namespace UnicefVirtualWarehouse
{
	public class SessionHelper
	{
		public static ISessionFactory GetNHibernateSessionFactory()
		{
			NHibernate.Cfg.Configuration configuration = new NHibernate.Cfg.Configuration();
			configuration.Configure();
			configuration.Properties.Add("proxyfactory.factory_class", "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");
			
			configuration.AddResource("Models/Mappings/Supplier.hbm.xml", Assembly.GetAssembly(typeof(Models.Supplier))); 

			ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings["UnicefVirtualWarehouse"];
			
			if (connectionSettings == null || String.IsNullOrEmpty(connectionSettings.ConnectionString))
				throw new ApplicationException("The database connection string was not set in the configuration file.", new Exception());
			
			configuration.Properties["connection.connection_string"] = connectionSettings.ConnectionString; ;

			return configuration.BuildSessionFactory();
		}
	}
}