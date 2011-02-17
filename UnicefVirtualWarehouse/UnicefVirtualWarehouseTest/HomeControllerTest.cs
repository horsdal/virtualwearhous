using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    public class HomeControllerTestLoggedInAsManufaturer : ControllerTestBase<HomeController>
    {
        protected override bool IsLoggedIn()
        {
            return true;
        }

        [Test]
        public void IndexReturnManufacturerHomeViewWithManufaturerAsModelData()
        {
            var view = controllerUnderTest.Index() as ViewResult;
            
            Assert.That(view.ViewName, Is.EqualTo("ManufacturerHome"));

            var manufacturerFromViewData = view.ViewData.Model as Manufacturer;
            Assert.That(manufacturerFromViewData, Is.Not.Null);
            Assert.That(manufacturerFromViewData.Name, Is.EqualTo("Novonordisk"));
        }

    }
}
