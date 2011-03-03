using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    public class ManufacturerControllerTestLoggedInAsAdmin : ControllerTestBase<ManufacturerController>
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
        public void DetailsViewHasManufacturerInViewModel()
        {
            var manufacturer = new ManufacturerRepository().GetAll().First();

            var result =  controllerUnderTest.Details(manufacturer.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturerFromView = result.ViewData.Model as Manufacturer;
            Assert.That(manufacturerFromView, Is.Not.Null);
            Assert.That(manufacturerFromView.Id, Is.EqualTo(manufacturer.Id));
            Assert.That(manufacturerFromView.Name, Is.EqualTo(manufacturer.Name));
            Assert.That(manufacturerFromView.GMP, Is.EqualTo(manufacturer.GMP));
            Assert.That(manufacturerFromView.Contact.Id, Is.EqualTo(manufacturer.Contact.Id));
        }

        [Test]
        public void CanAccessCreatePage()
        {
            var result = controllerUnderTest.Create() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }
    }

    [TestFixture]
    public class ManufacturerControllerTestLoggedInAsUnicef : ControllerTestBase<ManufacturerController>
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
        public void DetailsViewHasManufacturerInViewModel()
        {
            var manufacturer = new ManufacturerRepository().GetAll().First();

            var result = controllerUnderTest.Details(manufacturer.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturerFromView = result.ViewData.Model as Manufacturer;
            Assert.That(manufacturerFromView, Is.Not.Null);
            Assert.That(manufacturerFromView.Id, Is.EqualTo(manufacturer.Id));
            Assert.That(manufacturerFromView.Name, Is.EqualTo(manufacturer.Name));
            Assert.That(manufacturerFromView.GMP, Is.EqualTo(manufacturer.GMP));
            Assert.That(manufacturerFromView.Contact.Id, Is.EqualTo(manufacturer.Contact.Id));
        }

        [Test]
        public void CannotAccessCreatePage()
        {
            var result = controllerUnderTest.Create() as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

    }

    [TestFixture]
    public class ManufacturerControllerTestLoggedInAsManufacturer : ControllerTestBase<ManufacturerController>
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
        public void DetailsViewHasManufacturerInViewModel()
        {
            var manufacturer = new ManufacturerRepository().GetAll().First();

            var result = controllerUnderTest.Details(manufacturer.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturerFromView = result.ViewData.Model as Manufacturer;
            Assert.That(manufacturerFromView, Is.Not.Null);
            Assert.That(manufacturerFromView.Id, Is.EqualTo(manufacturer.Id));
            Assert.That(manufacturerFromView.Name, Is.EqualTo(manufacturer.Name));
            Assert.That(manufacturerFromView.GMP, Is.EqualTo(manufacturer.GMP));
            Assert.That(manufacturerFromView.Contact.Id, Is.EqualTo(manufacturer.Contact.Id));
        }

        [Test]
        public void CannotAccessCreatePage()
        {
            var result = controllerUnderTest.Create() as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CanCreateAndDeleteManufacturer()
        {
            var manufacturerRepo = new ManufacturerRepository();
            var manufacturer = new Manufacturer() {GMP = true, Name = "TestManu " + DateTime.Now.Ticks};
            controllerUnderTest.Create(manufacturer);

            var newManufacturerFromDb = manufacturerRepo.GetByName(manufacturer.Name).FirstOrDefault();
            Assert.That(newManufacturerFromDb, Is.Not.Null);
            Assert.That(newManufacturerFromDb.Name, Is.EqualTo(manufacturer.Name));
            Assert.That(newManufacturerFromDb.GMP, Is.EqualTo(manufacturer.GMP));

            controllerUnderTest.Delete(newManufacturerFromDb.Id, new FormCollection());
            var newManufacturersFromDbAfterDelete = manufacturerRepo.GetByName(manufacturer.Name);
            Assert.That(newManufacturersFromDbAfterDelete, Is.Empty);
        }
    }

    [TestFixture]
    public class ManufacturerControllerTestNotLoggedIn : ControllerTestBase<ManufacturerController>
    {
        protected override bool IsLoggedIn()
        {
            return false;
        }

        [Test]
        public void DetailsViewHasManufacturerInViewModel()
        {
            var manufacturer = new ManufacturerRepository().GetAll().First();

            var result = controllerUnderTest.Details(manufacturer.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturerFromView = result.ViewData.Model as Manufacturer;
            Assert.That(manufacturerFromView, Is.Not.Null);
            Assert.That(manufacturerFromView.Id, Is.EqualTo(manufacturer.Id));
            Assert.That(manufacturerFromView.Name, Is.EqualTo(manufacturer.Name));
            Assert.That(manufacturerFromView.GMP, Is.EqualTo(manufacturer.GMP));
            Assert.That(manufacturerFromView.Contact.Id, Is.EqualTo(manufacturer.Contact.Id));
        }

        [Test]
        public void CannotAccessCreatePage()
        {
            var result = controllerUnderTest.Create() as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

    }

}
