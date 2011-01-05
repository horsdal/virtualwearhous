using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Rhino.Mocks;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    public class ProductControllerTestLoggedInAsUnicef : ControllerTestBase<ProductController>
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
        public void CanAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.EqualTo("")); //that is the default view for the action
        }

        [Test]
        public void CanAddAndFindAProduct()
        {
            var newProductName = string.Format("New test product #{0}#", DateTime.Now.Ticks);

            var repo = new ProductRepository();
            var catogoryId = 1; 

            controllerUnderTest.Create(new FormCollection(new NameValueCollection {{"Key.Name", newProductName}, {"Value", catogoryId.ToString()}}));

            var products = repo.GetByName(newProductName);
            Assert.That(products.Count, Is.EqualTo(1));
            Assert.That(products[0].Name, Is.EqualTo(newProductName));
        }
    }

      [TestFixture]
    public class ProductControllerTestLoggedInAsAdmin : ControllerTestBase<ProductController>
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
        public void CanAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.EqualTo("")); //that is the default view for the action
        }

        [Test]
        public void CanAddAndFindAProduct()
        {
            var newProductName = string.Format("New test product #{0}#", DateTime.Now.Ticks);

            var repo = new ProductRepository();
            var catogoryId = 1; 

            controllerUnderTest.Create(new FormCollection(new NameValueCollection {{"Key.Name", newProductName}, {"Value", catogoryId.ToString()}}));

            var products = repo.GetByName(newProductName);
            Assert.That(products.Count, Is.EqualTo(1));
            Assert.That(products[0].Name, Is.EqualTo(newProductName));
        }
    }

      [TestFixture]
      class ProductControllerTestAsNotLoggedIn : ControllerTestBase<ProductController>
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
      class ProductControllerTestLoggedInAsManufacturer : ControllerTestBase<ProductController>
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
