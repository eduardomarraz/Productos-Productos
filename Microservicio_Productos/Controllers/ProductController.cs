using AutoMapper;
using Microservicio_Productos.DbContexts;
using Microservicio_Productos.Dtos;
using Microservicio_Productos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio_Productos.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductsDbContext _productsDbContext;
        private readonly IMapper _mapper;

        public ProductController(ProductsDbContext productsDbContext, IMapper mapper)
        {
            this._productsDbContext = productsDbContext;
            this._mapper = mapper;
        }

        //api/products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var productDto = new List<ProductDto>();
            var products = _productsDbContext.Products;
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        
        [HttpGet("{guid}")]
        public IActionResult GetProductsId(Guid guid)
        {
            var product = _productsDbContext.Products.FirstOrDefault(product => product.ProductId == guid);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(product));
 
        }

        [HttpPost]

        public IActionResult CreateProduct([FromBody]ProductCreatedDto productCreatedDto) 
        {
        var product = new Product();

            _mapper.Map(productCreatedDto, product);

            _productsDbContext.Products.Add(product);

            var result = _productsDbContext.SaveChanges();

            return Created("", productCreatedDto);
        
        }
    }
}
