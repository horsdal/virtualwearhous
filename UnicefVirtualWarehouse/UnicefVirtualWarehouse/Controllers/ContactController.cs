using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;

namespace UnicefVirtualWarehouse.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View();
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
            return View();
        } 

        //
        // POST: /Contact/Create

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(FormCollection form)
		{
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

			var db = MvcApplication.CurrentUnicefContext;
			
			db.Contacts.Add(contact);
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
