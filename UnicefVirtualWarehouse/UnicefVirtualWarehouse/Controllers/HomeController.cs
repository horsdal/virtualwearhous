using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return ReturnManufaturerHome();
            return View();
        }

        private ActionResult ReturnManufaturerHome()
        {
            var user = new UserRepository().GetByName(User.Identity.Name);
            return View("ManufacturerHome", user.AssociatedManufaturer);
        }
    }
}
