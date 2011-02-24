using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

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

        [Test]
        public void IndexModelDataIncludesContact()
        {
            var novoContact = new UserRepository().GetByName(FakeNovoUser).AssociatedManufaturer.Contact;
            var view = controllerUnderTest.Index() as ViewResult;

            Assert.That(view.ViewName, Is.EqualTo("ManufacturerHome"));

            var manufacturerFromViewData = view.ViewData.Model as Manufacturer;
            Assert.That(manufacturerFromViewData, Is.Not.Null);
            Assert.That(manufacturerFromViewData.Contact, Is.Not.Null);
            Assert.That(manufacturerFromViewData.Contact.Id, Is.EqualTo(novoContact.Id));
        }

    }

    [TestFixture]
    public class HomeControllerTestLoggedInAsUnicef : ControllerTestBase<HomeController>
    {
        protected override bool IsLoggedIn()
        {
            return true;
        }

        protected override string Role()
        {
            return UnicefRole.Unicef.ToString();
        }

        [Test]
        public void IndexReturnManufacturerHomeViewWithManufaturerAsModelData()
        {
            var view = controllerUnderTest.Index() as ViewResult;
            Assert.That(view.ViewName, Is.EqualTo("UnicefHome"));
        }
    }

    [TestFixture]
    public class HomeControllerTestLoggedInAsAdmin : ControllerTestBase<HomeController>
    {
        protected override bool IsLoggedIn()
        {
            return true;
        }

        protected override string Role()
        {
            return UnicefRole.Administrator.ToString();
        }

        [Test]
        public void IndexReturnManufacturerHomeViewWithManufaturerAsModelData()
        {
            var view = controllerUnderTest.Index() as ViewResult;
            Assert.That(view.ViewName, Is.EqualTo("AdminHome"));
        }
    }

    [TestFixture]
    public class HomeControllerTestNotLoggedIn : ControllerTestBase<HomeController>
    {
        protected override bool IsLoggedIn()
        {
            return false;
        }

        [Test]
        public void IndexRedirectToIndexOfProductCategories()
        {
            var view = controllerUnderTest.Index() as RedirectToRouteResult;
            Assert.That(view.RouteValues.Values, Contains.Item("Index"));
            Assert.That(view.RouteValues.Values, Contains.Item("ProductCategory"));
        }
    }
}
