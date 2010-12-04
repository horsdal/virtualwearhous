﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}