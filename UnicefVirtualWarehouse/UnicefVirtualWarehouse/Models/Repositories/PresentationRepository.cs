using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnicefVirtualWarehouse.Models.Repositories
{
    public class PresentationRepository
    {
        private UnicefContext db;

        public PresentationRepository()
        {
            db = MvcApplication.CurrentUnicefContext;
        }

        public IList<Presentation> GetByName(string name)
        {
            return db.Presentations.Where(pres => pres.Name == name).ToList();
        }

        public Presentation GetById(int id)
        {
            return db.Presentations.Include("Products").Where(p => p.Id == id).SingleOrDefault();
        }

        public void Add(Presentation presentation, Product product)
        {
            db.Presentations.Add(presentation);
            product.Presentations.Add(presentation);
            db.SaveChanges();
        }

        public IList<Presentation> GetAll()
        {
            return db.Presentations.Include("Products").ToList();
        }

        public void DeleteById(int id)
        {
            var presentation = GetById(id);
            if (HasNoManufacturerPresentations(presentation))
                Delete(presentation);
        }

        public void Delete(Presentation presentation)
        {
            RemovePresentationFromProducts(presentation);
            db.Presentations.Remove(presentation);
            db.SaveChanges();
        }

        private void RemovePresentationFromProducts(Presentation presentation)
        {
            if (presentation.Products != null)
                foreach (var product in presentation.Products)
                    product.Presentations.Remove(presentation);
        }

        private bool HasNoManufacturerPresentations(Presentation presentation)
        {
            var manufacturerPresentationRepo = new ManufacturerPresentationRepository();
            return (manufacturerPresentationRepo.GetByPresentationId(presentation.Id).Count() == 0);
        }

        public IList<Presentation> GetAllByOwner(Manufacturer owner)
        {
            return db.Presentations.Where(p => p.Owner.Id == owner.Id).ToList();
        }
    }
}