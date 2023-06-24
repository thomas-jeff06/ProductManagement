using Microsoft.AspNetCore.Mvc;
using ProductManagement.DAL.Repositories;
using ProductManagement.Entity;

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

        public IActionResult FilterProductByName(string input)
        {
            ProductRepository productRepository = new ProductRepository();

            List<Product> products = productRepository.GetProductsByName(input);

            return View("~/Views/Sales/ProductManagementHome.cshtml", products);
        }

        public IActionResult InsertProduct()
        {
            return View();
        }

        public IActionResult UpdateProduct(long ProductId)
        {
            return View();
        }

        public IActionResult RemoveProduct(long ProductId)
        {
            return View();
        }
    }
}
