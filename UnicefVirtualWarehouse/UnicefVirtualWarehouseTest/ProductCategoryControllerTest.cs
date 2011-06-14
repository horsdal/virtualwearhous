using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    public class ProductCategoryControllerTestLoggedInAsUnicef : ControllerTestBase<ProductCategoryController>
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
        public void CanAddAndFindAProductCategory()
        {
            var newProdcutCategoryName = string.Format("New test product category #{0}#", DateTime.Now.Ticks);

            var repo = new ProductCategoryRepository();

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Name", newProdcutCategoryName } }));

            var categories = repo.GetByName(newProdcutCategoryName);
            Assert.That(categories.Count, Is.EqualTo(1));
            Assert.That(categories[0].Name, Is.EqualTo(newProdcutCategoryName));
        }

        [Test]
        public void CanAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.EqualTo("")); //that is the default view for the action
        }

        [Test]
        public void CanAccessDeletePage()
        {
            var repo = new ProductCategoryRepository();
            var productCategory = repo.GetAll().First();

            var result = controllerUnderTest.Delete(productCategory.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo(""));
            var productInViewData = result.ViewData.Model as ProductCategory;
            Assert.That(productInViewData, Is.Not.Null);
            Assert.That(productInViewData.Id, Is.EqualTo(productCategory.Id));
            Assert.That(productInViewData.Name, Is.EqualTo(productCategory.Name));
        }

        [Test]
        public void CanDeletePresentationWithoutAnyPresentations()
        {
            var newProductName = string.Format("New test product categrory #{0}#", DateTime.Now.Ticks);

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Name", newProductName } }));
            var repo = new ProductCategoryRepository();
            var productCategories = repo.GetByName(newProductName);

            var result = controllerUnderTest.Delete(productCategories.First().Id, new FormCollection()) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));

            var deletedProductcategroy= repo.GetById(productCategories.First().Id);
            Assert.That(deletedProductcategroy, Is.Null);
        }

        [Test]
        public void DontFailWhenDeletingProductThatDoesNotExist()
        {
            var productCategoryRepo = new ProductCategoryRepository();
            Assert.That(productCategoryRepo.GetById(int.MaxValue), Is.Null);
            controllerUnderTest.Delete(int.MaxValue, new FormCollection());
        }

        [Test]
        public void CannotDeleteProductWithPresentations()
        {
            var productCategoryRepo = new ProductCategoryRepository();
            var productCategoryNotToBeDeleted = productCategoryRepo.GetAll().Where(pc => pc.Products.Count() != 0).First();

            Assert.That(productCategoryNotToBeDeleted, Is.Not.Null);

            controllerUnderTest.Delete(productCategoryNotToBeDeleted.Id, new FormCollection());

            var productCategoryAfterDelete = productCategoryRepo.GetById(productCategoryNotToBeDeleted.Id);
            Assert.That(productCategoryAfterDelete, Is.Not.Null);
            Assert.That(productCategoryAfterDelete.Id, Is.EqualTo(productCategoryNotToBeDeleted.Id));
        }
    }

    public class ProductCategoryControllerTestLoggedInAsAdmin : ControllerTestBase<ProductCategoryController>
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
        public void CanAddAndFindAProductCategory()
        {
            var newProdcutCategoryName = string.Format("New test product category #{0}#", DateTime.Now.Ticks);

            var repo = new ProductCategoryRepository();

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Name", newProdcutCategoryName } }));

            var categories = repo.GetByName(newProdcutCategoryName);
            Assert.That(categories.Count, Is.EqualTo(1));
            Assert.That(categories[0].Name, Is.EqualTo(newProdcutCategoryName));
        }

        [Test]
        public void CanAccessCreatePage()
        {
            var view = controllerUnderTest.Create() as ViewResult;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.ViewName, Is.EqualTo("")); //that is the default view for the action
        }

        [Test]
        public void CanAccessDeletePage()
        {
            var repo = new ProductCategoryRepository();
            var productCategory = repo.GetAll().First();

            var result = controllerUnderTest.Delete(productCategory.Id) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo(""));
            var productInViewData = result.ViewData.Model as ProductCategory;
            Assert.That(productInViewData, Is.Not.Null);
            Assert.That(productInViewData.Id, Is.EqualTo(productCategory.Id));
            Assert.That(productInViewData.Name, Is.EqualTo(productCategory.Name));
        }

        [Test]
        public void CanDeletePresentationWithoutAnyPresentations()
        {
            var newProductName = string.Format("New test product categrory #{0}#", DateTime.Now.Ticks);

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Name", newProductName } }));
            var repo = new ProductCategoryRepository();
            var productCategories = repo.GetByName(newProductName);

            var result = controllerUnderTest.Delete(productCategories.First().Id, new FormCollection()) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues.Values, Contains.Item("Index"));

            var deletedProductcategroy = repo.GetById(productCategories.First().Id);
            Assert.That(deletedProductcategroy, Is.Null);
        }

        [Test]
        public void DontFailWhenDeletingProductThatDoesNotExist()
        {
            var productCategoryRepo = new ProductCategoryRepository();
            Assert.That(productCategoryRepo.GetById(int.MaxValue), Is.Null);
            controllerUnderTest.Delete(int.MaxValue, new FormCollection());
        }

        [Test]
        public void CannotDeleteProductWithPresentations()
        {
            var productCategoryRepo = new ProductCategoryRepository();
            var productCategoryNotToBeDeleted = productCategoryRepo.GetAll().Where(pc => pc.Products.Count() != 0).First();

            Assert.That(productCategoryNotToBeDeleted, Is.Not.Null);

            controllerUnderTest.Delete(productCategoryNotToBeDeleted.Id, new FormCollection());

            var productCategoryAfterDelete = productCategoryRepo.GetById(productCategoryNotToBeDeleted.Id);
            Assert.That(productCategoryAfterDelete, Is.Not.Null);
            Assert.That(productCategoryAfterDelete.Id, Is.EqualTo(productCategoryNotToBeDeleted.Id));
        }
    }

    [TestFixture]
    class ProductCategoryControllerTestAsNotLoggedIn : ControllerTestBase<ProductCategoryController>
    {
        [Test]
        public void IndexViewDataIsSortedAlphabetically()
        {
            var result = controllerUnderTest.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var manufacturePresentationsFromView = result.ViewData.Model as IEnumerable<ProductCategory>;
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
    class ProductCategoryControllerTestLoggedInAsManufacturer : ControllerTestBase<ProductCategoryController>
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
