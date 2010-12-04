using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models
{
    public class Supplier
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public Contact Contact { get; set; }
    }
}