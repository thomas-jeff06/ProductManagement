
namespace ProductManagement.Entity
{
    public class Order : BaseEntity
    {
        public long OrderId { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public decimal TotalValue { get; set; }
    }
}
