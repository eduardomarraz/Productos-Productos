namespace Microservicio_Productos.Dtos
{
    public class ProductCreatedDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
