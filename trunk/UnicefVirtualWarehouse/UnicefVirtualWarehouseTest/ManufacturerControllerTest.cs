using System;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse;
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

        [Test]
        public void CanAccessDeletePageWithManufacturerInViewData()
        {
            var manufacturer = new ManufacturerRepository().GetAll().FirstOrDefault();
            var result = controllerUnderTest.Delete(manufacturer.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturerFromViewData = result.ViewData.Model as Manufacturer;
            Assert.That(manufacturerFromViewData, Is.Not.Null);
            Assert.That(manufacturerFromViewData.Id, Is.EqualTo(manufacturer.Id));
            Assert.That(manufacturerFromViewData.Name, Is.EqualTo(manufacturer.Name));            
        }

        [Test]
        public void CanCreateAndDeleteManufacturer()
        {
            var manufacturerRepo = new ManufacturerRepository();
            var manufacturer = new Manufacturer() { GMP = true, Name = "TestManu " + DateTime.Now.Ticks };
            controllerUnderTest.Create(manufacturer);

            var newManufacturerFromDb = manufacturerRepo.GetByName(manufacturer.Name).FirstOrDefault();
            Assert.That(newManufacturerFromDb, Is.Not.Null);
            Assert.That(newManufacturerFromDb.Name, Is.EqualTo(manufacturer.Name));
            Assert.That(newManufacturerFromDb.GMP, Is.EqualTo(manufacturer.GMP));

            controllerUnderTest.Delete(newManufacturerFromDb.Id, new FormCollection());
            var newManufacturersFromDbAfterDelete = manufacturerRepo.GetByName(manufacturer.Name);
            Assert.That(newManufacturersFromDbAfterDelete, Is.Empty);
        }

        [Test]
        public void CanAccessEditPageWithManufacturerAsViewData()
        {
            var manufacturer = new ManufacturerRepository().GetAll().FirstOrDefault();
            var result = controllerUnderTest.Edit(manufacturer.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturerFromViewData = result.ViewData.Model as Manufacturer;
            Assert.That(manufacturerFromViewData, Is.Not.Null);
            Assert.That(manufacturerFromViewData.Id, Is.EqualTo(manufacturer.Id));
            Assert.That(manufacturerFromViewData.Name, Is.EqualTo(manufacturer.Name));
        }

        [Test]
        public void CanEditManufacturer()
        {
            var manufacturer = new ManufacturerRepository().GetAll().FirstOrDefault();
            var oldName = manufacturer.Name;
            var newName = manufacturer.Name = oldName + " and then some";
            controllerUnderTest.Edit(manufacturer.Id, manufacturer);

            RenewDbContextAndControllerUnderTest();

            var manufacturerAfterEdit = new ManufacturerRepository().GetById(manufacturer.Id);
            Assert.That(manufacturerAfterEdit.Name, Is.EqualTo(newName));

            manufacturerAfterEdit.Name = oldName;
            controllerUnderTest.Edit(manufacturerAfterEdit.Id, manufacturerAfterEdit);

            app.RenewUnicefContext();
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

        [Test]
        public void CannotCreateManufacturer()
        {
            var manufacturerRepo = new ManufacturerRepository();
            var manufacturer = new Manufacturer() { GMP = true, Name = "TestManu " + DateTime.Now.Ticks };
            controllerUnderTest.Create(manufacturer);

            var newManufacturerFromDb = manufacturerRepo.GetByName(manufacturer.Name).FirstOrDefault();
            Assert.That(newManufacturerFromDb, Is.Null);
        }

        [Test]
        public void CannotAccessDeletePage()
        {
            var manu = new ManufacturerRepository().GetAll().FirstOrDefault();
            var result = controllerUnderTest.Delete(manu.Id) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotDeleteManufacturer()
        {
            var manufacturerRepo = new ManufacturerRepository();
            var manufacturer = manufacturerRepo.GetAll().FirstOrDefault();
            controllerUnderTest.Delete(manufacturer.Id, new FormCollection());

            var newManufacturerFromDb = manufacturerRepo.GetById(manufacturer.Id);
            Assert.That(newManufacturerFromDb, Is.Not.Null);
            Assert.That(newManufacturerFromDb.Name, Is.EqualTo(manufacturer.Name));
        }

        [Test]
        public void CannotAccessEditPage()
        {
            var manu = new ManufacturerRepository().GetAll().FirstOrDefault();
            var result = controllerUnderTest.Edit(manu.Id) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotEditManufacturer()
        {
            var manufacturer = new ManufacturerRepository().GetAll().FirstOrDefault();
            var oldName = manufacturer.Name;
            var newName = manufacturer.Name = oldName + " and then some";
            controllerUnderTest.Edit(manufacturer.Id, manufacturer);

            app.RenewUnicefContext();

            var manufacturerAfterEdit = new ManufacturerRepository().GetById(manufacturer.Id);
            Assert.That(manufacturerAfterEdit.Name, Is.EqualTo(oldName));
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
        public void CannotCreateManufacturer()
        {
            var manufacturerRepo = new ManufacturerRepository();
            var manufacturer = new Manufacturer() { GMP = true, Name = "TestManu " + DateTime.Now.Ticks };
            controllerUnderTest.Create(manufacturer);

            var newManufacturerFromDb = manufacturerRepo.GetByName(manufacturer.Name).FirstOrDefault();
            Assert.That(newManufacturerFromDb, Is.Null);
        }

        [Test]
        public void CannotAccessDeletePage()
        {
            var manu = new ManufacturerRepository().GetAll().FirstOrDefault();
            var result = controllerUnderTest.Delete(manu.Id) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotDeleteManufacturer()
        {
            var manufacturerRepo = new ManufacturerRepository();
            var manufacturer = manufacturerRepo.GetAll().FirstOrDefault();
            controllerUnderTest.Delete(manufacturer.Id, new FormCollection());

            var newManufacturerFromDb = manufacturerRepo.GetById(manufacturer.Id);
            Assert.That(newManufacturerFromDb, Is.Not.Null);
            Assert.That(newManufacturerFromDb.Name, Is.EqualTo(manufacturer.Name));
        }
        [Test]
        public void CannotAccessEditPage()
        {
            var manu = new ManufacturerRepository().GetAll().FirstOrDefault();
            var result = controllerUnderTest.Edit(manu.Id) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotEditManufacturer()
        {
            var manufacturer = new ManufacturerRepository().GetAll().FirstOrDefault();
            var oldName = manufacturer.Name;
            var newName = manufacturer.Name = oldName + " and then some";
            controllerUnderTest.Edit(manufacturer.Id, manufacturer);

            app.RenewUnicefContext();

            var manufacturerAfterEdit = new ManufacturerRepository().GetById(manufacturer.Id);
            Assert.That(manufacturerAfterEdit.Name, Is.EqualTo(oldName));
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

        [Test]
        public void CannotCreateManufacturer()
        {
            var manufacturerRepo = new ManufacturerRepository();
            var manufacturer = new Manufacturer() { GMP = true, Name = "TestManu " + DateTime.Now.Ticks };
            controllerUnderTest.Create(manufacturer);

            var newManufacturerFromDb = manufacturerRepo.GetByName(manufacturer.Name).FirstOrDefault();
            Assert.That(newManufacturerFromDb, Is.Null);
        }

        [Test]
        public void CannotAccessDeletePage()
        {
            var manu = new ManufacturerRepository().GetAll().FirstOrDefault();
            var result = controllerUnderTest.Delete(manu.Id) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotDeleteManufacturer()
        {
            var manufacturerRepo = new ManufacturerRepository();
            var manufacturer = manufacturerRepo.GetAll().FirstOrDefault();
            controllerUnderTest.Delete(manufacturer.Id, new FormCollection());

            var newManufacturerFromDb = manufacturerRepo.GetById(manufacturer.Id);
            Assert.That(newManufacturerFromDb, Is.Not.Null);
            Assert.That(newManufacturerFromDb.Name, Is.EqualTo(manufacturer.Name));
        }

        [Test]
        public void CannotAccessEditPage()
        {
            var manu = new ManufacturerRepository().GetAll().FirstOrDefault();
            var result = controllerUnderTest.Edit(manu.Id) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void CannotEditManufacturer()
        {
            var manufacturer = new ManufacturerRepository().GetAll().FirstOrDefault();
            var oldName = manufacturer.Name;
            var newName = manufacturer.Name = oldName + " and then some";
            controllerUnderTest.Edit(manufacturer.Id, manufacturer);

            app.RenewUnicefContext();

            var manufacturerAfterEdit = new ManufacturerRepository().GetById(manufacturer.Id);
            Assert.That(manufacturerAfterEdit.Name, Is.EqualTo(oldName));
        }
    }
}
