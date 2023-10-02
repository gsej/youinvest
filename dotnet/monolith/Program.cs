using database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace monolith;

/// <summary>
/// Will consume messages and summarize dividends
/// </summary>
class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            //     .ConfigureAppConfiguration((config) =>
            //     {
            //         config.AddJsonFile("appsettings.json");
            //         config.AddUserSecrets<Program>();
            //         config.AddEnvironmentVariables();
            //         config.Build();
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<InvestmentsDbContext>(
                    opts => opts.UseSqlServer(
                        "Server=localhost;Initial Catalog=investments;Persist Security Info=False;User ID=sa;Password=Password123!;MultipleActiveResultSets=False;Encrypt=True;Trust Server Certificate=True;Connection Timeout=30;")
                );
            })
            .Build();


        EnsureDatabase(builder.Services);
     

        //EnsureDatabase();
      //  var processor = new ProducerService();
      //  processor.Process();

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

     private static void EnsureDatabase(IServiceProvider services)
     {
         var context = services.GetRequiredService<InvestmentsDbContext>();
         context.Database.EnsureCreated();
         context.Database.Migrate();

    //     using (var scope = app.Services.CreateScope())
    //     {
    //         var services = scope.ServiceProvider;
    //
    //         var context = services.GetRequiredService<MyContext>();    
    //         context.Database.Migrate();
    //     }
     }
}