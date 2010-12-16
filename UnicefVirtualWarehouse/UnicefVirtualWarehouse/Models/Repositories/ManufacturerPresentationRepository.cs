using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnicefVirtualWarehouse.Models.Repositories
{
    public class ManufacturerPresentationRepository
    {
        private UnicefContext db;

        public ManufacturerPresentationRepository()
        {
            db = MvcApplication.CurrentUnicefContext;
        }

        public IList<ManufacturerPresentation> GetByPresentationId(int presentationId)
        {
           return db.ManufacturerPresentations.Where(mp => mp.Presentation.Id == presentationId).ToList();
        }

        public void Add(ManufacturerPresentation manufacturerPresentation)
        {
            db.ManufacturerPresentations.Add(manufacturerPresentation);
            db.SaveChanges();
        }

        public IList<ManufacturerPresentation> GetAll()
        {
            return db.ManufacturerPresentations.Include("Presentation").Include("Manufacturer").ToList();
        }
    }
}