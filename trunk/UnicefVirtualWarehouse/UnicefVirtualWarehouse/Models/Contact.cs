using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models
{
    public class Contact
    {
        public int Id { get; private set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
    }
}