using MediatR;
using Microservicio_Productos.Models;
using Microservicio_Productos.Repositories;

namespace Microservicio_Productos.Mediador.Consultas;

// ConsultaListaProductos class to add coupon to the list of products
public class ConsultaListaProductos : IRequest<List<Product>>
{
}

// ConsultaListaProductosHandler class to handle the ConsultaListaProductos request
public class ConsultaListaProductosHandler : IRequestHandler<ConsultaListaProductos, List<Product>>
{
    private readonly IProductRepository productRepository;

    //constructor
    public ConsultaListaProductosHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    //Handle method to add coupon to the list of products 
    public async Task<List<Product>> Handle(ConsultaListaProductos request,
                                            CancellationToken cancellationToken)
    {
        var resultado = productRepository.GetProducts();

        return resultado;
    }
}

