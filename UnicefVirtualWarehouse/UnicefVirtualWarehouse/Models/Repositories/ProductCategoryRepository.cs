using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

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

        public IList<ProductCategory> GetAll()
        {
            return db.ProductCatagories.Include("Products").ToList();
        }

        public ProductCategory GetById(int id)
        {
            return db.ProductCatagories.Include("Products").Where(pc => pc.Id == id).FirstOrDefault();
        }

        public void DeleteById(int id)
        {
            var categoryToDelete = GetById(id);
            if (categoryToDelete != null && categoryToDelete.Products.Count() == 0)
                Delete(categoryToDelete);
        }

        private void Delete(ProductCategory categoryToDelete)
        {
            db.ProductCatagories.Remove(categoryToDelete);
            db.SaveChanges();
        }
    }
}