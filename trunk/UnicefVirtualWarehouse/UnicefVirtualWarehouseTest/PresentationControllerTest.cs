using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    public class PresentationControllerTestLoggedInAsManufaturer : ControllerTestBase<PresentationController>
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
        public void CanAddAndFindAPresentation()
        {
            var newPresentationName = string.Format("New test presentation #{0}#", DateTime.Now.Ticks);

            var repo = new PresentationRepository();
            var productId = FakeApp.CurrentUnicefContext.Product.First().Id;

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Key.Name", newPresentationName }, { "Value", productId.ToString() } }));

            var presentations = repo.GetByName(newPresentationName);
            Assert.That(presentations.Count, Is.EqualTo(1));
            Assert.That(presentations[0].Name, Is.EqualTo(newPresentationName));
            Assert.That(presentations[0].Products.FirstOrDefault(product => product.Id == productId), Is.Not.Null);
        }

        [Test]
        public void CanAccessDeletePage()
        {
            var repo = new PresentationRepository();
            var presentation = repo.GetAll().First();

            var result = controllerUnderTest.Delete(presentation.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo(""));
            var presentationInViewData = result.ViewData.Model as Presentation;
            Assert.That(presentationInViewData, Is.Not.Null);
            Assert.That(presentationInViewData.Id, Is.EqualTo(presentation.Id));
            Assert.That(presentationInViewData.Name, Is.EqualTo(presentation.Name));
        }

        [Test]
        public void CanDeletePresentationWithoutAnyManufacturerPresentations()
        {
            var newPresentationName = string.Format("New test presentation #{0}#", DateTime.Now.Ticks);
            var productId = FakeApp.CurrentUnicefContext.Product.First().Id;

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Key.Name", newPresentationName }, { "Value", productId.ToString() } }));
            var repo = new PresentationRepository();
            var presentations = repo.GetByName(newPresentationName);

            var result = controllerUnderTest.Delete(presentations.First().Id, new FormCollection()) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));

            var deletedPresentation = repo.GetById(presentations.First().Id);
            Assert.That(deletedPresentation, Is.Null);
        }

        [Test]
        public void CannotDeletePresentationWithManufacturerPresentations()
        {
            var manufacturerPresentationRepo = new ManufacturerPresentationRepository();
            var presentationNotToBeDeleted = manufacturerPresentationRepo.GetAll().First().Presentation;

            Assert.That(presentationNotToBeDeleted, Is.Not.Null);

            controllerUnderTest.Delete(presentationNotToBeDeleted.Id, new FormCollection());

            var repo = new PresentationRepository();
            var presentationAfterDelete = repo.GetById(presentationNotToBeDeleted.Id);
            Assert.That(presentationAfterDelete, Is.Not.Null);
            Assert.That(presentationAfterDelete.Id, Is.EqualTo(presentationNotToBeDeleted.Id));
        }

        [Test]
        public void ManageContainsManufacturersOwnPresentations()
        {
            var novo = new UserRepository().GetByName(FakeNovoUser).AssociatedManufaturer;
            var novoPresentations = new PresentationRepository().GetAllByOwner(novo);

            var view = controllerUnderTest.Manage() as ViewResult;
            var presentations = view.ViewData.Model as IEnumerable<Presentation>;
            Assert.That(presentations, Is.Not.Null);
            Assert.That(presentations.FirstOrDefault(), Is.Not.Null);
            Assert.That(presentations, Is.EquivalentTo(novoPresentations));
        }

    }

    public class PresentationControllerTestLoggedInAsAdmin : ControllerTestBase<PresentationController>
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
        public void CanAddAndFindAPresentation()
        {
            var newPresentationName = string.Format("New test presentation #{0}#", DateTime.Now.Ticks);

            var repo = new PresentationRepository();
            var productId = FakeApp.CurrentUnicefContext.Product.First().Id;

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Key.Name", newPresentationName }, { "Value", productId.ToString() } }));

            var presentations = repo.GetByName(newPresentationName);
            Assert.That(presentations.Count, Is.EqualTo(1));
            Assert.That(presentations[0].Name, Is.EqualTo(newPresentationName));
            Assert.That(presentations[0].Products.FirstOrDefault(product => product.Id == productId), Is.Not.Null);
        }


        [Test]
        public void CanAccessDeletePage()
        {
            var repo = new PresentationRepository();
            var presentation = repo.GetAll().First();

            var result = controllerUnderTest.Delete(presentation.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo(""));
            var presentationInViewData = result.ViewData.Model as Presentation;
            Assert.That(presentationInViewData, Is.Not.Null);
            Assert.That(presentationInViewData.Id, Is.EqualTo(presentation.Id));
            Assert.That(presentationInViewData.Name, Is.EqualTo(presentation.Name));
        }

        [Test]
        public void CanDeletePresentationWithoutAnyManufacturerPresentations()
        {
            var newPresentationName = string.Format("New test presentation #{0}#", DateTime.Now.Ticks);
            var productId = FakeApp.CurrentUnicefContext.Product.First().Id;

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Key.Name", newPresentationName }, { "Value", productId.ToString() } }));
            var repo = new PresentationRepository();
            var presentations = repo.GetByName(newPresentationName);

            var result = controllerUnderTest.Delete(presentations.First().Id, new FormCollection()) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));

            var deletedPresentation = repo.GetById(presentations.First().Id);
            Assert.That(deletedPresentation, Is.Null);
        }

        [Test]
        public void DontFailWhenDeletingPresentationThatDoesNotExist()
        {
            var presentationRepo = new PresentationRepository();
            Assert.That(presentationRepo.GetById(int.MaxValue), Is.Null);
            controllerUnderTest.Delete(int.MaxValue, new FormCollection());
        }

        [Test]
        public void CannotDeletePresentationWithManufacturerPresentations()
        {
            var manufacturerPresentationRepo = new ManufacturerPresentationRepository();
            var presentationNotToBeDeleted = manufacturerPresentationRepo.GetAll().First().Presentation;

            Assert.That(presentationNotToBeDeleted, Is.Not.Null);

            controllerUnderTest.Delete(presentationNotToBeDeleted.Id, new FormCollection());

            var repo = new PresentationRepository();
            var presentationAfterDelete = repo.GetById(presentationNotToBeDeleted.Id);
            Assert.That(presentationAfterDelete, Is.Not.Null);
            Assert.That(presentationAfterDelete.Id, Is.EqualTo(presentationNotToBeDeleted.Id));
        }
    }

    [TestFixture]
    class PresentationControllerTestAsNotLoggedIn : ControllerTestBase<PresentationController>
    {
        [Test]
        public void IndexContainsSummaryPrices()
        {
            var res = controllerUnderTest.Index() as ViewResult;
            var presentationsForView = res.ViewData.Model as IEnumerable<PresentationViewModel>;
            Assert.That(presentationsForView, Is.Not.Null);

            CheckPresentationViewModels(presentationsForView);
        }

        [Test]
        public void ProductSubviewContainsSummaryPrices()
        {
            var res = controllerUnderTest.Product(138) as ViewResult;
            var presentationsForView = res.ViewData.Model as IEnumerable<PresentationViewModel>;
            Assert.That(presentationsForView, Is.Not.Null);

            CheckPresentationViewModels(presentationsForView);
        }

        private void CheckPresentationViewModels(IEnumerable<PresentationViewModel> presentationsForView)
        {
            var manufacturerPresentationRepo = new ManufacturerPresentationRepository();
            foreach (var p in presentationsForView)
            {
                var manufacturerPresentationsForP = manufacturerPresentationRepo.GetByPresentationId(p.Presentation.Id);
                if (manufacturerPresentationsForP.Count == 0)
                {
                    Assert.That(p.MaxPrice, Is.EqualTo(0));
                    Assert.That(p.MinPrice, Is.EqualTo(0));
                    Assert.That(p.AveragePrice, Is.EqualTo(0));
                }
                else
                {
                    var maxPriceForPresentation = manufacturerPresentationsForP.Max(mp => mp.Price);
                    var minPriceForPresentation = manufacturerPresentationsForP.Min(mp => mp.Price);
                    var averagePriceForPrensentation = manufacturerPresentationsForP.Average(mp => mp.Price);
                    Assert.That(p.MaxPrice, Is.EqualTo(maxPriceForPresentation));
                    Assert.That(p.MinPrice, Is.EqualTo(minPriceForPresentation));
                    Assert.That(p.AveragePrice, Is.InRange(averagePriceForPrensentation - 1, averagePriceForPrensentation + 1));                   
                }
            }
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
        public void DeleteRedirectsToIndex()
        {
            var res = controllerUnderTest.Delete(1) as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));

            res = controllerUnderTest.Delete(1, new FormCollection()) as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));
            
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
    class PresentationControllerTestLoggedInAsUnicef : ControllerTestBase<PresentationController>
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
        public void DeleteRedirectsToIndex()
        {
            var res = controllerUnderTest.Delete(1) as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));

            res = controllerUnderTest.Delete(1, new FormCollection()) as RedirectToRouteResult;
            Assert.That(res.RouteValues.Values, Contains.Item("Index"));

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
