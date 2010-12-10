﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models.Repositories
{
    public class ProductRepository
    {
        private UnicefContext db;

        public ProductRepository()
        {
            db = MvcApplication.CurrentUnicefContext;
        }

        public IList<Product> GetAll()
        {
            return db.Product.ToList();
        }
 
        public void AddProduct(int catogoryId, Product product)
        {
            db.Product.Add(product);
            var categoryId = catogoryId;
            var category = db.ProductCatagories.Include("Products").FirstOrDefault(cat => cat.Id == categoryId);
            category.Products.Add(product);
            db.SaveChanges();
        }

        public IList<Product> GetByName(string name)
        {
            return db.Product.Where(p => p.Name == name).ToList();
        }

        public Product GetById(int productId)
        {
            return db.Product.Where(p => p.Id == productId).SingleOrDefault();
        }
    }
}