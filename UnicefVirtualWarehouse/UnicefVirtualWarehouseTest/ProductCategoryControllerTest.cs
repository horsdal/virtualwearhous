using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    public class ProductCategoryControllerTestLoggedInAsUnicef : ControllerTestBase<ProductCategoryController>
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
        public void CanAddAndFindAProductCategory()
        {
            var newProdcutCategoryName = string.Format("New test product category #{0}#", DateTime.Now.Ticks);

            var repo = new ProductCategoryRepository();

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Name", newProdcutCategoryName } }));

            var categories = repo.GetByName(newProdcutCategoryName);
            Assert.That(categories.Count, Is.EqualTo(1));
            Assert.That(categories[0].Name, Is.EqualTo(newProdcutCategoryName));
        }

        [Test]
        public void CanAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.EqualTo("")); //that is the default view for the action
        }
    }

    public class ProductCategoryControllerTestLoggedInAsAdmin : ControllerTestBase<ProductCategoryController>
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
        public void CanAddAndFindAProductCategory()
        {
            var newProdcutCategoryName = string.Format("New test product category #{0}#", DateTime.Now.Ticks);

            var repo = new ProductCategoryRepository();

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Name", newProdcutCategoryName } }));

            var categories = repo.GetByName(newProdcutCategoryName);
            Assert.That(categories.Count, Is.EqualTo(1));
            Assert.That(categories[0].Name, Is.EqualTo(newProdcutCategoryName));
        }

        [Test]
        public void CanAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.EqualTo("")); //that is the default view for the action
        }
    }

    [TestFixture]
    class ProductCategoryControllerTestAsNotLoggedIn : ControllerTestBase<ProductCategoryController>
    {
        [Test]
        public void CreateRedirectsToIndex()
        {
            var res = controllerUnderTest.Create() as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));

            res = controllerUnderTest.Create(new FormCollection()) as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
        }
    }

    [TestFixture]
    class ProductCategoryControllerTestLoggedInAsManufacturer : ControllerTestBase<ProductCategoryController>
    {
        protected override bool IsLoggedIn()
        {
            return true;
        }

        protected override string Role()
        {
            return UnicefRole.Manufacturer.ToString();
        }

        [Test]
        public void CreateRedirectsToIndex()
        {
            var res = controllerUnderTest.Create() as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));

            res = controllerUnderTest.Create(new FormCollection()) as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
        }
    }

}
