using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace UnicefVirtualWarehouse.Models.Repositories
{
    public class ManufacturerRepository
    {
        private UnicefContext db;

        public ManufacturerRepository()
        {
            db = MvcApplication.CurrentUnicefContext;
        }

        public IList<Manufacturer> GetAll()
        {
            return ManufacturerWithContact().ToList();
        }

        public Manufacturer GetById(int id)
        {
            return ManufacturerWithContact().FirstOrDefault(m => m.Id == id);
        }

        private DbQuery<Manufacturer> ManufacturerWithContact()
        {
            return db.Manufacturers.Include("Contact");
        }

        public IList<Manufacturer> GetByName(string name)
        {
            return ManufacturerWithContact().Where(m => m.Name == name).ToList();
        }

        public void Create(Manufacturer manufacturer)
        {
            db.Manufacturers.Add(manufacturer);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var manufacturerToDelete = db.Manufacturers.FirstOrDefault(m => m.Id == id);
            if (manufacturerToDelete != null)
                db.Manufacturers.Remove(manufacturerToDelete);

            db.SaveChanges();
        }
    }
}