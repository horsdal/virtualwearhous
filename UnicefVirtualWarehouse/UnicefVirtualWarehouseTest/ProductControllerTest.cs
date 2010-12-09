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
    public class ProductControllerTest
    {
        private FakeApp app;
        private MockRepository mocks;
        private HttpContextBase mockedhttpContext;
        private HttpRequestBase mockedHttpRequest;
        private ProductController controllerUnderTest;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            app = new FakeApp();

            mocks = new MockRepository();
            mockedhttpContext = mocks.DynamicMock<HttpContextBase>();
            mockedHttpRequest = mocks.DynamicMock<HttpRequestBase>();
            SetupResult.For(mockedhttpContext.Request).Return(mockedHttpRequest);
            SetupResult.For(mockedHttpRequest.IsAuthenticated).Return(true); // acts as if locked in

            mocks.ReplayAll();
        }

        [SetUp]
        public void TestSetup()
        {
            app.BeginTest();

            controllerUnderTest = new ProductController();
            controllerUnderTest.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), controllerUnderTest);
        }

        [TearDown]
        public void TestTeardown()
        {
            app.EndTest();
        }

        [Test]
        public void CanAddAndFindAProduct()
        {
            var newProductName = string.Format("New test product #{0}#", DateTime.Now.Ticks);

            var repo = new ProductRepository();
            var catogoryId = 1; 

            controllerUnderTest.Create(new FormCollection(new NameValueCollection {{"Key.Name", newProductName}, {"Value", catogoryId.ToString()}}));

            var products = repo.GetByName(newProductName);
            Assert.That(products.Count, Is.EqualTo(catogoryId));
            Assert.That(products[0].Name, Is.EqualTo(newProductName));
        }
    }
}
