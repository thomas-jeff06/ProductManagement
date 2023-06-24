using ProductManagement.Entity;

namespace ProductManagement.Models
{
    public class ViewModel
    {
        public List<Product> Products { get; set; }
        public List<OrderItems>? OrderItems { get; set; }
        public Product ProductSelect { get; set; }
        public int AmountProduct { get; set; }
        public decimal ValueProduct { get; set; }
    }
}
