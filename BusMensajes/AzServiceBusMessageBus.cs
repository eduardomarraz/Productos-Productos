namespace BusMensajes;

public class AzServiceBusMessageBus: IMessageBus
{
    private string connectionString =
        "Endpoint=sb://serviciobusejemplo240129.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=sTmwADiiWbcH1e57ILymPq1EnqQu/6xea+ASbBWAoiI=";

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
