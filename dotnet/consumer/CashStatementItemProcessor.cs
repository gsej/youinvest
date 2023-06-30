// using System.Text.Json;
// using Azure.Messaging.ServiceBus;
// using consumer.Database;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
//
// namespace consumer;
//
// public class CashStatementItemProcessor
// {
//     private readonly IHostApplicationLifetime _lifetime;
//     private readonly ILogger<CashStatementItemProcessor> _logger;
//     private readonly ConsumerDbContext _dbContext;
//     private readonly IList<ICashStatementItemEnricher> _enrichers; 
//     private readonly ServiceBusProcessor _processor;
//     private DateTime _lastMessageProcessedTime;
//
//     public CashStatementItemProcessor(IHostApplicationLifetime lifetime, ILogger<CashStatementItemProcessor> logger, ConsumerDbContext dbContext, ServiceBusClient serviceBusClient, QueueNames queueNames)
//     {
//         _lifetime = lifetime;
//         _logger = logger;
//         _dbContext = dbContext;
//         
//         _enrichers = new List<ICashStatementItemEnricher>();
//         _enrichers.Add(new CashStatementItemTypeEnricher());
//           
//         _processor = serviceBusClient.CreateProcessor(queueNames.CashStatementItemsQueue, new ServiceBusProcessorOptions());
//     }
//
//     public async Task Process()
//     {
//         try
//         {
//             _lastMessageProcessedTime = DateTime.UtcNow;
//             
//             // add handler to process messages
//             _processor.ProcessMessageAsync += MessageHandler;
//
//             // add handler to process any errors
//             _processor.ProcessErrorAsync += ErrorHandler;
//
//             // start processing 
//             await _processor.StartProcessingAsync();
//
//             while (true)
//             {
//                 TimeSpan ts = DateTime.UtcNow - _lastMessageProcessedTime;
//                 
//                 _logger.LogInformation($"time since last message: {ts.TotalSeconds} second");
//
//                 if (ts.TotalSeconds > 10)
//                 {
//                     Console.WriteLine("\nStopping the receiver...");
//                     await _processor.StopProcessingAsync();
//                     Console.WriteLine("Stopped receiving messages");
//                     break;
//                 }
//                 
//                 Thread.Sleep(1000);
//             }
//             
//             
//             // Console.WriteLine("Wait for a minute and then press any key to end the processing");
//             // Console.ReadKey();
//             //
//             // // stop processing 
//             // Console.WriteLine("\nStopping the receiver...");
//             // await _processor.StopProcessingAsync();
//             // Console.WriteLine("Stopped receiving messages");
//         }
//         finally
//         {
//             // Calling DisposeAsync on client types is required to ensure that network
//             // resources and other unmanaged objects are properly cleaned up.
//             await _processor.DisposeAsync();
//      //       await _serviceBusClient.DisposeAsync();
//         }
//     }
//
//     async Task MessageHandler(ProcessMessageEventArgs args)
//     {
//         try
//         {
//             string body = args.Message.Body.ToString();
//             Console.WriteLine($"Received: {body}");
//
//             var item = JsonSerializer.Deserialize<CashStatementItemRaw>(body);
//             
//             var cashStatementItem = MapCashStatementItem(item);
//    
//
//             foreach (var enricher in _enrichers)
//             {
//                 enricher.Enrich(cashStatementItem);
//             }
//
//             // save the item....
//             await SaveCashStatementItem(cashStatementItem);
//
//             _lastMessageProcessedTime = DateTime.Now;
//
//             // if (item.IsDividend)
//             // {
//             //     var year = item.Year;
//             //     var account = item.Account;
//             //
//             //     var existingDividend =
//             //         await _dbContext.Dividends.SingleOrDefaultAsync(d => d.Year == year && d.Account == account);
//             //
//             //     if (existingDividend != null)
//             //     {
//             //         existingDividend.TotalReceived += item.Receipt_Amount_Gbp;
//             //     }
//             //     else
//             //     {
//             //         var newDividend = new Dividends
//             //         {
//             //             Account = item.Account,
//             //             Year = item.Year,
//             //             TotalReceived = item.Receipt_Amount_Gbp
//             //         };
//             //         _dbContext.Dividends.Add(newDividend);
//             //     }
//             //
//             //     await _dbContext.SaveChangesAsync();
//             //
//             //
//             // }
//             // else if (item.IsContribution)
//             // {
//             //     var year = item.Year;
//             //     var account = item.Account;
//             //
//             //     var existingContribution =
//             //         await _dbContext.Contributions.SingleOrDefaultAsync(d => d.Year == year && d.Account == account);
//             //
//             //     if (existingContribution != null)
//             //     {
//             //         existingContribution.TotalReceived += item.Receipt_Amount_Gbp;
//             //         existingContribution.TotalReceived += item.Payment_Amount_Gbp;
//             //     }
//             //     else
//             //     {
//             //         var newContribution = new Contributions()
//             //         {
//             //             Account = item.Account,
//             //             Year = item.Year,
//             //             TotalReceived = item.Receipt_Amount_Gbp + item.Payment_Amount_Gbp
//             //         };
//             //         _dbContext.Contributions.Add(newContribution);
//             //     }
//             //
//             //     await _dbContext.SaveChangesAsync();
//             // }
//         }
//         catch (Exception exception)
//         {
//             _logger.LogError(exception, "error processing message");
//             _lifetime.StopApplication();
//         }
//
//         // complete the message. message is deleted from the queue. 
//         await args.CompleteMessageAsync(args.Message);
//     }
//
//     private Entities.CashStatementItem MapCashStatementItem(common.CashStatementItemRaw item)
//     {
//         var dbItem = new Entities.CashStatementItem
//         {
//             Account = item.Account,
//             Date = item.Date,
//             Description = item.Description,
//             PaymentAmountGbp = item.Payment_Amount_Gbp,
//             ReceiptAmountGbp = item.Receipt_Amount_Gbp
//         };
//
//         return dbItem;
//     }
//
//     private async Task SaveCashStatementItem(Entities.CashStatementItem cashStatementItem)
//     {
//         _dbContext.CashStatementItems.Add(cashStatementItem);
//         await _dbContext.SaveChangesAsync();
//     }
//
//     // handle any errors when receiving messages
//     static Task ErrorHandler(ProcessErrorEventArgs args)
//     {
//         Console.WriteLine(args.Exception.ToString());
//         return Task.CompletedTask;
//     }
// }