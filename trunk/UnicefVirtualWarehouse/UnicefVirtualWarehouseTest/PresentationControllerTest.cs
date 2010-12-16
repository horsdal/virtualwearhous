using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using UnicefVirtualWarehouse.Controllers;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    public class PresentationControllerTest : ControllerTestBase<PresentationController>
    {
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
}
