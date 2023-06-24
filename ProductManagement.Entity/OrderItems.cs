
namespace ProductManagement.Entity
{
    [Serializable]
    public class OrderItems : BaseEntity
    {
        public long OrderItemsId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}
