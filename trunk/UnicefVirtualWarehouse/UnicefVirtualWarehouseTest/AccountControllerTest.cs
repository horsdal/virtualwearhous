using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;

namespace UnicefVirtualWarehouseTest
{
    public class AccountControllerTestNotLoggedIn : ControllerTestBase<AccountController>
    {
        [Test]
        public void CannotAccessRegistrationPage()
        {
            var res = controllerUnderTest.Register() as RedirectToRouteResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.RouteValues.Values, Contains.Item("ProductCategory"));
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotRegisterUser()
        {
            var res = controllerUnderTest.Register(new RegisterModel()) as RedirectToRouteResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.RouteValues.Values, Contains.Item("ProductCategory"));
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
        }
    }

    public class AccountControllerTestLoggedInAsAdmin: ControllerTestBase<AlwaysValidAccountController>
    {
        protected override bool IsLoggedIn()
        {
            return true;
        }

        protected override  string Role()
        {
            return UnicefRole.Administrator.ToString();
        }

        [Test]
        public void CanAccessRegistrationPage()
        {
            var res = controllerUnderTest.Register() as ViewResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.ViewName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void CanRegisterAndDeleteManufacturerUser()
        {
            var accounts = new AccountMembershipService();

            var registerModel = new RegisterModel
                                    {
                                        AssociatedManufacturerId = 1,
                                        UserName = "TestManufacturerUserRegistration5",
                                        Password = "Pa3sword!",
                                        ConfirmPassword = "Pa3sword!",
                                        Email = "test@manufacturer.com",
                                        Role = UnicefRole.Manufacturer
                                    };
            controllerUnderTest.Register(registerModel);

            var validationResult =
                controllerUnderTest.LogOn(
                    new LogOnModel
                        {
                            UserName = registerModel.UserName,
                            Password = registerModel.Password,
                            RememberMe = false
                        }, "") as RedirectToRouteResult;
            Assert.That(validationResult, Is.Not.Null); // if login fails the same page is shown again, so when redirected login suceeded

            Assert.That(accounts.DeleteUser(registerModel.UserName), Is.True);
        }

        [Test]
        public void CanRegisterAndDeleteAdminUser()
        {
            var accounts = new AccountMembershipService();

            var registerModel = new RegisterModel
            {
                UserName = "TestAdminUserRegistration",
                Password = "Pa3sword!",
                ConfirmPassword = "Pa3sword!",
                Email = "test@manufacturer.com",
                Role = UnicefRole.Administrator
            };
            controllerUnderTest.Register(registerModel);

            var validationResult =
                controllerUnderTest.LogOn(
                    new LogOnModel
                    {
                        UserName = registerModel.UserName,
                        Password = registerModel.Password,
                        RememberMe = false
                    }, "") as RedirectToRouteResult;
            Assert.That(validationResult, Is.Not.Null); // if login fails the same page is shown again, so when redirected login suceeded

            Assert.That(accounts.DeleteUser(registerModel.UserName), Is.True);
        }
   
    }

    public class AccountControllerTestLoggedInAsUnicef: ControllerTestBase<AccountController>
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
        public void CannotAccessRegistrationPage()
        {
            var res = controllerUnderTest.Register() as RedirectToRouteResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.RouteValues.Values, Contains.Item("ProductCategory"));
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotRegisterUser()
        {
            var res = controllerUnderTest.Register(new RegisterModel()) as RedirectToRouteResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.RouteValues.Values, Contains.Item("ProductCategory"));
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
        }
    }
 
    public class AccountControllerTestLoggedInAsManufacturer : ControllerTestBase<AccountController>
    {
        protected override bool  IsLoggedIn()
        {
            return true;
        }

        [Test]
        public void CannotAccessRegistrationPage()
        {
            var res = controllerUnderTest.Register() as RedirectToRouteResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.RouteValues.Values, Contains.Item("ProductCategory"));
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotRegisterUser()
        {
            var res = controllerUnderTest.Register(new RegisterModel()) as RedirectToRouteResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.RouteValues.Values, Contains.Item("ProductCategory"));
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
        }
    }

    public class AlwaysValidAccountController : AccountController
    {
        protected override bool IsModelStateValidValid()
        {
            return true;
        }
    }
}
