using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnicefVirtualWarehouse.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public Manufacturer AssociatedManufaturer { get; set; }
    }
}
