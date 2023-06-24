using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Context;
using ProductManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repositories
{
    public class ProductRepository
    {
        public List<Product> GetProducts()
        {
            using (var context = new DataContext())
            {
                return context.Product
                .FromSqlRaw("EXEC GetProducts")
                .ToList();
            }
        }

        public List<Product> GetProductsByName(string name)
        {
            using (var context = new DataContext())
            {
                return context.Product
                .FromSqlRaw("EXEC GetProducts")
                .ToList();
            }
        }

        public Product GetProductById(long productId)
        {
            using (var context = new DataContext())
            {
                return context.Product
                .FromSqlRaw("EXEC GetProductById @ParameterProductId", new SqlParameter("@ParameterProductId", productId))
                .ToList()
                .FirstOrDefault();
            }
        }
    }
}
