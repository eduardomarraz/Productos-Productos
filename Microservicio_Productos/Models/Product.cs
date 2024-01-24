namespace Microservicio_Productos.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
