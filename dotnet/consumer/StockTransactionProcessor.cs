//  using System.Text.Json;
// using Azure.Messaging.ServiceBus;
// using consumer.CashStatementItemEnrichers;
//  using consumer.Database;
//  using consumer.StockTransactionEnrichers;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
//
// namespace consumer;
//
// public class StockTransactionProcessor
// {
//     private readonly IHostApplicationLifetime _lifetime;
//     private readonly ILogger<StockTransactionProcessor> _logger;
//     private readonly ConsumerDbContext _dbContext;
//
//     private readonly IList<IStockTransactionEnricher> _enrichers;
//     private readonly ServiceBusProcessor _processor;
//     private DateTime _lastMessageProcessedTime;
//
//     public StockTransactionProcessor(IHostApplicationLifetime lifetime,
//         ILogger<StockTransactionProcessor> logger,
//         ConsumerDbContext dbContext,
//         ServiceBusClient serviceBusClient,
//         QueueNames queueNames,
//         StockTransactionStampDutyEnricher stampDutyEnricher)
//     {
//         _lifetime = lifetime;
//         _logger = logger;
//         _dbContext = dbContext;
//         
//         _enrichers = new List<IStockTransactionEnricher>();
//         _enrichers.Add(new StockTransactionTypeEnricher());
//         _enrichers.Add(new StockTransactionFeeEnricher());
//         _enrichers.Add(stampDutyEnricher);
//         
//         _processor = serviceBusClient.CreateProcessor(queueNames.StockTransactionsQueue, new ServiceBusProcessorOptions());
//     }
//     public async Task Process()
//     { try
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
//         }
//         finally
//         {
//             // Calling DisposeAsync on client types is required to ensure that network
//             // resources and other unmanaged objects are properly cleaned up.
//             await _processor.DisposeAsync(); ;
//         }
//     }
//
//     async Task MessageHandler(ProcessMessageEventArgs args)
//     {
//         string body = args.Message.Body.ToString();
//         Console.WriteLine($"Received: {body}");
//
//         var item = JsonSerializer.Deserialize<common.StockTransaction>(body);
//
//         var stockTransaction = MapStockTransaction(item);
//        
//         foreach (var enricher in _enrichers)
//         {
//             enricher.Enrich(stockTransaction);
//         }
//
//         // save the item....
//         _dbContext.StockTransactions.Add(stockTransaction);
//         await _dbContext.SaveChangesAsync();
//         
//         _lastMessageProcessedTime = DateTime.UtcNow;
//
//         // if (item.IsDividend)
//         // {
//         //     var year = item.Year;
//         //     var account = item.Account;
//         //     
//         //     var existingDividend = await _context.Dividends.SingleOrDefaultAsync(d => d.Year == year && d.Account == account);
//         //
//         //     if (existingDividend != null)
//         //     {
//         //         existingDividend.TotalReceived += item.Receipt_Amount_Gbp;
//         //     }
//         //     else
//         //     {
//         //         var newDividend = new Dividends
//         //         {
//         //             Account = item.Account,
//         //             Year = item.Year,
//         //             TotalReceived = item.Receipt_Amount_Gbp
//         //         };
//         //         _context.Dividends.Add(newDividend);
//         //     }
//         //
//         //     await _context.SaveChangesAsync();
//         //
//         // }
//         // else if (item.IsContribution)
//         // {
//             // var year = item.Year;
//             // var account = item.Account;
//             //
//             // var existingContribution = await _context.Contributions.SingleOrDefaultAsync(d => d.Year == year && d.Account == account);
//             //
//             // if (existingContribution != null)
//             // {
//             //     existingContribution.TotalReceived += item.Receipt_Amount_Gbp;
//             //     existingContribution.TotalReceived += item.Payment_Amount_Gbp;
//             // }
//             // else
//             // {
//                 // var newContribution = new Contributions()
//                 // {
//                 //     Account = item.Account,
//                 //     Year = item.Year,
//                 //     TotalReceived = item.Receipt_Amount_Gbp + item.Payment_Amount_Gbp
//                 // };
//                 // _context.Contributions.Add(newContribution);
//        //     }
//
//        
//      //   }
//
//
//         // complete the message. message is deleted from the queue. 
//         await args.CompleteMessageAsync(args.Message);
//     }
//
//     private Entities.StockTransaction MapStockTransaction(common.StockTransaction item)
//     {
//         var dbItem = new Entities.StockTransaction()
//         {
//
//             Account = item.Account,
//             Date = item.Date,
//             Transaction = item.Transaction,
//             Description = item.Description,
//             Quantity = item.Quantity,
//             AmountGbp = item.Amount_Gbp,
//             Reference = item.Reference
//         };
//         return dbItem;
//     }
//
//
//     // handle any errors when receiving messages
//      Task ErrorHandler(ProcessErrorEventArgs args)
//     {
//         _logger.LogError(args.Exception.ToString());
//         _lifetime.StopApplication();
//         return Task.CompletedTask;
//     }
// }