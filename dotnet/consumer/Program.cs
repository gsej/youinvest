// using Azure.Messaging.ServiceBus;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Azure;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using common;
// using consumer.Database;
// using consumer.StockTransactionEnrichers;
// using Microsoft.Azure.Amqp.Framing;
//
// namespace consumer;
//
// /// <summary>
// /// Will consume messages and summarize dividends
// /// </summary>
// class Program
// {
//     static async Task Main(string[] args)
//     {
//         var host = Host.CreateDefaultBuilder(args)
//             .ConfigureAppConfiguration((config) =>
//             {
//                 config.AddJsonFile("appsettings.json");
//                 config.AddUserSecrets<Program>();
//                 config.AddEnvironmentVariables();
//                 config.Build();
//             })
//
//             .ConfigureServices((context, services) =>
//             {
//                 var connectionString = context.Configuration.GetString("youinvest-db-connectionstring");
//                 services.AddDbContext<ConsumerDbContext>(options => options.UseSqlServer(connectionString));
//                 services.AddSingleton<IHostedService, ConsumerService>();
//
//                 services.AddSingleton<StockTransactionStampDutyEnricher>();
//
//                 var queueNames = new QueueNames
//                 {
//                     CashStatementItemsQueue = context.Configuration.GetString("cashstatement-items-queue"),
//                     StockTransactionsQueue = context.Configuration.GetString("stocktransactions-queue"),
//                 };
//
//                 services.AddSingleton(queueNames);
//                 
//                 services.AddAzureClients(clientsBuilder =>
//                 {
//                     clientsBuilder.AddServiceBusClient(context.Configuration.GetString("namespace-connectionstring"))
//                         .ConfigureOptions(options =>
//                         {
//                             options.TransportType = ServiceBusTransportType.AmqpWebSockets;
//                             options.RetryOptions.Delay = TimeSpan.FromMilliseconds(50);
//                             options.RetryOptions.MaxDelay = TimeSpan.FromSeconds(5);
//                             options.RetryOptions.MaxRetries = 3;
//                         });
//                 });
//
//                 services.AddScoped<DataLoader>();
//
//                 services.AddSingleton<CashStatementItemProcessor>();
//                 services.AddSingleton<StockTransactionProcessor>();
//
//             })
//             .Build();
//         
//         using (var scope = host.Services.CreateScope())
//         {
//             var services = scope.ServiceProvider;
//
//             var context = services.GetRequiredService<ConsumerDbContext>();
//             await context.Database.MigrateAsync();
//
//             var loader = services.GetRequiredService<DataLoader>();
//             await loader.LoadStocks();
//         }
//
//         await host.RunAsync();
//     }
// }