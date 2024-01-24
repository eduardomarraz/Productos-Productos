using Microservicio_Productos.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace Microservicio_Productos.DbContexts
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


    }
}
