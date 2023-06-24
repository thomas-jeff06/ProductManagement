using Newtonsoft.Json;
using ProductManagement.DAL.Repositories;
using ProductManagement.Entity;
using System;

namespace ProductManagement.DAL.Util
{
    public class TransactionTRA
    {
        public List<OrderItems> ConvertJsonToOrderItems(string orderItemsJsom)
        {
            List<OrderItems> orderItemsList = new List<OrderItems> ();

            if (!string.IsNullOrEmpty(orderItemsJsom))
                orderItemsList = JsonConvert.DeserializeObject<List<OrderItems>>(orderItemsJsom);

            return orderItemsList;
        }

        public bool InsertNewOrder(List<OrderItems> orderItems)
        {
            OrderRepository orderRepository = new OrderRepository();
            OrderItemsRepository orderItemsRepository = new OrderItemsRepository();
            ProductRepository productRepository = new ProductRepository();
            Product product = new Product();
            Order newOrder = new Order();

            newOrder.Identifier = GenerateIdentifier();

            newOrder.CreationDate = DateTime.Now;
            newOrder.UpdateDate = DateTime.Now;
            
            foreach(OrderItems orderItem in orderItems)
            {
                product = productRepository.GetProductById(orderItem.ProductId);

                newOrder.Description += "|" + product.ProductName + "|";

                newOrder.TotalValue += (orderItem.Value * orderItem.Quantity);
            }

            long orderId = orderRepository.InsertOrder(newOrder);

            if (orderId > 0)
            {
                foreach (OrderItems orderItem in orderItems)
                {
                    orderItem.OrderId = orderId;
                    orderItem.ProductId = product.ProductId;
                    orderItem.CreationDate = DateTime.Now;
                    orderItem.UpdateDate = DateTime.Now;

                    orderItemsRepository.InsertOrderItems(orderItem);
                }
                return true;
            }
            
            return false;
        }

        private string GenerateIdentifier()
        {
            string identifier;

            Random random = new Random(); 
            
            char charRandom = (char)(random.Next('A', 'Z' + 1));
            string numbersRandom = random.Next(0, 1000).ToString("D3") + random.Next(0, 1000).ToString("D3") + random.Next(0, 1000).ToString("D3");

            identifier = "P" + "_" + charRandom.ToString() + numbersRandom + "_" + "C";

            return identifier;
        }
    }
}
