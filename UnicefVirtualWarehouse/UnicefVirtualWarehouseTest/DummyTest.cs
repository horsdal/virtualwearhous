using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    public class DummyTest
    {
        [Test]
        public void AlwaysSucceed()
        {
            Assert.That(true, Is.True);
        }
    }
}
