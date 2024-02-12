using BusMensajes;

namespace Microservicio_Productos.Dtos
{
    public class MensajeCreacion : IntegrationBaseMessage
    {
        public Guid ProductId { get; set; }

        public Guid? ID_Producto { get; set; }
    }
}
