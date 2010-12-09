using System;
using System.Linq;
using System.Collections.Generic;

namespace UnicefVirtualWarehouse.Models.Repositories
{
    public class ProductCategoryRepository
    {
        private UnicefContext db;

        public ProductCategoryRepository()
        {
            db = MvcApplication.CurrentUnicefContext;
        }

        public IList<ProductCategory> GetByName(string name)
        {
            return db.ProductCatagories.Where(cat => cat.Name == name).ToList();
        }

        public void AddProductCategory(ProductCategory productCategory)
        {
            db.ProductCatagories.Add(productCategory);
            db.SaveChanges();
        }
    }
}