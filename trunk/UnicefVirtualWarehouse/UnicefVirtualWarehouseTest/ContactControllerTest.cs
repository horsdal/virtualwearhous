using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models.Repositories;
using UnicefVirtualWarehouse.Models;
using NUnit.Framework;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    public class ContactControllerTestLoggedInAsManufacturer : ControllerTestBase<ContactController>
    {
        protected override bool IsLoggedIn()
        {
            return true;
        }

        [Test]
        public void CanAccesCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.Empty); //Which means the default view for the controller method
        }

        [Test]
        public void CanCreateContactInformationForLoggedInManufacturer()
        {
            var address = "Address";
            var zip = "Zip";
            var city = "City";
            var phone = "Phone";
            var fax = "Fax";
            var email = "Email";
            var website = "Website";

            controllerUnderTest.Create(
                new FormCollection(
                    new NameValueCollection 
                    { 
                        { "Address", address }, 
                        { "Zip", zip },
                        { "City", city },
                        { "Phone", phone },
                        { "Fax", fax },
                        { "Email", email },
                        { "Website", website }
                    }));

            var user = new UserRepository().GetByName(FakeNovoUser);
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Role, Is.EqualTo((int) UnicefRole.Manufacturer));
            Assert.That(user.AssociatedManufaturer, Is.Not.Null);
            var contact = user.AssociatedManufaturer.Contact;
            Assert.That(contact.Address, Is.EqualTo(address));
            Assert.That(contact.Zip, Is.EqualTo(zip));
            Assert.That(contact.City, Is.EqualTo(city));
            Assert.That(contact.Phone, Is.EqualTo(phone));
            Assert.That(contact.Email, Is.EqualTo(email));
            Assert.That(contact.Website, Is.EqualTo(website));
        }
    }

    [TestFixture]
    public class ContactControllerTestLoggedInAsUnicef : ControllerTestBase<ContactController>
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
        public void CannontAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as RedirectToRouteResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotCreateContact()
        {
            var address = "Failing Address";
            var zip = "Failing Zip";
            var city = "Failing City";
            var phone = "Failing Phone";
            var fax = "Failing Fax";
            var email = "Failing Email";
            var website = "Failing Website";

            var view = controllerUnderTest.Create(
                new FormCollection(
                    new NameValueCollection 
                    { 
                        { "Address", address }, 
                        { "Zip", zip },
                        { "City", city },
                        { "Phone", phone },
                        { "Fax", fax },
                        { "Email", email },
                        { "Website", website }
                    })) as RedirectToRouteResult;

            Assert.That(view, Is.Not.Null);
            Assert.That(view.RouteValues.Values, Contains.Item("Index"));

            var user = new UserRepository().GetByName(FakeNovoUser);
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Role, Is.EqualTo((int)UnicefRole.Manufacturer));
            Assert.That(user.AssociatedManufaturer, Is.Not.Null);
            var contact = user.AssociatedManufaturer.Contact;
            if (contact != null)
            {
                Assert.That(contact.Address, Is.Not.EqualTo(address));
                Assert.That(contact.Zip, Is.Not.EqualTo(zip));
                Assert.That(contact.City, Is.Not.EqualTo(city));
                Assert.That(contact.Phone, Is.Not.EqualTo(phone));
                Assert.That(contact.Email, Is.Not.EqualTo(email));
                Assert.That(contact.Website, Is.Not.EqualTo(website));
            }
        }
    }

    [TestFixture]
    public class ContactControllerTestNotLoggedIn : ControllerTestBase<ContactController>
    {
        protected override bool IsLoggedIn()
        {
            return false;
        }

        [Test]
        public void CannontAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as RedirectToRouteResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotCreateContact()
        {
            var address = "Failing Address";
            var zip = "Failing Zip";
            var city = "Failing City";
            var phone = "Failing Phone";
            var fax = "Failing Fax";
            var email = "Failing Email";
            var website = "Failing Website";

            var view = controllerUnderTest.Create(
                new FormCollection(
                    new NameValueCollection 
                    { 
                        { "Address", address }, 
                        { "Zip", zip },
                        { "City", city },
                        { "Phone", phone },
                        { "Fax", fax },
                        { "Email", email },
                        { "Website", website }
                    })) as RedirectToRouteResult;

            Assert.That(view, Is.Not.Null);
            Assert.That(view.RouteValues.Values, Contains.Item("Index"));

            var user = new UserRepository().GetByName(FakeNovoUser);
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Role, Is.EqualTo((int)UnicefRole.Manufacturer));
            Assert.That(user.AssociatedManufaturer, Is.Not.Null);
            var contact = user.AssociatedManufaturer.Contact;
            if (contact != null)
            {
                Assert.That(contact.Address, Is.Not.EqualTo(address));
                Assert.That(contact.Zip, Is.Not.EqualTo(zip));
                Assert.That(contact.City, Is.Not.EqualTo(city));
                Assert.That(contact.Phone, Is.Not.EqualTo(phone));
                Assert.That(contact.Email, Is.Not.EqualTo(email));
                Assert.That(contact.Website, Is.Not.EqualTo(website));
            }
        }
    }

    [TestFixture]
    public class ContactControllerTestLoggedInAsAdmin : ControllerTestBase<ContactController>
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
        public void CanAccesCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.Empty); //Which means the default view for the controller method
        }

        [Test]
        public void CanCreateContactInformationForLoggedInManufacturer()
        {
            var user = new UserRepository().GetByName(FakeNovoUser);
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Role, Is.EqualTo((int)UnicefRole.Manufacturer));
            Assert.That(user.AssociatedManufaturer, Is.Not.Null);

            var address = "Address";
            var zip = "Zip";
            var city = "City";
            var phone = "Phone";
            var fax = "Fax";
            var email = "Email";
            var website = "Website";
            var manufacturerId = user.AssociatedManufaturer.Id;

            controllerUnderTest.Create(
                new FormCollection(
                    new NameValueCollection 
                    { 
                        { "Address", address }, 
                        { "Zip", zip },
                        { "City", city },
                        { "Phone", phone },
                        { "Fax", fax },
                        { "Email", email },
                        { "Website", website },
                        { "Manufacturer", manufacturerId.ToString() }
                    }));

            var contact = user.AssociatedManufaturer.Contact;
            Assert.That(contact.Address, Is.EqualTo(address));
            Assert.That(contact.Zip, Is.EqualTo(zip));
            Assert.That(contact.City, Is.EqualTo(city));
            Assert.That(contact.Phone, Is.EqualTo(phone));
            Assert.That(contact.Email, Is.EqualTo(email));
            Assert.That(contact.Website, Is.EqualTo(website));
        }
    }

}
