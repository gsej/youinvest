using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace consumer;

public abstract class QueueSender<T> : IAsyncDisposable
{
    private readonly string _queueName;
    private ServiceBusClient _client;
    private ServiceBusSender _sender;
    
    private const string NamespaceConnectionString =
        "Endpoint=sb://youinvest-servicebus-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=QbNOrlsh2oCQc8ThulsBU0ftid5uU4xcu+ASbGPHk4Y=";

    public QueueSender(string queueName)
    {
        _queueName = queueName;
        
        var clientOptions = new ServiceBusClientOptions()
        { 
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        _client = new ServiceBusClient(NamespaceConnectionString, clientOptions);
        _sender = _client.CreateSender(_queueName);
    }
    
    public async Task Send(T transaction)
    {
        using ServiceBusMessageBatch messageBatch = await _sender.CreateMessageBatchAsync();

        var body = JsonSerializer.Serialize(transaction);
        // try adding a message to the batch
        if (!messageBatch.TryAddMessage(new ServiceBusMessage(body)))
        {
            // if it is too large for the batch
            throw new Exception($"The message is too large to fit in the batch.");
        }
        
        try
        {
            // Use the producer client to send the batch of messages to the Service Bus queue
            await _sender.SendMessagesAsync(messageBatch);
            Console.WriteLine($"A message has been published to the queue.");
        }
        catch (Exception e)
        {
            Console.WriteLine("an exception occurred: " + e.Message);
        }
    }

    
    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
        await _client.DisposeAsync();
    }
}