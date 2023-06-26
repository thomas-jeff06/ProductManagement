using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Context;
using ProductManagement.Entity;

namespace ProductManagement.DAL.Repositories
{
    public class OrderRepository
    {
        public Order GetOrderById(long orderId)
        {
            using (var context = new DataContext())
            {
                return context.Order
                .FromSqlRaw("EXEC GetOrderById @ParameterOrderId", new SqlParameter("@ParameterOrderId", orderId))
                .ToList()
                .FirstOrDefault();
            }
        }

        public List<Order> GetOrders()
        {
            using (var context = new DataContext())
            {
                return context.Order
                .FromSqlRaw("EXEC GetOrders")
                .ToList();
            }
        }

        public long InsertOrder(Order order)
        {
            try
            {
                using (var context = new DataContext())
                {
                    long nextId = context.Order.Count() + 1;

                    var ParameterOrderId = new SqlParameter("@ParameterOrderId", nextId);
                    var ParameterIdentifier = new SqlParameter("@ParameterIdentifier", order.Identifier);
                    var ParameterDescription = new SqlParameter("@ParameterDescription", order.Description);
                    var ParameterTotalValue = new SqlParameter("@ParameterTotalValue", order.TotalValue);
                    var ParameterCreationDate = new SqlParameter("@ParameterCreationDate", order.CreationDate);
                    var ParameterUpdateDate = new SqlParameter("@ParameterUpdateDate", order.UpdateDate);

                    context.Database.ExecuteSqlRaw("EXEC InsertOrder @ParameterOrderId, @ParameterIdentifier, @ParameterDescription, @ParameterTotalValue, @ParameterCreationDate, @ParameterUpdateDate",
                        ParameterOrderId, ParameterIdentifier, ParameterDescription, ParameterTotalValue, ParameterCreationDate, ParameterUpdateDate);

                     return context.Order.Count();
                }

            } catch (Exception ex)
            {
                return 0;
            }
        }

        public Order GetOrderByProductId(long productId)
        {
            using (var context = new DataContext())
            {
                return context.Order
                .FromSqlRaw("EXEC GetOrderByProductId @ParameterProductId", new SqlParameter("@ParameterProductId", productId))
                .ToList()
                .FirstOrDefault();
            }
        }

        public bool DeleteOrder(long orderId)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var ParameterOrderId = new SqlParameter("@ParameterOrderId", orderId);

                    context.Database.ExecuteSqlRaw("EXEC DeleteOrder @ParameterOrderId",
                        ParameterOrderId);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateOrder(Order order)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var ParameterOrderId = new SqlParameter("@ParameterOrderId", order.OrderId);
                    var ParameterIdentifier = new SqlParameter("@ParameterIdentifier", order.Identifier);
                    var ParameterDescription = new SqlParameter("@ParameterDescription", order.Description);
                    var ParameterUpdateDate = new SqlParameter("@ParameterUpdateDate", order.UpdateDate);

                    context.Database.ExecuteSqlRaw("EXEC UpdateOrder @ParameterOrderId, @ParameterIdentifier, @ParameterDescription, @ParameterUpdateDate",
                        ParameterOrderId, ParameterIdentifier, ParameterDescription, ParameterUpdateDate);
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

