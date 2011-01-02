﻿using System.Linq;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            var contacts = MvcApplication.CurrentUnicefContext.Contacts.ToList();

            return View(contacts);
        }

        //
        // GET: /Contact/Details/5

        public ActionResult Details(int id)
        {
			var manufacturerContact = MvcApplication.CurrentUnicefContext.Contacts.Where(contact => contact.Id == id).ToList();

			return View("Index", manufacturerContact);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated || !(User.IsInRole(UnicefRole.Manufacturer.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString())))
                return RedirectToAction("Index");
            
            return View();
        } 

        //
        // POST: /Contact/Create

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(FormCollection form)
		{
            if (!Request.IsAuthenticated || !(User.IsInRole(UnicefRole.Manufacturer.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString())))
                return RedirectToAction("Index");
            
            var contact = new Contact 
			{
				Address = form["Address"],
				Zip = form["Zip"],
				City = form["City"],
				Phone = form["Phone"],
				Fax = form["Fax"],
				Email = form["Email"],
				Website = form["Website"],
			};

            var user = new UserRepository().GetByName(User.Identity.Name);
            Manufacturer manufacturer;
            if (User.IsInRole(UnicefRole.Administrator.ToString()))
                manufacturer = new ManufacturerRepository().GetById(int.Parse(form["Manufacturer"]));
            else
                manufacturer = user.AssociatedManufaturer;

			var db = MvcApplication.CurrentUnicefContext;
			
			db.Contacts.Add(contact);
            manufacturer.Contact = contact;
			db.SaveChanges();

			return RedirectToAction("Index", contact);
		}
        
        //
        // GET: /Contact/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contact/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
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
