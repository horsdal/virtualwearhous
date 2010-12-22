using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    class ManufacturerPresentationControllerTest : ControllerTestBase<ManufacturerPresentationController>
    {
        protected override bool IsLoggedIn()
        {
            return true;
        }

        [Test]
        public void CanAddAndFindAManufacturerPresentation()
        {
            var repo = new ManufacturerPresentationRepository();
            var presentationId = 1;
            var price = DateTime.Now.Ticks % int.MaxValue;

            controllerUnderTest.Create(
                new FormCollection(new NameValueCollection
                                       {
                                           {"Key.Licensed", "true"},
                                           {"Key.CPP", "false"},
                                           {"Key.MinUnit", "5"},
                                           {"Key.Size", "100"},
                                           {"Key.Price", price.ToString() },
                                           {"Value", presentationId.ToString()}
                                       }));
            var manufacturerPresentations = repo.GetByPresentationId(presentationId);
            Assert.That(manufacturerPresentations.Count, Is.GreaterThanOrEqualTo(1));
            var createdManufacturerPresentation = manufacturerPresentations.FirstOrDefault(m => m.Price == price);
            Assert.That(createdManufacturerPresentation, Is.Not.Null);
            Assert.That(createdManufacturerPresentation.Presentation.Id, Is.EqualTo(presentationId));
        }
    }

    [TestFixture]
    class ManufacturerPresentationControllerTestLoggedInAsAdmin : ControllerTestBase<ManufacturerPresentationController>
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
        public void CanAddAndFindAManufacturerPresentation()
        {
            var repo = new ManufacturerPresentationRepository();
            var presentationId = 1;
            var price = DateTime.Now.Ticks % int.MaxValue;

            controllerUnderTest.Create(
                new FormCollection(new NameValueCollection
                                       {
                                           {"Key.Licensed", "true"},
                                           {"Key.CPP", "false"},
                                           {"Key.MinUnit", "5"},
                                           {"Key.Size", "100"},
                                           {"Key.Price", price.ToString() },
                                           {"Value", presentationId.ToString()}
                                       }));
            var manufacturerPresentations = repo.GetByPresentationId(presentationId);
            Assert.That(manufacturerPresentations.Count, Is.GreaterThanOrEqualTo(1));
            var createdManufacturerPresentation = manufacturerPresentations.FirstOrDefault(m => m.Price == price);
            Assert.That(createdManufacturerPresentation, Is.Not.Null);
            Assert.That(createdManufacturerPresentation.Presentation.Id, Is.EqualTo(presentationId));
        }
    }

    [TestFixture]
    class ManufacturerPresentationControllerTestAsNotLoggedIn : ControllerTestBase<ManufacturerPresentationController>
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
    class ManufacturerPresentationControllerTestAsUnicefUser : ControllerTestBase<ManufacturerPresentationController>
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
        public void CreateRedirectsToIndex()
        {
            var res = controllerUnderTest.Create() as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));

            res = controllerUnderTest.Create(new FormCollection()) as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
        }
    }
}