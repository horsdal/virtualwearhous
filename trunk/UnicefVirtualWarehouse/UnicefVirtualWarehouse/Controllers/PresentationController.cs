using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{
    public class PresentationViewModel
    {
        public Presentation Presentation { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
        public int AveragePrice { get; set; }
    }

    public class PresentationController : Controller
    {
        private readonly ProductRepository productRepo = new ProductRepository();
        private readonly PresentationRepository presentationRepo = new PresentationRepository();
        //
        // GET: /Presentation/

        public ActionResult Index()
        {
            var presentations = presentationRepo.GetAll().Select(
                p =>
                    {
                        var manufacturerPresentations =
                            new ManufacturerPresentationRepository().GetByPresentationId(p.Id);
                        return new PresentationViewModel
                            {
                                Presentation = p,
                                MaxPrice = MaxPriceFor(manufacturerPresentations),
                                AveragePrice = AveragePriceFor(manufacturerPresentations),
                                MinPrice = MinPriceFor(manufacturerPresentations)
                            };
                    });
            return View(presentations.ToList());
        }

        private int MinPriceFor(IList<ManufacturerPresentation> manufacturerPresentations)
        {
            return manufacturerPresentations.Count == 0 ?
                0 :
                manufacturerPresentations.Min(p => p.Price);            
        }

        private int AveragePriceFor(IList<ManufacturerPresentation> manufacturerPresentations)
        {
            var averagePrice = manufacturerPresentations.Count == 0
                                   ? 0
                                   : manufacturerPresentations.Average(p => p.Price);
            return (int) Math.Round(averagePrice);
        }

        private int MaxPriceFor(IList<ManufacturerPresentation> manufacturerPresentations)
        {
            return manufacturerPresentations.Count == 0 ?
                0 :
                manufacturerPresentations.Max(p => p.Price);
        }

        public ActionResult Product(int id)
        {
            var product = productRepo.GetById(id);
            var presentations = product.Presentations.Select(
               p =>
               {
                   var manufacturerPresentations =
                       new ManufacturerPresentationRepository().GetByPresentationId(p.Id);
                   return new PresentationViewModel
                   {
                       Presentation = p,
                       MaxPrice = MaxPriceFor(manufacturerPresentations),
                       AveragePrice = AveragePriceFor(manufacturerPresentations),
                       MinPrice = MinPriceFor(manufacturerPresentations)
                   };
               });
            return View("index", presentations.ToList());
        }

 
        //
        // GET: /Presentation/Create

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated || UserMayNotEditPresentations())
                return RedirectToAction("Index");

            var products = productRepo.GetAll();
            return View(new KeyValuePair<Presentation, IEnumerable<Product>>(new Presentation(), products));
        } 

        //
        // POST: /Presentation/Create

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(FormCollection form)
		{
            if (!Request.IsAuthenticated || UserMayNotEditPresentations())
                return RedirectToAction("Index");
            
            CreateAndSaveNewPresentation(form);
		    return RedirectToAction("Index");
		}

        private bool UserMayNotEditPresentations()
        {
            return !(User.IsInRole(UnicefRole.Manufacturer.ToString()) || User.IsInRole((UnicefRole.Administrator.ToString())));
        }

        private void CreateAndSaveNewPresentation(FormCollection form)
        {
            int productId;
            if (int.TryParse(form["Value"], out productId))
            {
                var product = productRepo.GetById(productId);
                var presentation = new Presentation {Name = form["Key.Name"], Products = new List<Product> {product}};

                presentationRepo.Add(presentation, product);
            }
        }

 
        //
        // GET: /Presentation/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated || UserMayNotEditPresentations())
                return RedirectToAction("Index");

            return View(presentationRepo.GetById(id));
        }

        //
        // POST: /Presentation/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return DoDelete(id);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        private ActionResult DoDelete(int id)
        {
            if (!Request.IsAuthenticated || UserMayNotEditPresentations())
                return RedirectToAction("Index");

            presentationRepo.DeleteById(id);
 
            return RedirectToAction("Index");
        }
    }
}
