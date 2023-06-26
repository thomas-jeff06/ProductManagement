using Newtonsoft.Json;
using ProductManagement.DAL.Repositories;
using ProductManagement.Entity;
using static ProductManagement.Entity.Enum;

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
                    orderItem.CreationDate = DateTime.Now;
                    orderItem.UpdateDate = DateTime.Now;

                    orderItemsRepository.InsertOrderItems(orderItem);
                }
                return true;
            }
            
            return false;
        }

        public string RemoveProductToOrder(List<OrderItems> orderItems, long productId)
        {
            OrderItems orderItemsAux = new OrderItems();

            foreach(OrderItems orderItem in orderItems)
            {
                if(orderItem.ProductId == productId)
                {
                    orderItemsAux = orderItem;
                }
            }

            orderItems.Remove(orderItemsAux);

            string newOrderItems = System.Text.Json.JsonSerializer.Serialize(orderItems);

            return newOrderItems;

        }

        public bool InsertNewProduct(string productName, int category)
        {
            ProductRepository productRepository = new ProductRepository();
            Product newProduct = new Product();

            newProduct.ProductName = productName;
            newProduct.Category = (TypeCategory)System.Enum.Parse(typeof(TypeCategory), category.ToString());
            newProduct.CreationDate = DateTime.Now;
            newProduct.UpdateDate = DateTime.Now;

            return productRepository.InsertProduct(newProduct);
        }

        public bool UpdateProduct(long productId, string productName, TypeCategory category)
        {
            ProductRepository productRepository = new ProductRepository();
            Product product = productRepository.GetProductById(productId);

            product.ProductName = productName;
            product.Category = category;

            product.UpdateDate = DateTime.Now;

            return productRepository.UpdateProduct(product);
        }

        private string GenerateIdentifier()
        {
            string identifier;

            Random random = new Random(); 
            
            char charRandom = (char)(random.Next('A', 'Z' + 1));
            string numbersRandom = random.Next(0, 1000).ToString("D3");

            identifier = "P" + "_" + charRandom.ToString() + numbersRandom + "_" + "C";

            return identifier;
        }

        public bool ProductIsAssociatedAnOrder(long productId)
        {
            OrderRepository orderRepository = new OrderRepository();

            Order order = orderRepository.GetOrderByProductId(productId);

            if (order == null)
                return false;

            return true;
        }

        public bool RemoveProduct(long productId)
        {
            ProductRepository productRepository = new ProductRepository();

            if(productRepository.DeleteProduct(productId))
                return true;

            return false;
        }

        public bool RemoveOrder(long orderId)
        {
            OrderItemsRepository orderItemsRepository = new OrderItemsRepository();
            OrderRepository orderRepository = new OrderRepository();

            List<OrderItems> orderItems = orderItemsRepository.GetOrderItemsByOrderId(orderId);

            foreach (OrderItems item in orderItems)
            {
                orderItemsRepository.DeleteOrderItems(item.OrderItemsId);
            }

            orderRepository.DeleteOrder(orderId);

            return true;
        }

        public bool UpdateOrder(long orderId, string Identifier, string description)
        {
            OrderRepository orderRepository = new OrderRepository();
            Order order = orderRepository.GetOrderById(orderId);

            order.Identifier = Identifier;
            order.Description = description;

            order.UpdateDate = DateTime.Now;

            return orderRepository.UpdateOrder(order);

        }
    }
}
