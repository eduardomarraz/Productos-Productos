namespace BusMensajes;

public class AzServiceBusMessageBus: IMessageBus
{
    private string connectionString =
        "Endpoint=sb://misaservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=KEnbRG44lOLu+kUZkJpfhaNfyXvhkRZDS+ASbOSB7Ts=";

    public async Task PublicarMensaje(IntegrationBaseMessage message, string topicName)
    {
        ISenderClient topicClient = new TopicClient(connectionString, topicName);

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
