using Microsoft.AspNetCore.Mvc;
using ProductManagement.DAL.Repositories;
using ProductManagement.DAL.Util;
using ProductManagement.Entity;
using static ProductManagement.Entity.Enum;

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
            return View("~/Views/Product/InsertProduct.cshtml");
        }

        public IActionResult UpdateProduct(long productId)
        {
            ProductRepository productRepository = new ProductRepository();
            TransactionTRA transactionTRA = new TransactionTRA();

            Product product = productRepository.GetProductById(productId);


            if (transactionTRA.ProductIsAssociatedAnOrder(product.ProductId))
            {
                TempData["Condition"] = "Não é possivel alterar Categoria desse produto";
            }

            return View("~/Views/Product/UpdateProduct.cshtml", product);
        }

        public IActionResult RemoveProduct(long productId)
        {
            ProductRepository productRepository = new ProductRepository();
            TransactionTRA transactionTRA = new TransactionTRA();

            Product product = productRepository.GetProductById(productId);


            if (transactionTRA.ProductIsAssociatedAnOrder(product.ProductId))
            {
                TempData["Error"] = "Não é possivel Remover esse produto, pois está associado a um pedido";
                return RedirectToAction("ProductListManage");
            }

            else if (transactionTRA.RemoveProduct(productId))
            {
                TempData["Error"] = "Produto Removido";
                return RedirectToAction("ProductListManage");
            }
            else
                TempData["Error"] = "Algo deu errado, Tente novamente depois.";
                return RedirectToAction("ProductListManage");
        }

        public IActionResult InsertNewProduct(string productName, int category)
        {
            TransactionTRA transactionTRA = new TransactionTRA();

            bool sucess = transactionTRA.InsertNewProduct(productName, category);

            if (sucess)
                return View("~/Views/Sales/AlertSucess.cshtml", "Novo Produto adicionado!");
            else
                return View();
        }

        public IActionResult ProductToUpdate(long productId, string productName, TypeCategory category)
        {
            TransactionTRA transactionTRA = new TransactionTRA();

            bool sucess = transactionTRA.UpdateProduct(productId, productName, category);

            return RedirectToAction("ProductListManage");
        }
    }
}
