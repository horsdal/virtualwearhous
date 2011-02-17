using System.Collections.Generic;

namespace UnicefVirtualWarehouse.Models
{
    public class Presentation
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public Manufacturer Owner { get; set; }
    }
}