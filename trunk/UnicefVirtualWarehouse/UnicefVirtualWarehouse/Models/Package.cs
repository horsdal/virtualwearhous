using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models
{
    public class Package
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public int MinUnit { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
    }
}