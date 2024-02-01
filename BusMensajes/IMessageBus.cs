namespace BusMensajes;
public interface IMessageBus
{
    Task PublicarMensaje (IntegrationBaseMessage message, string topicName, string connectionString);
}
