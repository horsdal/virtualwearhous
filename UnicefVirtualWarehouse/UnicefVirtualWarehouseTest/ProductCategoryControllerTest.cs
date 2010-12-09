using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    public class ProductCategoryControllerTest : ControllerTestBase<ProductCategoryController>
    {
        [Test]
        public void CanAddAndFindAProduct()
        {
            var newProdcutCategoryName = string.Format("New test product category #{0}#", DateTime.Now.Ticks);

            var repo = new ProductCategoryRepository();

            controllerUnderTest.Create(new FormCollection(new NameValueCollection { { "Name", newProdcutCategoryName } }));

            var categories = repo.GetByName(newProdcutCategoryName);
            Assert.That(categories.Count, Is.EqualTo(1));
            Assert.That(categories[0].Name, Is.EqualTo(newProdcutCategoryName));
        }
    }
}
