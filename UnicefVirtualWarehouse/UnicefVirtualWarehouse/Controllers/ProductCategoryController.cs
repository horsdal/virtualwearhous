﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{

    [HandleError]
    public class ProductCategoryController : Controller
    {
        private ProductCategoryRepository productCategoryRepo = new ProductCategoryRepository();
        //
        // GET: /ProductCategory/

        public ActionResult Index()
        {
            IList<ProductCategory> products = MvcApplication.CurrentUnicefContext.ProductCatagories.ToList();
            return View(products.OrderBy(p => p.Name));
        }

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated || User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return RedirectToAction("Index");

            return View();
        }

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(FormCollection form)
		{
            if (!Request.IsAuthenticated || User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return RedirectToAction("Index");
            
            var productCategory = new ProductCategory { Name = form["Name"] };
		    productCategoryRepo.AddProductCategory(productCategory);

			return RedirectToAction("Index");
		}

        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated || User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return RedirectToAction("Index");

            return View(productCategoryRepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            if (!Request.IsAuthenticated || User.IsInRole(UnicefRole.Manufacturer.ToString()))
                return RedirectToAction("Index");

            productCategoryRepo.DeleteById(id);

            return RedirectToAction("Index");
        }

    }
}
