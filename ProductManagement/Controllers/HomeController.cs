using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProductManagement.DAL.Context;
using ProductManagement.DAL.Repositories;
using ProductManagement.DAL.Util;
using ProductManagement.Entity;
using ProductManagement.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string orderItems)
        {
            return View(orderItems);
        }

        public IActionResult ProductManagementHome(string orderItems)
        {
            ProductRepository productRepository = new ProductRepository();
            ViewModel viewModel = new ViewModel();
            List<Product> products = productRepository.GetProducts();
            TransactionTRA transactionTRA = new TransactionTRA();

            viewModel.Products = products;
            viewModel.OrderItems = transactionTRA.ConvertJsonToOrderItems(orderItems);

            return View("~/Views/Sales/ProductManagementHome.cshtml", viewModel);
        }

        public IActionResult InsertNewProdutoToOrder(long productId, string orderItems)
        {
            ProductRepository productRepository = new ProductRepository();
            ViewModel viewModel = new ViewModel();
            TransactionTRA transactionTRA = new TransactionTRA();

            viewModel.OrderItems = transactionTRA.ConvertJsonToOrderItems(orderItems);

            viewModel.ProductSelect = productRepository.GetProductById(productId);

            if (!viewModel.OrderItems.IsNullOrEmpty())
            {
                foreach (OrderItems orderItem in viewModel.OrderItems)
                {
                    if (orderItem.ProductId == productId)
                    {
                        TempData["Error"] = "O productId já existe na lista!";
                        return RedirectToAction("ProductManagementHome", new { orderItems = orderItems });
                    }
                }
            }

            return View("~/Views/Sales/InsertNewOrderItems.cshtml", viewModel);
        }

        public IActionResult NewOrderItemsToInsert(int amountProduct, decimal valueProduct, string orderItems, long productId)
        {
            ProductRepository productRepository = new ProductRepository();
            OrderItems newOrderItems = new OrderItems();
            Product product = new Product();
            ViewModel viewModel = new ViewModel();
            List<Product> products = new List<Product>();
            TransactionTRA transactionTRA = new TransactionTRA();

            viewModel.Products = products;

            viewModel.OrderItems = transactionTRA.ConvertJsonToOrderItems(orderItems);

            if (amountProduct > 0)
            {
                newOrderItems.ProductId = productId;
                newOrderItems.Value = valueProduct;
                newOrderItems.Quantity = amountProduct;

                viewModel.OrderItems.Add(newOrderItems);
            }

            foreach (OrderItems item in viewModel.OrderItems)
            {
                product = productRepository.GetProductById(item.ProductId);
                viewModel.Products.Add(product);
            }

            return View("~/Views/Sales/NewOrderItemsToInsert.cshtml", viewModel);
        }

        public IActionResult RemoveProdutoToOrder(long productId, List<OrderItems> orderItems)
        {
            return View();
        }

        public IActionResult InsertNewOrderItems(string orderItems)
        {
            TransactionTRA transactionTRA = new TransactionTRA();

            if(transactionTRA.InsertNewOrder(transactionTRA.ConvertJsonToOrderItems(orderItems)))
            {
                return RedirectToAction("AlertSucess", new { message = "Pedido Adicionado!" });
            }

            TempData["Error"] = "Algo Deu Errado, tente novamente mais tarde!";

            return RedirectToAction("NewOrderItemsToInsert", new { orderItems = orderItems });
        }

        public IActionResult AlertSucess(string message)
        {
            return View("~/Views/Sales/AlertSucess.cshtml", message);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}