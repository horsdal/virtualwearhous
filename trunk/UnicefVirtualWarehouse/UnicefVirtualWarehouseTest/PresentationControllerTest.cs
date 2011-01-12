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
    }

}
