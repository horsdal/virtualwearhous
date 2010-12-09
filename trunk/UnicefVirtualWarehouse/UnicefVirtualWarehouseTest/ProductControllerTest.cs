﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public class ProductControllerTest : ControllerTestBase<ProductController>
    {
        [Test]
        public void CanAddAndFindAProduct()
        {
            var newProductName = string.Format("New test product #{0}#", DateTime.Now.Ticks);

            var repo = new ProductRepository();
            var catogoryId = 1; 

            controllerUnderTest.Create(new FormCollection(new NameValueCollection {{"Key.Name", newProductName}, {"Value", catogoryId.ToString()}}));

            var products = repo.GetByName(newProductName);
            Assert.That(products.Count, Is.EqualTo(catogoryId));
            Assert.That(products[0].Name, Is.EqualTo(newProductName));
        }
    }
}
