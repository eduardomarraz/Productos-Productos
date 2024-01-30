using AutoMapper;
using Microservicio_Productos.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio_Productos.Controllers
{  //TODO Finalizar ControladorCategoris
    [Authorize]
    [Route("api/products")]
    [ApiController]
    public class CategoryController : Controller
    {
        //Campos
        private readonly ProductsDbContext productsDbContext;
        private readonly IMapper mapper;

        public CategoryController(ProductsDbContext productsDbContext, IMapper mapper)
        {
            this.productsDbContext = productsDbContext; //BD
            this.mapper = mapper;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        //{
        //    var result = await productsDbContext.GetAllCategories();
        //    return Ok(_mapper.Map<List<CategoryDto>>(result));
        //}
    }
}
