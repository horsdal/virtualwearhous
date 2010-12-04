using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;

namespace UnicefVirtualWarehouse.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Products/

        public ActionResult Index()
        {
            IList<Product> products = MvcApplication.CurrentUnicefContext.Product.ToList();

            return View(products);
        }

        public ActionResult ProductCategory(int id)
        {
            var productCategory = MvcApplication.CurrentUnicefContext.ProductCatagories.Include("Products").SingleOrDefault(p => p.Id == id);

            return View("index", productCategory.Products.ToList());
        }
    }
}
