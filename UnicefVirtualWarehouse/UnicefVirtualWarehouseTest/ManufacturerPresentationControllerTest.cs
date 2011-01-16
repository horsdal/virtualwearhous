using System;
using System.Collections.Generic;
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

        [Test]
        public void WhenCreatingAManufacturerPresentationItBelongsToTheLoggedInManufacturer()
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
            Assert.That(createdManufacturerPresentation.Manufacturer.Id, Is.EqualTo(2)); //Which is Novo
        }

        [Test]
        public void IndexViewDataContainsOnlyPricingInformationForThisManufacturer()
        {
            var manufacturer = new ManufacturerRepository().GetById(2);
            var manufacturerPresentationRepo = new ManufacturerPresentationRepository();
            var allManufacturerPresentations = manufacturerPresentationRepo.GetAll();
            var myManufaturerPresentations = manufacturerPresentationRepo.GetByManufacturer(manufacturer);

            var result = controllerUnderTest.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            Assert.That(manufacturePresentationsFromView.Count(), Is.EqualTo(allManufacturerPresentations.Count));
            foreach (var mpFromView in manufacturePresentationsFromView)
            {
                if (mpFromView.Manufacturer.Id == manufacturer.Id)
                {
                    var priceFromDB = myManufaturerPresentations.SingleOrDefault(p => p.ID == mpFromView.ID).Price;
                    Assert.That(mpFromView.Price, Is.EqualTo(priceFromDB));
                }
                else
                {
                    Assert.That(mpFromView.Price, Is.EqualTo(0));
                }
            }            
        }

        [Test]
        public void DetailsViewDataContainsOnlyPricingInformationForThisManufacturer()
        {
            var manufacturer = new ManufacturerRepository().GetById(2);
            var manufacturerPresentationRepo = new ManufacturerPresentationRepository();
            var allManufacturerPresentations = manufacturerPresentationRepo.GetAll();
            var myManufaturerPresentations = manufacturerPresentationRepo.GetByManufacturer(manufacturer);

            var result = controllerUnderTest.Details(1) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            foreach (var mpFromView in manufacturePresentationsFromView)
            {
                if (mpFromView.Manufacturer.Id == manufacturer.Id)
                {
                    var priceFromDB = myManufaturerPresentations.SingleOrDefault(p => p.ID == mpFromView.ID).Price;
                    Assert.That(mpFromView.Price, Is.EqualTo(priceFromDB));
                }
                else
                {
                    Assert.That(mpFromView.Price, Is.EqualTo(0));
                }
            }
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

        [Test]
        public void IndexViewDataContainsPricingInformation()
        {
            var allManufacturerPresentations = new ManufacturerPresentationRepository().GetAll();

            var result = controllerUnderTest.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            Assert.That(manufacturePresentationsFromView.Count(), Is.EqualTo(allManufacturerPresentations.Count));
            foreach (var mpFromView in manufacturePresentationsFromView)
            {
                var priceFromDB = allManufacturerPresentations.SingleOrDefault(p => p.ID == mpFromView.ID).Price;
                Assert.That(mpFromView.Price, Is.EqualTo(priceFromDB));
            }
        }

        [Test]
        public void DetailsViewDataContainsPricingInformation()
        {
            var allManufacturerPresentations = new ManufacturerPresentationRepository().GetAll();

            var result = controllerUnderTest.Details(1) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            foreach (var mpFromView in manufacturePresentationsFromView)
            {
                var priceFromDB = allManufacturerPresentations.SingleOrDefault(p => p.ID == mpFromView.ID).Price;
                Assert.That(mpFromView.Price, Is.EqualTo(priceFromDB));
            }
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

        [Test]
        public void IndexViewDataContainsNoPricingInformation()
        {
            var result = controllerUnderTest.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            foreach (var mpFromView in manufacturePresentationsFromView)
                Assert.That(mpFromView.Price, Is.EqualTo(0));
        }

        [Test]
        public void DetailsViewDataContainsNoPricingInformation()
        {
            var result = controllerUnderTest.Details(1) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            foreach (var mpFromView in manufacturePresentationsFromView)
                Assert.That(mpFromView.Price, Is.EqualTo(0));
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

        [Test]
        public void IndexViewDataContainsPricingInformation()
        {
            var allManufacturerPresentations = new ManufacturerPresentationRepository().GetAll();

            var result = controllerUnderTest.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            Assert.That(manufacturePresentationsFromView.Count(), Is.EqualTo(allManufacturerPresentations.Count));
            foreach (var mpFromView in manufacturePresentationsFromView)
            {
                var priceFromDB = allManufacturerPresentations.SingleOrDefault(p => p.ID == mpFromView.ID).Price;
                Assert.That(mpFromView.Price, Is.EqualTo(priceFromDB));
            }
        }

        [Test]
        public void DeatilsViewDataContainsPricingInformation()
        {
            var allManufacturerPresentations = new ManufacturerPresentationRepository().GetAll();

            var result = controllerUnderTest.Details(1) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            foreach (var mpFromView in manufacturePresentationsFromView)
            {
                var priceFromDB = allManufacturerPresentations.SingleOrDefault(p => p.ID == mpFromView.ID).Price;
                Assert.That(mpFromView.Price, Is.EqualTo(priceFromDB));
            }
        }
    }
}