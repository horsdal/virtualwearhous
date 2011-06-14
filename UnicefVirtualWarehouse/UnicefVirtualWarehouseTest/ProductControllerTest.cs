using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Rhino.Mocks;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    public class ProductControllerTestLoggedInAsUnicef : ControllerTestBase<ProductController>
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
        public void CanAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.EqualTo("")); //that is the default view for the action
        }

        [Test]
        public void CanAddAndFindAProduct()
        {
            var newProductName = string.Format("New test product #{0}#", DateTime.Now.Ticks);

            var repo = new ProductRepository();
            var catogoryId = new ProductCategoryRepository().GetAll().First().Id; 

            controllerUnderTest.Create(new FormCollection(new NameValueCollection {{"Key.Name", newProductName}, {"Value", catogoryId.ToString()}}));

            var products = repo.GetByName(newProductName);
            Assert.That(products.Count, Is.EqualTo(1));
            Assert.That(products[0].Name, Is.EqualTo(newProductName));
        }

        [Test]
        public void CanAccessDeletePage()
        {
            var repo = new ProductRepository();
            var product = repo.GetAll().First();

            var result = controllerUnderTest.Delete(product.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo(""));
            var productInViewData = result.ViewData.Model as Product;
            Assert.That(productInViewData, Is.Not.Null);
            Assert.That(productInViewData.Id, Is.EqualTo(product.Id));
            Assert.That(productInViewData.Name, Is.EqualTo(product.Name));
        }

        [Test]
        public void CanDeletePresentationWithoutAnyPresentations()
        {
            var newProductName = string.Format("New test product #{0}#", DateTime.Now.Ticks);
            var productCategory = new ProductCategoryRepository().GetAll().First();

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Key.Name", newProductName }, { "Value", productCategory.Id.ToString() } }));
            var repo = new ProductRepository();
            var products = repo.GetByName(newProductName);

            var result = controllerUnderTest.Delete(products.First().Id, new FormCollection()) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));

            var deletedProductCategory = repo.GetById(products.First().Id);
            Assert.That(deletedProductCategory, Is.Null);
        }

        [Test]
        public void DontFailWhenDeletingProductThatDoesNotExist()
        {
            var productRepository = new ProductRepository();
            Assert.That(productRepository.GetById(int.MaxValue), Is.Null);
            controllerUnderTest.Delete(int.MaxValue, new FormCollection());
        }

        [Test]
        public void CannotDeleteProductWithPresentations()
        {
            var presentationRepo = new PresentationRepository();
            var productNotToBeDeleted =
                presentationRepo.GetAll().Where(pres => pres.Products.Count() != 0).First().Products.First();

            Assert.That(productNotToBeDeleted, Is.Not.Null);

            controllerUnderTest.Delete(productNotToBeDeleted.Id, new FormCollection());

            var repo = new ProductRepository();
            var productAfterDelete = repo.GetById(productNotToBeDeleted.Id);
            Assert.That(productAfterDelete, Is.Not.Null);
            Assert.That(productAfterDelete.Id, Is.EqualTo(productNotToBeDeleted.Id));
        }
    }

    [TestFixture]
    public class ProductControllerTestLoggedInAsAdmin : ControllerTestBase<ProductController>
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
        public void CanAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.EqualTo("")); //that is the default view for the action
        }

        [Test]
        public void CanAddAndFindAProduct()
        {
            var newProductName = string.Format("New test product #{0}#", DateTime.Now.Ticks);

            var repo = new ProductRepository();
            var categoryId = new ProductCategoryRepository().GetAll().First().Id; 

            controllerUnderTest.Create(new FormCollection(new NameValueCollection {{"Key.Name", newProductName}, {"Value", categoryId.ToString()}}));

            var products = repo.GetByName(newProductName);
            Assert.That(products.Count, Is.EqualTo(1));
            Assert.That(products[0].Name, Is.EqualTo(newProductName));
        }

        [Test]
        public void CanAccessDeletePage()
        {
            var repo = new ProductRepository();
            var product = repo.GetAll().First();

            var result = controllerUnderTest.Delete(product.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo(""));
            var productInViewData = result.ViewData.Model as Product;
            Assert.That(productInViewData, Is.Not.Null);
            Assert.That(productInViewData.Id, Is.EqualTo(product.Id));
            Assert.That(productInViewData.Name, Is.EqualTo(product.Name));
        }

        [Test]
        public void CanDeletePresentationWithoutAnyPresentations()
        {
            var newProductName = string.Format("New test product #{0}#", DateTime.Now.Ticks);
            var productCategory = new ProductCategoryRepository().GetAll().First();

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Key.Name", newProductName }, {"Value", productCategory.Id.ToString()} }));
            var repo = new ProductRepository();
            var products = repo.GetByName(newProductName);

            var result = controllerUnderTest.Delete(products.First().Id, new FormCollection()) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));

            var deletedPresentation = repo.GetById(products.First().Id);
            Assert.That(deletedPresentation, Is.Null);
        }

        [Test]
        public void DontFailWhenDeletingProductThatDoesNotExist()
        {
            var presentationRepo = new ProductRepository();
            Assert.That(presentationRepo.GetById(int.MaxValue), Is.Null);
            controllerUnderTest.Delete(int.MaxValue, new FormCollection());
        }

        [Test]
        public void CannotDeleteProductWithPresentations()
        {
            var presentationRepo = new PresentationRepository();
            var productNotToBeDeleted = 
                presentationRepo.GetAll().Where(pres => pres.Products.Count() != 0).First().Products.First();

            Assert.That(productNotToBeDeleted, Is.Not.Null);

            controllerUnderTest.Delete(productNotToBeDeleted.Id, new FormCollection());

            var repo = new ProductRepository();
            var productAfterDelete = repo.GetById(productNotToBeDeleted.Id);
            Assert.That(productAfterDelete, Is.Not.Null);
            Assert.That(productAfterDelete.Id, Is.EqualTo(productNotToBeDeleted.Id));
        }
 
        [Test]
        public void CannotCreateProductInNoExistantCategory()
        {
            var newProductName = string.Format("New test product #{0}#", DateTime.Now.Ticks);

            var repo = new ProductRepository();
            var categoryId = int.MaxValue;

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Key.Name", newProductName }, { "Value", categoryId.ToString() } }));
            var productAfterCreation = repo.GetByName(newProductName).FirstOrDefault();
            Assert.That(productAfterCreation, Is.Null);
        }
    }

      [TestFixture]
      class ProductControllerTestAsNotLoggedIn : ControllerTestBase<ProductController>
      {
          [Test]
          public void IndexViewDataIsSortedAlphabetically()
          {
              var result = controllerUnderTest.Index() as ViewResult;
              Assert.That(result, Is.Not.Null);
              var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<Product>;
              Assert.That(manufacturePresentationsFromView, Is.Ordered.By("Name"));
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
      }

      [TestFixture]
      class ProductControllerTestLoggedInAsManufacturer : ControllerTestBase<ProductController>
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
      }
}
