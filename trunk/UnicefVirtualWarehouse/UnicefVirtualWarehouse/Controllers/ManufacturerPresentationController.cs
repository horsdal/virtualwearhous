using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{
    [HandleError]
    public class ManufacturerPresentationController : Controller
    {
        private ManufacturerPresentationRepository manufacturerPresentationRepo = new ManufacturerPresentationRepository();
        private ManufacturerRepository manufacturerRepo = new ManufacturerRepository();

        //
        // GET: /ManufacturerPresentation/

        public ActionResult Index()
        {
            var manufacturerPresentations = manufacturerPresentationRepo.GetAll();
            ScrubPricingInformation(manufacturerPresentations);
            return View(manufacturerPresentations.OrderBy(mp => mp.Presentation.Name));
        }

        private void ScrubPricingInformation(IList<ManufacturerPresentation> manufacturerPresentations)
        {
            if (!Request.IsAuthenticated)
                ScrubAllPricingInformation(manufacturerPresentations);
            if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Manufacturer.ToString()))
                ScrubPricingInformationFromPresentationsFromOtherManufacturers(manufacturerPresentations);
        }

        private void ScrubPricingInformationFromPresentationsFromOtherManufacturers(IList<ManufacturerPresentation> manufacturerPresentations)
        {
            var user = new UserRepository().GetByName(User.Identity.Name);
            var othersManufacturerPresentations =
                manufacturerPresentations.Where(mp => mp.Manufacturer.Id != user.AssociatedManufaturer.Id);
            ScrubAllPricingInformation(othersManufacturerPresentations.ToList());
        }

        private void ScrubAllPricingInformation(IList<ManufacturerPresentation> manufacturerPresentations)
        {
            foreach (var manufacturerPresentation in manufacturerPresentations)
                manufacturerPresentation.Price = 0;
        }

        //
        // GET: /ManufacturerPresentation/Details/5

		public ActionResult Details(int id)
		{
		    IList<ManufacturerPresentation> manPresentations = new ManufacturerPresentationRepository().GetByPresentationId(id);
		    ScrubPricingInformation(manPresentations);
		    return View("Index", manPresentations);
		}

        //
        // GET: /ManufacturerPresentation/Create

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated || !UserThatMayEdit())
                return RedirectToAction("Index");

            var presentations = new PresentationRepository().GetAll();
            var manufacturers = manufacturerRepo.GetAll();
            return View(new CreateManufacturerPresentationViewModel(presentations, manufacturers));
        }

        private bool UserThatMayEdit()
        {
            return (User.IsInRole(UnicefRole.Manufacturer.ToString()) || User.IsInRole(UnicefRole.Administrator.ToString()));
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
                return Create();
            }
        }

        private ManufacturerPresentation CreateAndSaveNewManufacturePresentation(FormCollection collection)
        {
            ManufacturerPresentation manufacturerPresentation = CreateManufacturerPresentation(collection);
            manufacturerPresentationRepo.Add(manufacturerPresentation);
            return manufacturerPresentation;
        }

        private ManufacturerPresentation CreateManufacturerPresentation(FormCollection collection)
        {
            var presentaionRepo = new PresentationRepository();
            var user = new UserRepository().GetByName(User.Identity.Name);
            if (user == null)
                throw new ApplicationException("User not found");

            return new ManufacturerPresentation()
                       {
                           CPP = Convert.ToBoolean(collection["ManufacturerPresentation.CPP"].Split(',')[0]),
                           Licensed = Convert.ToBoolean(collection["ManufacturerPresentation.Licensed"].Split(',')[0]),
                           MinUnit = Convert.ToInt32(collection["ManufacturerPresentation.MinUnit"]),
                           Price = Convert.ToInt32(collection["ManufacturerPresentation.Price"]),
                           Size = Convert.ToInt32(collection["ManufacturerPresentation.Size"]),
                           Presentation =
                               presentaionRepo.GetById(Convert.ToInt32(collection["Presentations"])),
                           Manufacturer = 
                               manufacturerRepo.GetById(Convert.ToInt32(collection["Manufacturers"])) ?? user.AssociatedManufaturer
                       };
        }

        //
        // GET: /ManufacturerPresentation/Delete/5

        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated || !UserThatMayEdit())
                return RedirectToAction("Index");
            return View(GetManufacturerPresentation(id));
        }

        private ManufacturerPresentation GetManufacturerPresentation(int id)
        {
            var manuPres = manufacturerPresentationRepo.GetById(id);
            return CheckThatUserMayEditPresentation(manuPres);
        }

        private ManufacturerPresentation CheckThatUserMayEditPresentation(ManufacturerPresentation manuPres)
        {
            if (User.IsInRole(UnicefRole.Administrator.ToString()) || manuPres == null)
                return manuPres;
            else
                return CheckThatPresentationBelongsToManufacturer(manuPres);
        }

        private ManufacturerPresentation CheckThatPresentationBelongsToManufacturer(ManufacturerPresentation manuPres)
        {
            var manufacturerUser = new UserRepository().GetByName(User.Identity.Name);
            return (manuPres.Manufacturer.Id == manufacturerUser.AssociatedManufaturer.Id) ? manuPres : null;
        }

        //
        // POST: /ManufacturerPresentation/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (!Request.IsAuthenticated || !UserThatMayEdit())
                    return RedirectToAction("Index");

                var mp = GetManufacturerPresentation(id);
                if (mp != null)
                    manufacturerPresentationRepo.Delete(mp);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Manage()
        {
            if (Request.IsAuthenticated && User.IsInRole(UnicefRole.Manufacturer.ToString()))
            {
                var manufacturerAssociatedWithUser = new UserRepository().GetByName(User.Identity.Name).AssociatedManufaturer;
                var presentationsBelongingToManufacturer = manufacturerPresentationRepo.GetByManufacturer(manufacturerAssociatedWithUser);
                return View(presentationsBelongingToManufacturer);
            }
            return RedirectToAction("Index");
        }
    }

    public class CreateManufacturerPresentationViewModel
    {
        public IList<Presentation> Presentations { get; set; }
        public IList<Manufacturer> Manufacturers { get; set; }
        public ManufacturerPresentation ManufacturerPresentation { get; set; }

        public CreateManufacturerPresentationViewModel(IList<Presentation> presentations, IList<Manufacturer> manufacturers)
        {
            Presentations = presentations;
            Manufacturers = manufacturers;
            ManufacturerPresentation = new ManufacturerPresentation();
        }
    }
}
