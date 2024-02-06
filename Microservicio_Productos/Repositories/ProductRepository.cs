
using BusMensajes;
using Microservicio_Productos.DbContexts;
using Microservicio_Productos.Dtos;
using Microservicio_Productos.Models;
using Microservicio_Productos.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Microservicio_Productos.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext productsDbContext;

        public ProductRepository(ProductsDbContext productsDbContext)
        {
            this.productsDbContext = productsDbContext;
        }

        public Product CreateProduct(Product product)
        {
            product.ProductId = Guid.NewGuid();
            productsDbContext.Products.Add(product);
            SaveChanges();
            return product;
        }

        public List<Product> GetProducts()
        {
            return productsDbContext.Products.ToList();
        }

        public Product GetProductsId(Guid guid)
        {
            return productsDbContext.Products.FirstOrDefault(e => e.ProductId == guid);
        }

        public int SaveChanges()
        {
            var result = productsDbContext.SaveChanges();
            return result;
        }
    }
}
