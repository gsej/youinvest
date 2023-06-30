// using Azure.Messaging.ServiceBus;
// using common;
//
// namespace producer;
//
// public class StockTransactionSender : QueueSender<StockTransaction>
// {
//     private const string QueueName = "stocktransactions-queue";
//     public StockTransactionSender(ServiceBusClient serviceBusClient) : base(serviceBusClient, QueueName)
//     {
//     }
// }