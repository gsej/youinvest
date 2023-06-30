namespace monolith;

/// <summary>
/// Will consume messages and summarize dividends
/// </summary>
class Program
{
    public static void Main(string[] args)
    {
        var processor = new ProducerService();
        processor.Process();

        // var host = Host.CreateDefaultBuilder(args)
        //     .ConfigureAppConfiguration((config) =>
        //     {
        //         config.AddJsonFile("appsettings.json");
        //         config.AddUserSecrets<Program>();
        //         config.AddEnvironmentVariables();
        //         config.Build();
        //     })

        //     .ConfigureServices((context, services) =>
        //     {
        //         services.AddSingleton<IHostedService, ProducerService>();           


        //         services.AddSingleton<CashStatementSender>();
        //         services.AddSingleton<StockTransactionSender>();

        //     })
        //     .Build();

        // await host.RunAsync();
    }
}