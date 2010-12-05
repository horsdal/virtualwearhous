using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnicefVirtualWarehouse.Controllers
{
    public class ManufacturerController : Controller
    {
        //
        // GET: /Manufacture/

        public ActionResult Index()
        {
            var manufacturers = MvcApplication.CurrentUnicefContext.Manufacturers.ToList();
            return View(manufacturers);
        }

        //
        // GET: /Manufacture/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Manufacture/Create

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");

            return View();
        } 

        //
        // POST: /Manufacture/Create

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
        // GET: /Manufacture/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            
            return View();
        }

        //
        // POST: /Manufacture/Edit/5

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
        // GET: /Manufacture/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }

        //
        // POST: /Manufacture/Delete/5

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
