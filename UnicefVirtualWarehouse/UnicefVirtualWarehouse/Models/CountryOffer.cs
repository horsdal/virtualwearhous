using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models
{
    public class CountryOffer
    {
        public int Id { get; private set; }
        public Country Country { get; set; }
        public Package Package { get; set; }
        public int PriceFactor { get; set; }
        public bool AuthorizedForSelling { get; set; }
    }
}