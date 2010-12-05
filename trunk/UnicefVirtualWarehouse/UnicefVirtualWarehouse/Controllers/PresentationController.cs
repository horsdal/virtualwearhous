using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;

namespace UnicefVirtualWarehouse.Controllers
{
    public class PresentationController : Controller
    {
        //
        // GET: /Presentation/

        public ActionResult Index()
        {
            var presentations = MvcApplication.CurrentUnicefContext.Presentations.ToList();
            return View(presentations);
        }

        public ActionResult Product(int id)
        {
            var product = MvcApplication.CurrentUnicefContext.Product.Include("Presentations").SingleOrDefault(p => p.Id == id);

            return View("index", product.Presentations.ToList());
        }

        //
        // GET: /Presentation/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Presentation/Create

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        } 

        //
        // POST: /Presentation/Create

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(FormCollection form)
		{
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            var presentation = new Presentation { Name = form["Name"], Products = new List<Product>() };
			var db = MvcApplication.CurrentUnicefContext;

			db.Presentations.Add(presentation);
			db.SaveChanges();

			return RedirectToAction("Index");
		}
        
        //
        // GET: /Presentation/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }

        //
        // POST: /Presentation/Edit/5

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
        // GET: /Presentation/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }

        //
        // POST: /Presentation/Delete/5

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
