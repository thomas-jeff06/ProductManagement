using System.ComponentModel.DataAnnotations.Schema;
using static ProductManagement.Entity.Enum;

namespace ProductManagement.Entity
{
    public class Product :  BaseEntity
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        [Column(TypeName = "tinyint")]
        public TypeCategory Category { get; set; }
    }
}
