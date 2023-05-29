using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace producer;

public abstract class QueueSender<T> : IAsyncDisposable
{
    private readonly string _queueName;
    private ServiceBusClient _client;
    private ServiceBusSender _sender;
    
    private const string NamespaceConnectionString =
        "Endpoint=sb://youinvest-servicebus-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=dA1+GzrFPZcGJr6wfPIfV/OPxE9lXUPgQ+ASbNCaC8c=";

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
        var body = JsonSerializer.Serialize(transaction);
        var message = new ServiceBusMessage(body);
               
        try
        {
            await _sender.SendMessageAsync(message);
            Console.WriteLine($"A message has been published to the queue {_queueName}");
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