using System;
using System.Collections.Generic;
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
            var logFile = File.OpenRead("App_Data/log.txt");
            var sizeOfLogBefore = logFile.Length;
            logFile.Close();

            FakeApp app = new FakeApp();
            app.Application_Error(this, new EventArgs());

            var sizeOfLogAfter = File.OpenRead("App_Data/log.txt").Length;
            Assert.That(sizeOfLogAfter, Is.GreaterThan(sizeOfLogBefore));
        }
    }
}
