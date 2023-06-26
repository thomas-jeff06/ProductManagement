using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
               .FromSqlRaw("EXEC GetProductsByName @ParameterName", new SqlParameter("@ParameterName", name))
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

        public bool InsertProduct(Product product)
        {
            using (var context = new DataContext())
            {
                long nextId = context.Product.Count() + 1;

                var ParameterProductId = new SqlParameter("@ParameterProductId", nextId);
                var ParameterProductName = new SqlParameter("@ParameterProductName", product.ProductName);
                var ParameterProductCategory = new SqlParameter("@ParameterProductCategory", product.Category);
                var ParameterCreationDate = new SqlParameter("@ParameterCreationDate", product.CreationDate);
                var ParameterUpdateDate = new SqlParameter("@ParameterUpdateDate", product.UpdateDate);

                context.Database.ExecuteSqlRaw("EXEC InsertProduct @ParameterProductId, @ParameterProductName, @ParameterProductCategory, @ParameterCreationDate, @ParameterUpdateDate",
                        ParameterProductId, ParameterProductName, ParameterProductCategory, ParameterCreationDate, ParameterUpdateDate);
            }

            return true;
        }

        public bool UpdateProduct(Product product)
        {
            using (var context = new DataContext())
            {
                var ParameterProductId = new SqlParameter("@ParameterProductId", product.ProductId) ;
                var ParameterProductName = new SqlParameter("@ParameterProductName", product.ProductName);
                var ParameterProductCategory = new SqlParameter("@ParameterProductCategory", product.Category);
                var ParameterUpdateDate = new SqlParameter("@ParameterUpdateDate", product.UpdateDate);

                context.Database.ExecuteSqlRaw("EXEC UpdateProduct @ParameterProductId, @ParameterProductName, @ParameterProductCategory, @ParameterUpdateDate",
                        ParameterProductId, ParameterProductName, ParameterProductCategory, ParameterUpdateDate);
            }

            return true;
        }

        public bool DeleteProduct(long productId)
        {
            using (var context = new DataContext())
            {
                var ParameterProductId = new SqlParameter("@ParameterProductId", productId);

                context.Database.ExecuteSqlRaw("EXEC DeleteProduct @ParameterProductId",
                        ParameterProductId);
            }

            return true;
        }
    }
}
