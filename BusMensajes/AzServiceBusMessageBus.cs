
namespace BusMensajes;

public class AzServiceBusMessageBus: IMessageBus
{
    
   
    public async Task PublicarMensaje(IntegrationBaseMessage message, string topicName,string connectionString)
    {
      
        ISenderClient topicClient = new TopicClient(connectionString, topicName );

        var jsonMessage = JsonConvert.SerializeObject(message);
        var serviceBusMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage))
        {
            CorrelationId = Guid.NewGuid().ToString()
        };

        await topicClient.SendAsync(serviceBusMessage);
        Console.WriteLine($"Sent message to {topicClient.Path}");
        await topicClient.CloseAsync();

    }
}
