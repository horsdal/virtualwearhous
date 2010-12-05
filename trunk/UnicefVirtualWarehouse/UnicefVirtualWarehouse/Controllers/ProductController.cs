using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using System.Data.Entity;

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
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            var product = new Product { Name = form["Name"], Presentations = new List<Presentation>() };
            var db = MvcApplication.CurrentUnicefContext;
            db.Product.Add(product);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
