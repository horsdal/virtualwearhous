using System;
using System.Collections.Generic;
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
            return db.Manufacturers.ToList();
        }

        public Manufacturer GetById(int id)
        {
            return db.Manufacturers.FirstOrDefault(m => m.Id == id);
        }
    }
}