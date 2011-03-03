using System;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index", "ProductCategory");

            return ReturnHomeForUserWithRoles();
        }

        private ActionResult ReturnHomeForUserWithRoles()
        {
            if (User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return ReturnManufaturerHome();

            if (User.IsInRole(UnicefRole.Unicef.ToString()))
                return View("UnicefHome");

            if (User.IsInRole(UnicefRole.Administrator.ToString()))
                return View("AdministratorHome");

            return null;
        }

        private ActionResult ReturnManufaturerHome()
        {
            var user = new UserRepository().GetByName(User.Identity.Name);
            return View("ManufacturerHome", user.AssociatedManufaturer);
        }
    }
}
