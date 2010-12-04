using System.Data.Entity;
using UnicefVirtualWarehouse.Models;


namespace UnicefVirtualWarehouse
{
    public class UnicefContext : DbContext
    {
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ManufacturerPresentation> ManufacturerPresentations { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
    }
}