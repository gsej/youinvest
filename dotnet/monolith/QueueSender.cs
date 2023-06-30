// using System.Text.Json;
// using System.Threading.Channels;
// using Azure.Messaging.ServiceBus;
//
// namespace producer;
//
// public abstract class QueueSender<T> : IAsyncDisposable, ISender<T>
// {
//     private readonly string _queueName;
//     private readonly ServiceBusSender _sender;
//
//     private readonly Channel<string> _channel;
//
//     protected QueueSender(Channel<string> channel)
//     {
//         _channel = channel;        
//     }
//     
//     public async Task Send(T item)
//     {
//         var body = JsonSerializer.Serialize(transaction);
//         var message = new ServiceBusMessage(body);
//                
//         try
//         {
//             await _sender.SendMessageAsync(message);
//             Console.WriteLine($"A message has been published to the queue {_queueName}");
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine("an exception occurred: " + e.Message);
//         }
//     }
//
//     public async ValueTask DisposeAsync()
//     {
//         await _sender.DisposeAsync();
//     }
// }