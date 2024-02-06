using Microservicio_Productos.Messaging;

namespace Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAzServiceBusConsumer(this IApplicationBuilder app)
        {
            var serviceBusConsumer = app.ApplicationServices.GetRequiredService<IAzServiceBusConsumer>();
            var hostApplicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            hostApplicationLifetime.ApplicationStarted.Register(() => OnStarted(serviceBusConsumer));
            hostApplicationLifetime.ApplicationStopping.Register(() => OnStopping(serviceBusConsumer));

            return app;
        }

        private static void OnStarted(IAzServiceBusConsumer serviceBusConsumer)
        {
            serviceBusConsumer.Start();
        }

        private static void OnStopping(IAzServiceBusConsumer serviceBusConsumer)
        {
            serviceBusConsumer.Stop();
        }
    }
}