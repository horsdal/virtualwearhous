using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var mpByPresID = db.ManufacturerPresentations.Where(mp => mp.Presentation.Id == presentationId);
            return IncludeDependencies(mpByPresID).ToList();
        }

        private IQueryable<ManufacturerPresentation> IncludeDependencies(IQueryable<ManufacturerPresentation> mpByPresID)
        {
            return mpByPresID.Include("Presentation").Include("Manufacturer");
        }

        public void Add(ManufacturerPresentation manufacturerPresentation)
        {
            db.ManufacturerPresentations.Add(manufacturerPresentation);
            db.SaveChanges();
        }

        public IList<ManufacturerPresentation> GetAll()
        {
            return IncludeDependencies(db.ManufacturerPresentations).ToList();
        }

        public IList<ManufacturerPresentation> GetByManufacturer(Manufacturer manufacturer)
        {
            var mpsByManufacturer = db.ManufacturerPresentations.Where(
                mp =>
                    mp.Manufacturer.Id == manufacturer.Id
                );
            return IncludeDependencies(mpsByManufacturer).ToList();
        }

        public ManufacturerPresentation GetById(int id)
        {
            return IncludeDependencies(db.ManufacturerPresentations).SingleOrDefault(mp => mp.ID == id);
        }

        public void Delete(ManufacturerPresentation mp)
        {
            db.ManufacturerPresentations.Remove(mp);
            db.SaveChanges();
        }
    }
}