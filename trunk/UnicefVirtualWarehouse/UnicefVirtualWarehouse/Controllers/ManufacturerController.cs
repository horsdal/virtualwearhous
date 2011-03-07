using System.Linq;
using System.Web.Mvc;
using UnicefVirtualWarehouse.Models;
using UnicefVirtualWarehouse.Models.Repositories;

namespace UnicefVirtualWarehouse.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly ManufacturerRepository manufacturerRepo = new ManufacturerRepository();
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
            var manufacturer = manufacturerRepo.GetById(id);
            return View(manufacturer);
        }

        //
        // GET: /Manufacture/Create

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                return RedirectToAction("Index");

            return View();
        } 

        //
        // POST: /Manufacture/Create

        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            try
            {
                return HandleCreateRequest(manufacturer);
            }
            catch
            {
                return View();
            }
        }

        private ActionResult HandleCreateRequest(Manufacturer manufacturer)
        {
            if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                return RedirectToAction("Index");

            CreateNewManufacturer(manufacturer);

            return RedirectToAction("Index");
        }

        private void CreateNewManufacturer(Manufacturer manufacturer)
        {
            var contactRepo = new ContactRepository();
            var contact = new Contact();
            contactRepo.Create(contact);
            manufacturer.Contact = contact;
            manufacturerRepo.Create(manufacturer);
        }

        //
        // GET: /Manufacture/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                return RedirectToAction("Index");

            var manufacturerToEdit = manufacturerRepo.GetById(id);

            return View(manufacturerToEdit);
        }

        
        // POST: /Manufacture/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Manufacturer editedManufacturer)
        {
            try
            {
                if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                    return RedirectToAction("Index");

                manufacturerRepo.Update(editedManufacturer);
 
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
            if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                return RedirectToAction("Index");

            var manufacturerToEdit = manufacturerRepo.GetById(id);

            return View(manufacturerToEdit);
        }

        //
        // POST: /Manufacture/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (!Request.IsAuthenticated || !User.IsInRole(UnicefRole.Administrator.ToString()))
                    return RedirectToAction("Index");

                manufacturerRepo.Delete(id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
