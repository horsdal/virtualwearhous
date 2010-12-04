using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnicefVirtualWarehouse;
using NHibernate;
using UnicefVirtualWarehouse.Models;

namespace UnicefVirtualWarehouseTest
{
	[TestFixture]
	public class SupplierTest
	{
		[Test]
		public void GetSupplierTest()
        {
			ISessionFactory sessionFactory = SessionHelper.GetNHibernateSessionFactory();
			ISession session = sessionFactory.OpenSession();

			session.Get(typeof(Supplier), 1);
        }
	}
}