using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Rhino.Mocks;

namespace UnicefVirtualWarehouseTest
{
    public class ControllerTestBase<ControllerType> where ControllerType : Controller, new()
    {
        private FakeApp app;
        private MockRepository mocks;
        private HttpContextBase mockedhttpContext;
        private HttpRequestBase mockedHttpRequest;
        private HttpResponseBase mockedHttpResponse;
        private HttpCookieCollection cookies = new HttpCookieCollection();
        protected ControllerType controllerUnderTest;
        protected string FakeNovoUser { get { return "FakeNovoUser"; } }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            app = new FakeApp();

            mocks = new MockRepository();
            mockedhttpContext = mocks.DynamicMock<HttpContextBase>();
            mockedHttpRequest = mocks.DynamicMock<HttpRequestBase>();
            mockedHttpResponse = mocks.DynamicMock<HttpResponseBase>();
            var id = new GenericIdentity(FakeNovoUser);
            var theUser = new GenericPrincipal(id, new [] { Role() } );
            SetupResult.For(mockedhttpContext.Request).Return(mockedHttpRequest);
            SetupResult.For(mockedHttpRequest.IsAuthenticated).Return(IsLoggedIn());
            SetupResult.For(mockedhttpContext.User).Return(theUser);
            SetupResult.For(mockedhttpContext.Response).Return(mockedHttpResponse);
            SetupResult.For(mockedHttpResponse.Cookies).Return(cookies);

            mocks.ReplayAll();
        }

        protected virtual bool IsLoggedIn()
        {
            return false;
        }

        protected virtual string Role()
        {
            return "Manufacturer";
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
