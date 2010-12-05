using System.Collections.Generic;

namespace UnicefVirtualWarehouse.Models
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public ICollection<Presentation> Presentations { get; set; }
    }
}