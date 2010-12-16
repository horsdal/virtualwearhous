using System;
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
            return db.Presentations.Where(p => p.Id == id).SingleOrDefault();
        }

        public void Add(Presentation presentation, Product product)
        {
            db.Presentations.Add(presentation);
            product.Presentations.Add(presentation);
            db.SaveChanges();
        }

        public IList<Presentation> GetAll()
        {
            return db.Presentations.ToList();
        }
    }
}