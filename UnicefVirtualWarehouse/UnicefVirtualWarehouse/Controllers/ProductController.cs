using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository productRepo = new ProductRepository();
        //
        // GET: /Products/

        public ActionResult Index()
        {
            return View(productRepo.GetAll());
        }

        public ActionResult ProductCategory(int id)
        {
            var productCategory = MvcApplication.CurrentUnicefContext.ProductCatagories.Include("Products").SingleOrDefault(p => p.Id == id);

            return View("index", productCategory.Products.ToList());
        }

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated || User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return RedirectToAction("Index");

            var categories = MvcApplication.CurrentUnicefContext.ProductCatagories.ToList();
            var prod = new Product();
            return View(new KeyValuePair<Product, IEnumerable<ProductCategory>>(prod, categories));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            if (!Request.IsAuthenticated || User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return RedirectToAction("Index");

            CreateNewProduct(form);

            return RedirectToAction("Index");
        }

        private void CreateNewProduct(FormCollection form)
        {
            var product = new Product {Name = form["Key.Name"], Presentations = new List<Presentation>()};
           
            int categoryId;
            if (int.TryParse(form["Value"], out categoryId))
                productRepo.AddProduct(categoryId, product);
        }

        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated || User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return RedirectToAction("Index");

            var productToDelete = productRepo.GetById(id);

            return View(productToDelete);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            if (!Request.IsAuthenticated || User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return RedirectToAction("Index");

            productRepo.DeleteById(id);

            return RedirectToAction("Index");
        }
    }
}
