using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Context;
using ProductManagement.Entity;

namespace ProductManagement.DAL.Repositories
{
    public class OrderItemsRepository
    {
        public bool InsertOrderItems(OrderItems orderItems)
        {
            try
            {
                using (var context = new DataContext())
                {
                    long nextId = context.OrderItems.Count() + 1;

                    var ParameterOrderItemsId = new SqlParameter("@ParameterOrderItemsId", nextId);
                    var ParameterOrderId = new SqlParameter("@ParameterOrderId", orderItems.OrderId);
                    var ParameterProductId = new SqlParameter("@ParameterProductId", orderItems.ProductId);
                    var ParameterQuantity = new SqlParameter("@ParameterQuantity", orderItems.Quantity);
                    var ParameterValue = new SqlParameter("@ParameterValue", orderItems.Value);
                    var ParameterCreationDate = new SqlParameter("@ParameterCreationDate", orderItems.CreationDate);
                    var ParameterUpdateDate = new SqlParameter("@ParameterUpdateDate", orderItems.UpdateDate);

                    context.Database.ExecuteSqlRaw("EXEC InsertOrderItems @ParameterOrderItemsId, @ParameterOrderId, @ParameterProductId, @ParameterQuantity, @ParameterValue, @ParameterCreationDate, @ParameterUpdateDate",
                        ParameterOrderItemsId, ParameterOrderId, ParameterProductId, ParameterQuantity, ParameterValue, ParameterCreationDate, ParameterUpdateDate);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }     
        }

        public List<OrderItems> GetOrderItemsByOrderId(long orderId)
        {
            using (var context = new DataContext())
            {
                var orderIdParameter = new SqlParameter("@ParameterOrderId", orderId);

                return context.OrderItems
                .FromSqlRaw("EXEC GetOrderItemsByOrderId @ParameterOrderId", orderIdParameter)
                .ToList();
            }
        }

        public bool DeleteOrderItems(long orderItemsId)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var ParameterOrderItemsId = new SqlParameter("@ParameterOrderItemsId", orderItemsId);

                    context.Database.ExecuteSqlRaw("EXEC DeleteOrderItems @ParameterOrderItemsId",
                        ParameterOrderItemsId);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
