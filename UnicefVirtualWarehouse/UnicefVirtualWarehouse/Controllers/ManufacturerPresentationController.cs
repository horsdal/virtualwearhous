using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{
    public class ManufacturerPresentationController : Controller
    {
        private ManufacturerPresentationRepository manufacturerPresentationRepo = new ManufacturerPresentationRepository();

        //
        // GET: /ManufacturerPresentation/

        public ActionResult Index()
        {
            var manufacturerPresentations = manufacturerPresentationRepo.GetAll();

			return View(manufacturerPresentations);
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
            if (!Request.IsAuthenticated || !(User.IsInRole(UnicefRole.Manufacturer.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString())))
                return RedirectToAction("Index");

            var presentations = new PresentationRepository().GetAll();
            return View(new KeyValuePair<ManufacturerPresentation, IEnumerable<Presentation>>(new ManufacturerPresentation(), presentations));
        } 

        //
        // POST: /ManufacturerPresentation/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var name = User.Identity.Name;

                if (!Request.IsAuthenticated || !(User.IsInRole(UnicefRole.Manufacturer.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString())))
                    return RedirectToAction("Index");
                var newManufacturerPresentation = CreateAndSaveNewManufacturePresentation(collection);
                
                return Details(newManufacturerPresentation.Presentation.Id);
            }
            catch
            {
                return View();
            }
        }

        private ManufacturerPresentation CreateAndSaveNewManufacturePresentation(FormCollection collection)
        {
            var presentaionRepo = new PresentationRepository();
            var manufacturerUser = new UserRepository().GetByName(User.Identity.Name);
            if (manufacturerUser == null)
                throw new ApplicationException("User not found");

            var manufacturerPresentation = new ManufacturerPresentation()
                                               {
                                                   CPP = Convert.ToBoolean(collection["Key.CPP"].Split(',')[0]),
                                                   Licensed = Convert.ToBoolean(collection["Key.Licensed"].Split(',')[0]),
                                                   MinUnit = Convert.ToInt32(collection["Key.MinUnit"]),
                                                   Price = Convert.ToInt32(collection["Key.Price"]),
                                                   Size = Convert.ToInt32(collection["Key.Size"]),
                                                   Presentation =
                                                       presentaionRepo.GetById(Convert.ToInt32(collection["Value"])),
                                                    Manufacturer = manufacturerUser.AssociatedManufaturer
                                               };
            manufacturerPresentationRepo.Add(manufacturerPresentation);

            return manufacturerPresentation;
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
