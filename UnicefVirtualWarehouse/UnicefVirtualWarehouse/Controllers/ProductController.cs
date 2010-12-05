using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
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

		public ActionResult Manufacturer(int id)
		{
			//IEnumerable<Presentation> manufacturerPresentations = 
			//    MvcApplication.CurrentUnicefContext.ManufacturerPresentations.Include("Presentation").Where(man => man.Manufacturer.Id == id).Select(manPres => manPres.) ToList();

			return View();
		}

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");

            var categories = MvcApplication.CurrentUnicefContext.ProductCatagories.ToList();
            var prod = new Product();
            return View(new KeyValuePair<Product, IEnumerable<ProductCategory>>(prod, categories));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");

            var product = new Product {Name = form["Key.Name"], Presentations = new List<Presentation>()};
            var db = MvcApplication.CurrentUnicefContext;
            db.Product.Add(product);
            var categoryId = int.Parse(form["Value"]);
            var category = db.ProductCatagories.Include("Products").FirstOrDefault(cat => cat.Id == categoryId);
            category.Products.Add(product);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
