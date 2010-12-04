using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models
{
    public class SupplierProduct
    {
        public int Id { get; private set; }
        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
        public bool Licensed { get; set; }
    }
}