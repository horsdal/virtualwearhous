using System;
using System.Linq;
using NUnit.Framework;
using UnicefVirtualWarehouse;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouseTest
{
    [TestFixture]
    public class UserTest
    {
        private FakeApp app;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            app = new FakeApp();
        }

        [SetUp]
        public void TestSetup()
        {
            app.BeginTest();
        }

        [TearDown]
        public void TestTeardown()
        {
            app.EndTest();
        }


        [Test]
        public void CanCreateSaveRetrieveAndDeleteAUser()
        {
            var manufatureRepo = new ManufacturerRepository();
            var userRepo = new UserRepository();

            var manufaturer = manufatureRepo.GetAll().First();

            var newUser = new User
                              {
                                  UserName = "TestManufaturerUser#" + DateTime.Now.Ticks.ToString(),
                                  Role = (int) UnicefRole.Manufacturer,
                                  AssociatedManufaturer = manufaturer
                              };
            userRepo.Add(newUser);

            var retrievedUser = userRepo.GetByName(newUser.UserName);
            Assert.That(retrievedUser.UserName, Is.EqualTo(newUser.UserName));
            Assert.That(retrievedUser.AssociatedManufaturer.Id, Is.EqualTo(newUser.AssociatedManufaturer.Id));

            Assert.That(userRepo.Delete(retrievedUser), Is.True);

            var retrievedUserAfterDelete = userRepo.GetByName(newUser.UserName);
            Assert.That(retrievedUserAfterDelete, Is.Null);
        }

        [Test]
        public void CannotAddTheSameUserNameTwice()
        {
            var manufatureRepo = new ManufacturerRepository();
            var userRepo = new UserRepository();

            var manufaturer = manufatureRepo.GetAll().First();

            var newUser = new User
            {
                UserName = "TestManufaturerUserWtihDuplicateName",
                Role = (int)UnicefRole.Manufacturer,
                AssociatedManufaturer = manufaturer
            };
            var anotherNewUserWithTheSameUserName = new User
                                                        {
                                                            UserName = newUser.UserName,
                                                            Role = (int) UnicefRole.Unicef
                                                        };
            Assert.That(userRepo.Add(newUser), Is.True);
            Assert.That(userRepo.Add(anotherNewUserWithTheSameUserName), Is.False);

            var userWithTheDuplatedName = from u in MvcApplication.CurrentUnicefContext.Users
                                          where u.UserName == newUser.UserName
                                          select u;
            Assert.That(userWithTheDuplatedName.Count(), Is.EqualTo(1));

            userRepo.Delete(newUser);
        }
    }
}
