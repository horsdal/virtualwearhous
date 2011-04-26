using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnicefVirtualWarehouse;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    public class ApplicationTest
    {
        [Test]
        public void ErrorsAreLoggedOnAppLevel()
        {
            var app = new FakeApp();
            using (var connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["unicefvirtualwarehouse"].ConnectionString))
            {
                connection.Open();
                var command =
                    new SqlCommand("SELECT COUNT(*)  FROM [UnicefVirtualWarehouse.UnicefContext].[dbo].[Logs]",
                                   connection);
                var logEntriesBefore = command.ExecuteScalar();

                app.Application_Error(this, new EventArgs());

                var logEntriesAfter = command.ExecuteScalar();

                Assert.That(logEntriesAfter, Is.GreaterThan(logEntriesBefore));
            }
        }
    }
}
