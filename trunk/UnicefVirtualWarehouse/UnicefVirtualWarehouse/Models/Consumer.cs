namespace UnicefVirtualWarehouse.Models
{
    public class Consumer
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public Contact Contact { get; set; }
    }
}