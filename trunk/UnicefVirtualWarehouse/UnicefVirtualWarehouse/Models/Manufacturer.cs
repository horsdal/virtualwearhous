namespace UnicefVirtualWarehouse.Models
{
    public class Manufacturer
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public Contact Contact { get; set; }
		public bool GMP { get; set; }
    }
}