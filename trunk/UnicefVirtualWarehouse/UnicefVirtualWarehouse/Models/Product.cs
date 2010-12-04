using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public ICollection<Presentation> Presentations { get; set; }
        public int PrductCatagory_ID { get; set; }
    }
}