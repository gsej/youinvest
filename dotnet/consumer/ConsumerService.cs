// using Microsoft.Extensions.Hosting;
//
// namespace consumer;
//
// public class ConsumerService : IHostedService
// {
//     private readonly IHostApplicationLifetime _lifetime;
//     private readonly CashStatementItemProcessor _cashStatementItemProcessor;
//     private readonly StockTransactionProcessor _stockTransactionsProcessor;
//   
//     public ConsumerService(IHostApplicationLifetime lifetime, CashStatementItemProcessor cashStatementItemProcessor, StockTransactionProcessor stockTransactionsProcessor)
//     {
//         _lifetime = lifetime;
//         _cashStatementItemProcessor = cashStatementItemProcessor;
//         _stockTransactionsProcessor = stockTransactionsProcessor;
//     }
//     
//     public async Task StartAsync(CancellationToken cancellationToken)
//     {
//         Console.WriteLine("starting the service");
//         
//     //    await _cashStatementItemProcessor.Process();
//         await _stockTransactionsProcessor.Process();
//
//         _lifetime.StopApplication();
//     }
//
//     public Task StopAsync(CancellationToken cancellationToken)
//     {
//         Console.WriteLine("stopping the service");
//         return Task.CompletedTask;
//     }
// }