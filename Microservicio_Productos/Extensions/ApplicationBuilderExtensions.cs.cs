

//using Microsoft.Azure.ServiceBus;
//using Microsoft.Azure.ServiceBus.Core;


//namespace GloboTicket.Services.Ordering.Messaging
//{
//    public class AzServiceBusConsumer : IAzServiceBusConsumer
//    {
//        private readonly string NombreSuscripcion = "test";

//        private readonly IReceiverClient PagoConfirmado;

//        private readonly IConfiguration _configuracion;


//        private readonly IMessageBus _mensajesBus;


//        private readonly string confirmarpagoTema;
//        public AzServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus)
//        {
//            _configuracion = configuration;
//            //_orderRepository = orderRepository;
//            // _logger = logger;
//            _mensajesBus = messageBus;

//            var serviceBusConnectionString = _configuracion.GetValue<string>("ServiceBusConnectionString");
//            //checkoutMessageTopic = _configuracion.GetValue<string>("CheckoutMessageTopic");
//            //orderPaymentRequestMessageTopic = _configuracion.GetValue<string>("OrderPaymentRequestMessageTopic");
//            confirmarpagoTema = _configuracion.GetValue<string>("confirmarpagoTema");

//            //checkoutMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, checkoutMessageTopic, NombreSuscripcion);
//            PagoConfirmado = new SubscriptionClient(serviceBusConnectionString, confirmarpagoTema, NombreSuscripcion);
//        }

//        public void Start()
//        {
//            var messageHandlerOptions = new MessageHandlerOptions(ErrorServicioBus) { MaxConcurrentCalls = 4 };

//            //checkoutMessageReceiverClient.RegisterMessageHandler(OnCheckoutMessageReceived, messageHandlerOptions);
//            PagoConfirmado.RegisterMessageHandler(OnConfirmarPago, messageHandlerOptions);
//        }

//        private async Task OnConfirmarPago(Message mensaje, CancellationToken arg2)
//        {
//            var body = Encoding.UTF8.GetString(mensaje.Body);//json from service bus
//            try
//            {
//                PagosDto Pagosdto =
//    JsonConvert.DeserializeObject<PagosDto>(body);
//            }
//            catch (Exception)
//            {


//            }


//            //await _orderRepository.UpdateOrderPaymentStatus(orderPaymentUpdateMessage.OrderId, orderPaymentUpdateMessage.PaymentSuccess);
//        }
//        private Task ErrorServicioBus(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
//        {
//            Console.WriteLine(exceptionReceivedEventArgs);

//            return Task.CompletedTask;
//        }

//        public void Stop()
//        {
//        }

        /*private async Task OnCheckoutMessageReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);//json from service bus

            //save order with status not paid
            BasketCheckoutMessage basketCheckoutMessage = JsonConvert.DeserializeObject<BasketCheckoutMessage>(body);

            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                UserId = basketCheckoutMessage.UserId,
                Id = orderId,
                OrderPaid = false,
                OrderPlaced = DateTime.Now,
                OrderTotal = basketCheckoutMessage.BasketTotal
            };

            await _orderRepository.AddOrder(order);

            //send order payment request message
            OrderPaymentRequestMessage orderPaymentRequestMessage = new OrderPaymentRequestMessage
            {
                CardExpiration = basketCheckoutMessage.CardExpiration,
                CardName = basketCheckoutMessage.CardName,
                CardNumber = basketCheckoutMessage.CardNumber,
                OrderId = orderId,
                Total = basketCheckoutMessage.BasketTotal
            };

            try
            {
                await _mensajesBus.PublishMessage(orderPaymentRequestMessage, orderPaymentRequestMessageTopic);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }*/



//    }
//}

