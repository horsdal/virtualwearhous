using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{
    public class PresentationController : Controller
    {
        private readonly ProductRepository productRepo = new ProductRepository();
        private readonly PresentationRepository presentationRepo = new PresentationRepository();
        //
        // GET: /Presentation/

        public ActionResult Index()
        {
            var presentations = presentationRepo.GetAll();
            return View(presentations);
        }

        public ActionResult Product(int id)
        {
            var product = productRepo.GetById(id);

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

            var products = productRepo.GetAll();
            return View(new KeyValuePair<Presentation, IEnumerable<Product>>(new Presentation(), products));
        } 

        //
        // POST: /Presentation/Create

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(FormCollection form)
		{
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index");
            
            CreateAndSaveNewPresentation(form);
		    return RedirectToAction("Index");
		}

        private void CreateAndSaveNewPresentation(FormCollection form)
        {
            int productId;
            if (int.TryParse(form["Value"], out productId))
            {
                var product = productRepo.GetById(productId);
                var presentation = new Presentation {Name = form["Key.Name"], Products = new List<Product> {product}};

                var db = MvcApplication.CurrentUnicefContext;

                presentationRepo.Add(presentation, product);
            }
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
