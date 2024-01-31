using BusMensajes;

namespace Microservicio_Productos.Dtos
{
    public class MensajeCreacion : IntegrationBaseMessage
    {
        public Guid ProductId { get; set; }
    }
}
