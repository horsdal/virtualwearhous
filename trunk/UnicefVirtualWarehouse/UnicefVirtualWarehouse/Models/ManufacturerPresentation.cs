using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models
{
    public class ManufacturerPresentation
    {
        public int ID { get; set; }
		public Manufacturer Manufacturer { get; private set; }
		public Presentation Presentation { get; set; }
		public Contact ManufacturingSite { get; set; }
        public bool Licensed { get; set; }
		public bool CPP { get; set; }
		public int MinUnit { get; set; }
		public int Size { get; set; }
		public int Price { get; set; }
    }
}