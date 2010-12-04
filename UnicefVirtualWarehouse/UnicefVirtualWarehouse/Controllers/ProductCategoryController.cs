using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;

namespace UnicefVirtualWarehouse.Controllers
{
    public class ProductCategoryController : Controller
    {
        //
        // GET: /ProductCategory/

        public ActionResult Index()
        {
            IList<ProductCategory> products = MvcApplication.CurrentUnicefContext.ProductCatagories.ToList();
            return View(products);
        }
    }
}
