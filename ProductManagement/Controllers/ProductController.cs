using Microsoft.AspNetCore.Mvc;
using ProductManagement.DAL.Repositories;
using ProductManagement.Entity;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductListManage()
        {
            ProductRepository productRepository = new ProductRepository();
            List<Product> products = productRepository.GetProducts();

            return View("~/Views/Product/ProductListManage.cshtml", products);
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
