using AutoMapper;
using BusMensajes;
using Microservicio_Productos.DbContexts;
using Microservicio_Productos.Dtos;
using Microservicio_Productos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;

namespace Microservicio_Productos.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductsDbContext _productsDbContext;
        private readonly IMapper _mapper;
        private readonly IMessageBus iMessageBus;
        private readonly IConfiguration configuration;
        private static HttpClient _httpClient = new HttpClient();

        public ProductController(ProductsDbContext productsDbContext, IMapper mapper, IMessageBus iMessageBus, IConfiguration configuration)
        {
            this._productsDbContext = productsDbContext;
            this._mapper = mapper;
            this.iMessageBus = iMessageBus;
            this.configuration = configuration;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://proyectoapismarketing.azurewebsites.net");

                //Espera máximo 30 segundos
                _httpClient.Timeout = new TimeSpan(0, 0, 30);
                //Para borrar cabeceras del Headers
                _httpClient.DefaultRequestHeaders.Clear();
                //Introducir cabecera para 
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
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

        public async Task<IActionResult> CreateProduct([FromBody]ProductCreatedDto productCreatedDto) 
        {
            var product = new Product();

            _mapper.Map(productCreatedDto, product);

            _productsDbContext.Products.Add(product);

            var result = _productsDbContext.SaveChanges();

            var dtomarketing = new marketingDto() { idProducto = product.ProductId };

            

            var mensaje = new MensajeCreacion { ProductId = product.ProductId, Id = Guid.NewGuid(), CreationDateTime = DateTime.Now };

            var serviceBusConnectionString = configuration.GetValue<string>("ServiceBusConnectionString");
            // Integració de mensajeria asincrona
            await iMessageBus.PublicarMensaje(mensaje, "productocreado", serviceBusConnectionString);

            await CrearProductoEnMarketing(dtomarketing);

           return Created("", productCreatedDto);

            
        
        }

        private async Task CrearProductoEnMarketing(marketingDto producto)
        {
            try
            {
                var respuesta = await _httpClient.PostAsync(
                    "api/M_Producto",
                    new StringContent(
                             JsonConvert.SerializeObject(producto),
                             Encoding.UTF8,
                             "application/json"
                          )
                    );

                respuesta.EnsureSuccessStatusCode();

                var contenido = await respuesta.Content.ReadAsStringAsync();
                var productoCreado = JsonConvert.DeserializeObject(contenido);
                var headers = respuesta.Content.Headers;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex.Message);
            }
        }
    }
}
