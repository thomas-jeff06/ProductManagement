using Microsoft.AspNetCore.Mvc;
using ProductManagement.DAL.Repositories;
using ProductManagement.DAL.Util;
using ProductManagement.Entity;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult OrderListManage()
        {
            OrderRepository orderRepository = new OrderRepository();
            List<Order> orders = orderRepository.GetOrders();

            return View("~/Views/Order/OrderListManage.cshtml", orders);
        }

        public IActionResult GetProductsInOrder(long orderId)
        {
            ProductRepository productRepository = new ProductRepository(); 
            OrderItemsRepository orderItemsRepository = new OrderItemsRepository();
            ViewModel viewModel = new ViewModel();
            Product product = new Product();

            viewModel.OrderItems = orderItemsRepository.GetOrderItemsByOrderId(orderId);
            viewModel.Products = new List<Product>();

            foreach (OrderItems orderItems in viewModel.OrderItems)
            {
                product = productRepository.GetProductById(orderItems.ProductId);

                viewModel.Products.Add(product);
            }

            return View("~/Views/Order/ProductListToOrder.cshtml", viewModel);
        }

        public IActionResult UpdateOrder(long orderId)
        {
            OrderRepository orderRepository = new OrderRepository();
            Order order = orderRepository.GetOrderById(orderId);

            return View("~/Views/Order/UpdateOrder.cshtml", order);
        }

        public IActionResult UpdateToOrder(long orderId, string Identifier, string description)
        {
            TransactionTRA transactionTRA = new TransactionTRA();

            bool sucess = transactionTRA.UpdateOrder(orderId, Identifier, description);

            return RedirectToAction("OrderListManage");
        }

        public IActionResult RemoveOrder(long orderId)
        {
            ProductRepository productRepository = new ProductRepository();
            TransactionTRA transactionTRA = new TransactionTRA();

            if (transactionTRA.RemoveOrder(orderId))
            {
                TempData["Error"] = "Pedido Removido";
                return RedirectToAction("OrderListManage");
            }
            else
                TempData["Error"] = "Algo deu errado, Tente novamente depois.";

            return RedirectToAction("OrderListManage");
        }
    }
}
