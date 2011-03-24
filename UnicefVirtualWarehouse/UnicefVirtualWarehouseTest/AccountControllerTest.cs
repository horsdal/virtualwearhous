using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    public class AccountControllerTestNotLoggedIn : ControllerTestBase<AlwaysValidAccountController>
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

        [Test]
        public void CannotDeleteUsers()
        {
            var res = controllerUnderTest.Delete(1) as RedirectToRouteResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
            Assert.That(res.RouteValues.Values, Contains.Item("ProductCategory"));
        }

        [Test]
        public void CannotAccessManagePage()
        {
            var res = controllerUnderTest.Manage() as RedirectToRouteResult;
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
        public void ManagePageContainsAllUsersFromMainDdInViewData()
        {
            var allUsers = new UserRepository().GetAll();
            var res = controllerUnderTest.Manage() as ViewResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.ViewName, Is.EqualTo(string.Empty));

            var usersFromViewData = res.ViewData.Model as IEnumerable<User>;
            Assert.That(usersFromViewData, Is.EquivalentTo(allUsers));
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

            var userFromMainDB = new UserRepository().GetByName(registerModel.UserName);
            var deleteRes = controllerUnderTest.Delete(userFromMainDB.Id) as ViewResult;
            Assert.That(deleteRes.ViewData.Model, Is.True);
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

        [Test]
        public void CannotCreateExistingUser()
        {
            var accounts = new AccountMembershipService();

            var registerModel = new RegisterModel
                                    {
                                        UserName = "TestAdminUserRegistrationDuplicate",
                                        Password = "Pa3sword!",
                                        ConfirmPassword = "Pa3sword!",
                                        Email = "test@manufacturer.com",
                                        Role = UnicefRole.Administrator
                                    };

            accounts.DeleteUser(registerModel.UserName);

            try
            {
                var firstRes = controllerUnderTest.Register(registerModel) as RedirectToRouteResult;
                Assert.That(firstRes, Is.Not.Null); // a redirect means registration succeeded
                var secondRes = controllerUnderTest.Register(registerModel) as ViewResult;
                Assert.That(secondRes, Is.Not.Null); // return to same page means something is wrong
            }
            finally
            {
                Assert.That(accounts.DeleteUser(registerModel.UserName), Is.True);
            }
        }

        [Test]
        public void CannotCreateUserAlreadyInMainDB()
        {
            var accounts = new AccountMembershipService();
            var userRepo = new UserRepository();

            var registerModel = new RegisterModel
            {
                UserName = "TestUnicefnUserRegistrationDuplicate",
                Password = "Pa3sword!",
                ConfirmPassword = "Pa3sword!",
                Email = "test@manufacturer.com",
                Role = UnicefRole.Unicef
            };

            accounts.DeleteUser(registerModel.UserName);
            userRepo.Add(new User
                             {
                                 UserName = registerModel.UserName,
                                 Role = (int) registerModel.Role
                             });
            try
            {
                var view = controllerUnderTest.Register(registerModel) as ViewResult;
                Assert.That(view, Is.Not.Null); // return to same page means something is wrong
            }
            finally
            {
                userRepo.Delete(registerModel.UserName);
            }
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
        
        [Test]
        public void CannotDeleteUsers()
        {
            var res = controllerUnderTest.Delete(1) as RedirectToRouteResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
            Assert.That(res.RouteValues.Values, Contains.Item("ProductCategory"));
        }

        [Test]
        public void CannotAccessManagePage()
        {
            var res = controllerUnderTest.Manage() as RedirectToRouteResult;
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
    
        [Test]
        public void CannotDeleteUsers()
        {
            var res = controllerUnderTest.Delete(1) as RedirectToRouteResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
            Assert.That(res.RouteValues.Values, Contains.Item("ProductCategory"));
        }

        [Test]
        public void CannotAccessManagePage()
        {
            var res = controllerUnderTest.Manage() as RedirectToRouteResult;
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
