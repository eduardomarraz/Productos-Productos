

using MediatR;
using Microservicio_Productos.Mediador.Consultas;
using Microservicio_Productos.Repositories;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;

namespace Microservicio_Productos.Messaging
{
    public class AzServiceBusConsumer : IAzServiceBusConsumer
    {
        private readonly string subscriptionName = "mensajerecibido";
        private readonly string topicomensaje;
        private readonly IReceiverClient ReceptorMensajes;

        private readonly IConfiguration _configuration;
        private readonly IMediator mediator;

        public AzServiceBusConsumer(IConfiguration configuration, IMediator mediator )
        {
            _configuration = configuration;
            this.mediator = mediator;
            var serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            topicomensaje = _configuration.GetValue<string>("productosTema"); //Tema

            ReceptorMensajes = new SubscriptionClient(serviceBusConnectionString, topicomensaje, subscriptionName);
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 1, AutoComplete = true };

            ReceptorMensajes.RegisterMessageHandler(OnPeticionProductoRecibida, messageHandlerOptions);
        }

        private async Task OnPeticionProductoRecibida(Message message, CancellationToken arg2)
        {
            
            var body = Encoding.UTF8.GetString(message.Body);//json from service bus
            ConsultaListaProductos consulta = new ConsultaListaProductos();
            await mediator.Send(consulta);

        }

        private Task OnServiceBusException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs);

            return Task.CompletedTask;
        }

        public void Stop()
        {
        }
    }
}
