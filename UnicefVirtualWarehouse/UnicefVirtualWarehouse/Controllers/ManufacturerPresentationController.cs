using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;

namespace UnicefVirtualWarehouse.Controllers
{
    public class ManufacturerPresentationController : Controller
    {
        //
        // GET: /ManufacturerPresentation/

        public ActionResult Index()
        {
			return View();
        }

        //
        // GET: /ManufacturerPresentation/Details/5

		public ActionResult Details(int id)
        {
			IList<ManufacturerPresentation> manPresentations =
				MvcApplication.CurrentUnicefContext.ManufacturerPresentations.Include("Presentation").Include("Manufacturer").Where(manPres => manPres.Presentation.Id == id).ToList();

			return View("Index", manPresentations);            
        }

        //
        // GET: /ManufacturerPresentation/Create

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        } 

        //
        // POST: /ManufacturerPresentation/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (!Request.IsAuthenticated)
                    return RedirectToAction("Index");
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /ManufacturerPresentation/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }

        //
        // POST: /ManufacturerPresentation/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (!Request.IsAuthenticated)
                    return RedirectToAction("Index");
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ManufacturerPresentation/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }

        //
        // POST: /ManufacturerPresentation/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (!Request.IsAuthenticated)
                    return RedirectToAction("Index");
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
