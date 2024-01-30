using AutoMapper;
using Microservicio_Productos.Dtos;
using Microservicio_Productos.Models;

namespace Microservicio_Productos.PerfilesAutoMapper
{
    public class PerfilProduct : Profile
    {

        public PerfilProduct()
        {
            CreateMap<Product, marketingDto>();
            CreateMap<Product, ProductCreatedDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreatedDto, Product>();


        }

    }

}
