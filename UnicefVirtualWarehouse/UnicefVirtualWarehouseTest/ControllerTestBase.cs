using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Rhino.Mocks;
using UnicefVirtualWarehouse.Controllers;

namespace UnicefVirtualWarehouseTest
{
    public class ControllerTestBase<ControllerType> where ControllerType : Controller, new()
    {
        private FakeApp app;
        private MockRepository mocks;
        private HttpContextBase mockedhttpContext;
        private HttpRequestBase mockedHttpRequest;
        protected ControllerType controllerUnderTest;

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

            controllerUnderTest = new ControllerType();
            controllerUnderTest.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), controllerUnderTest);
        }

        [TearDown]
        public void TestTeardown()
        {
            app.EndTest();
        }
    }
}
