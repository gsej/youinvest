// using System.Text.Json;
// using Azure.Messaging.ServiceBus;
// using consumer.Entities;
// using Microsoft.EntityFrameworkCore;
// using CashStatementItem = common.CashStatementItem;
//
// namespace consumer;
//
// public class CashStatementItemProcessor
// {
//
//     private readonly string _namespaceConnectionString;
//     private readonly string _queueName;
//
//
//     private readonly ConsumerDbContext _context;
//
//     private ServiceBusProcessor _processor;
//
//     private ServiceBusClient _client;
//
//     public CashStatementItemProcessor(string namespaceConnectionString, string queueName,
//         string databaseConnectionString)
//     {
//         _queueName = queueName;
//         _namespaceConnectionString = namespaceConnectionString;
//
//         var optionsBuilder = new DbContextOptionsBuilder<ConsumerDbContext>();
//         optionsBuilder.UseSqlServer(databaseConnectionString);
//         _context = new ConsumerDbContext(optionsBuilder.Options);
//
//         var clientOptions = new ServiceBusClientOptions()
//         {
//             TransportType = ServiceBusTransportType.AmqpWebSockets
//         };
//         _client = new ServiceBusClient(_namespaceConnectionString, clientOptions);
//
//         _processor = _client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());
//     }
//
//     public async Task Process()
//     {
//
//         try
//         {
//             // add handler to process messages
//             _processor.ProcessMessageAsync += MessageHandler;
//
//             // add handler to process any errors
//             _processor.ProcessErrorAsync += ErrorHandler;
//
//             // start processing 
//             await _processor.StartProcessingAsync();
//
//             Console.WriteLine("Wait for a minute and then press any key to end the processing");
//             Console.ReadKey();
//
//             // stop processing 
//             Console.WriteLine("\nStopping the receiver...");
//             await _processor.StopProcessingAsync();
//             Console.WriteLine("Stopped receiving messages");
//         }
//         finally
//         {
//             // Calling DisposeAsync on client types is required to ensure that network
//             // resources and other unmanaged objects are properly cleaned up.
//             await _processor.DisposeAsync();
//             await _client.DisposeAsync();
//         }
//     }
//
//     async Task MessageHandler(ProcessMessageEventArgs args)
//     {
//         string body = args.Message.Body.ToString();
//         Console.WriteLine($"Received: {body}");
//
//         var item = JsonSerializer.Deserialize<CashStatementItem>(body);
//
//         // save the item....
//         await SaveCashStatementItem(item);
//
//         if (item.IsDividend)
//         {
//             var year = item.Year;
//             var account = item.Account;
//             
//             var existingDividend = await _context.Dividends.SingleOrDefaultAsync(d => d.Year == year && d.Account == account);
//
//             if (existingDividend != null)
//             {
//                 existingDividend.TotalReceived += item.Receipt_Amount_Gbp;
//             }
//             else
//             {
//                 var newDividend = new Dividends
//                 {
//                     Account = item.Account,
//                     Year = item.Year,
//                     TotalReceived = item.Receipt_Amount_Gbp
//                 };
//                 _context.Dividends.Add(newDividend);
//             }
//
//             await _context.SaveChangesAsync();
//
//         }
//         else if (item.IsContribution)
//         {
//             var year = item.Year;
//             var account = item.Account;
//             
//             var existingContribution = await _context.Contributions.SingleOrDefaultAsync(d => d.Year == year && d.Account == account);
//
//             if (existingContribution != null)
//             {
//                 existingContribution.TotalReceived += item.Receipt_Amount_Gbp;
//                 existingContribution.TotalReceived += item.Payment_Amount_Gbp;
//             }
//             else
//             {
//                 var newContribution = new Contributions()
//                 {
//                     Account = item.Account,
//                     Year = item.Year,
//                     TotalReceived = item.Receipt_Amount_Gbp + item.Payment_Amount_Gbp
//                 };
//                 _context.Contributions.Add(newContribution);
//             }
//
//             await _context.SaveChangesAsync();
//         }
//
//
//         // complete the message. message is deleted from the queue. 
//         await args.CompleteMessageAsync(args.Message);
//     }
//
//     private async Task SaveCashStatementItem(CashStatementItem item)
//     {
//         var dbItem = new database.Entities.CashStatementItem
//         {
//             Account = item.Account,
//             Date = item.Date,
//             Description = item.Description,
//             PaymentAmountGbp = item.Payment_Amount_Gbp,
//             ReceiptAmountGbp = item.Receipt_Amount_Gbp
//         };
//
//         _context.CashStatementItems.Add(dbItem);
//         await _context.SaveChangesAsync();
//     }
//
//     // handle any errors when receiving messages
//     static Task ErrorHandler(ProcessErrorEventArgs args)
//     {
//         Console.WriteLine(args.Exception.ToString());
//         return Task.CompletedTask;
//     }
// }