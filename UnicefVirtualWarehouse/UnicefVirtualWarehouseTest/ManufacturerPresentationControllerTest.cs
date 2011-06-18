using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    class ManufacturerPresentationControllerTest : ControllerTestBase<ManufacturerPresentationController>
    {
        private ManufacturerPresentationRepository manufacturerPresentationRepo;

        protected override bool IsLoggedIn()
        {
            return true;
        }

        [SetUp]
        public void TestSetup()
        {
            manufacturerPresentationRepo = new ManufacturerPresentationRepository();
        }

        [Test]
        public void CanAddAndFindAManufacturerPresentation()
        {
            var repo = manufacturerPresentationRepo;
            var presentationId = 1;
            var price = DateTime.Now.Ticks % int.MaxValue;

            controllerUnderTest.Create(
                new FormCollection(new NameValueCollection
                                       {
                                           {"ManufacturerPresentation.Licensed", "true"},
                                           {"ManufacturerPresentation.CPP", "false"},
                                           {"ManufacturerPresentation.MinUnit", "5"},
                                           {"ManufacturerPresentation.Size", "100"},
                                           {"ManufacturerPresentation.Price", price.ToString() },
                                           {"Presentations", presentationId.ToString()}
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
            var repo = manufacturerPresentationRepo;
            var presentationId = 1;
            var price = DateTime.Now.Ticks % int.MaxValue;

            controllerUnderTest.Create(
                new FormCollection(new NameValueCollection
                                       {
                                           {"ManufacturerPresentation.Licensed", "true"},
                                           {"ManufacturerPresentation.CPP", "false"},
                                           {"ManufacturerPresentation.MinUnit", "5"},
                                           {"ManufacturerPresentation.Size", "100"},
                                           {"ManufacturerPresentation.Price", price.ToString() },
                                           {"Presentations", presentationId.ToString()}
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
            var manufacturerPresentationRepo = this.manufacturerPresentationRepo;
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
            var manufacturerPresentationRepo = this.manufacturerPresentationRepo;
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

        [Test]
        public void CanAccessDeletePageForOwnPresentation()
        {
            var pres = GetOwnPresentationFromDb();
            var res = controllerUnderTest.Delete(pres.ID) as ViewResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.ViewName, Is.EqualTo(""));
        }

        [Test]
        public void CanAccessPageEvenForNonExistantPresentation()
        {
            var res = controllerUnderTest.Delete(int.MaxValue) as ViewResult;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.ViewName, Is.EqualTo(""));
        }

        [Test]
        public void DeletePageContainsTheManufacturerPresentationForOwnPresentation()
        {
            var presFromDb = GetOwnPresentationFromDb();

            var res = controllerUnderTest.Delete(presFromDb.ID) as ViewResult;
            var presFromViewdata = res.ViewData.Model as ManufacturerPresentation;
            Assert.That(presFromViewdata, Is.Not.Null);
            Assert.That(presFromViewdata.ID, Is.EqualTo(presFromDb.ID));
            Assert.That(presFromViewdata.CPP, Is.EqualTo(presFromDb.CPP));
            Assert.That(presFromViewdata.Licensed, Is.EqualTo(presFromDb.Licensed));
            Assert.That(presFromViewdata.Manufacturer.Id, Is.EqualTo(presFromDb.Manufacturer.Id));
            Assert.That(presFromViewdata.Presentation.Id, Is.EqualTo(presFromDb.Presentation.Id));
            Assert.That(presFromViewdata.Price, Is.EqualTo(presFromDb.Price));
            Assert.That(presFromViewdata.Size, Is.EqualTo(presFromDb.Size));
        }

        private ManufacturerPresentation GetOwnPresentationFromDb()
        {
            var manufacturer = new ManufacturerRepository().GetById(2);
            return manufacturerPresentationRepo.GetByManufacturer(manufacturer).First();
        }

        [Test]
        public void DeletePageContainsNoManufacturerPresentationForOthersPresentation()
        {
            ManufacturerPresentation presFromDb = GetOthersPresentationFromDb();

            var res = controllerUnderTest.Delete(presFromDb.ID) as ViewResult;
            var presFromViewdata = res.ViewData.Model as ManufacturerPresentation;
            Assert.That(presFromViewdata, Is.Null);
        }

        private ManufacturerPresentation GetOthersPresentationFromDb()
        {
            var manufacturer = new ManufacturerRepository().GetById(1);
            return manufacturerPresentationRepo.GetByManufacturer(manufacturer).First();
        }

        [Test]
        public void CanDeleteOwnPresentation()
        {
            bool succes = false;
            var presFromDb = GetOwnPresentationFromDb();
            var toReAdd = new ManufacturerPresentation()
                              {
                                  CPP = presFromDb.CPP,
                                  Licensed = presFromDb.Licensed,
                                  Manufacturer = presFromDb.Manufacturer,
                                  ManufacturingSite = presFromDb.ManufacturingSite,
                                  MinUnit = presFromDb.MinUnit,
                                  Presentation = presFromDb.Presentation,
                                  Price = presFromDb.Price,
                                  Size = presFromDb.Size
                              };
            try
            {
                var res = controllerUnderTest.Delete(presFromDb.ID, new FormCollection()) as ViewResult;
                var fromDbAfterDelete = manufacturerPresentationRepo.GetById(presFromDb.ID);
                Assert.That(fromDbAfterDelete, Is.Null);
                succes = true;
            }
            finally
            {
                if (succes)
                    manufacturerPresentationRepo.Add(toReAdd);
            }
        }

        [Test]
        public void CannotDeleteOthersPresentation()
        {
            var presFromDb = GetOthersPresentationFromDb();
            var res = controllerUnderTest.Delete(presFromDb.ID, new FormCollection()) as ViewResult;
            var fromDbAfterDelete = manufacturerPresentationRepo.GetById(presFromDb.ID);
            Assert.That(fromDbAfterDelete, Is.Not.Null);
        }

        [Test]
        public void ManageContainsManufacturersOwnPresentations()
        {
            var novo = new UserRepository().GetByName(FakeNovoUser).AssociatedManufaturer;
            var novoPresentations = new ManufacturerPresentationRepository().GetByManufacturer(novo);

            var view = controllerUnderTest.Manage() as ViewResult;
            var manufacturerPresentationss = view.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            Assert.That(manufacturerPresentationss, Is.Not.Null);
            Assert.That(manufacturerPresentationss.FirstOrDefault(), Is.Not.Null);
            Assert.That(manufacturerPresentationss, Is.EquivalentTo(novoPresentations));
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
        public void CreatePageViewDataContainsManufacturers()
        {
            var result = controllerUnderTest.Create() as ViewResult;
            var viewmodel = result.ViewData.Model as CreateManufacturerPresentationViewModel;
            Assert.That(viewmodel.Manufacturers, Is.Not.Empty);
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
                                           {"ManufacturerPresentation.Licensed", "true"},
                                           {"ManufacturerPresentation.CPP", "false"},
                                           {"ManufacturerPresentation.MinUnit", "5"},
                                           {"ManufacturerPresentation.Size", "100"},
                                           {"ManufacturerPresentation.Price", price.ToString() },
                                           {"Presentations", presentationId.ToString()},
                                           {"Manufacturers", "1"}
                                       }));
            var manufacturerPresentations = repo.GetByPresentationId(presentationId);
            Assert.That(manufacturerPresentations.Count, Is.GreaterThanOrEqualTo(1));
            var createdManufacturerPresentation = manufacturerPresentations.FirstOrDefault(m => m.Price == price);
            Assert.That(createdManufacturerPresentation, Is.Not.Null);
            Assert.That(createdManufacturerPresentation.Presentation.Id, Is.EqualTo(presentationId));
            Assert.That(createdManufacturerPresentation.Manufacturer.Id, Is.EqualTo(1));
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

        private ManufacturerPresentation GetPresentationFromDb(ManufacturerPresentationRepository manufacturerPresentationRepository)
        {
            var manufacturer = new ManufacturerRepository().GetById(1);
            return manufacturerPresentationRepository.GetByManufacturer(manufacturer).First();
        }

        [Test]
        public void DeletePageContainsTheManufacturerPresentationForOwnPresentation()
        {
            var presFromDb = GetPresentationFromDb(new ManufacturerPresentationRepository());

            var res = controllerUnderTest.Delete(presFromDb.ID) as ViewResult;
            var presFromViewdata = res.ViewData.Model as ManufacturerPresentation;
            Assert.That(presFromViewdata, Is.Not.Null);
            Assert.That(presFromViewdata.ID, Is.EqualTo(presFromDb.ID));
            Assert.That(presFromViewdata.CPP, Is.EqualTo(presFromDb.CPP));
            Assert.That(presFromViewdata.Licensed, Is.EqualTo(presFromDb.Licensed));
            Assert.That(presFromViewdata.Manufacturer.Id, Is.EqualTo(presFromDb.Manufacturer.Id));
            Assert.That(presFromViewdata.Presentation.Id, Is.EqualTo(presFromDb.Presentation.Id));
            Assert.That(presFromViewdata.Price, Is.EqualTo(presFromDb.Price));
            Assert.That(presFromViewdata.Size, Is.EqualTo(presFromDb.Size));
        }


        [Test]
        public void CanDeletePresentation()
        {
            bool succes = false;
            var manufacturerPresentationRepo = new ManufacturerPresentationRepository();
            var presFromDb = GetPresentationFromDb(manufacturerPresentationRepo);
            var toReAdd = new ManufacturerPresentation()
            {
                CPP = presFromDb.CPP,
                Licensed = presFromDb.Licensed,
                Manufacturer = presFromDb.Manufacturer,
                ManufacturingSite = presFromDb.ManufacturingSite,
                MinUnit = presFromDb.MinUnit,
                Presentation = presFromDb.Presentation,
                Price = presFromDb.Price,
                Size = presFromDb.Size
            };
            try
            {
                var res = controllerUnderTest.Delete(presFromDb.ID, new FormCollection()) as ViewResult;
                var fromDbAfterDelete = manufacturerPresentationRepo.GetById(presFromDb.ID);
                Assert.That(fromDbAfterDelete, Is.Null);
                succes = true;
            }
            finally
            {
                if (succes)
                    manufacturerPresentationRepo.Add(toReAdd);
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
        public void IndexViewDataIsSortedAlphabetically()
        {
            var result = controllerUnderTest.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ManufacturerPresentation>;
            Assert.That(manufacturePresentationsFromView, IsOrderedByPresentationName());
        }

        private IResolveConstraint IsOrderedByPresentationName()
        {
            return
                Is.Ordered.Using<ManufacturerPresentation>(
                    (lhs, rhs) => lhs.Presentation.Name.CompareTo(rhs.Presentation.Name));
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

        [Test]
        public void CannotAccessDeletePage()
        {
            var result = controllerUnderTest.Delete(2) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));

            var result2 = controllerUnderTest.Delete(2, new FormCollection()) as RedirectToRouteResult;
            Assert.That(result2, Is.Not.Null);
            Assert.That(result2.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void GetsRedirectedToIndexWhenTryingToGoToManagePage()
        {
            var result = controllerUnderTest.Manage() as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
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

        [Test]
        public void CannotAccessDeletePage()
        {
            var result = controllerUnderTest.Delete(2) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));

            var result2 = controllerUnderTest.Delete(2, new FormCollection()) as RedirectToRouteResult;
            Assert.That(result2, Is.Not.Null);
            Assert.That(result2.RouteValues.Values, Contains.Item("Index"));
        }

        [Test]
        public void GetsRedirectedToIndexWhenTryingToGoToManagePage()
        {
            var result = controllerUnderTest.Manage() as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));
        }

    }
}