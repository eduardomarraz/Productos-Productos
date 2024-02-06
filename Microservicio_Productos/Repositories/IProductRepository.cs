

using Microservicio_Productos.Dtos;
using Microservicio_Productos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio_Productos.Repositories
{
    public interface IProductRepository
    {
       List<Product> GetProducts();
       Product GetProductsId(Guid guid);

       Product CreateProduct (Product product);

        int SaveChanges();


    }
}