using AjBell;
using database;
using database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace loader;

/// <summary>
/// loads various data files and adds to the database.
/// </summary>
class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<InvestmentsDbContext>(
                    opts => opts.UseSqlServer(
                        "Server=localhost;Initial Catalog=investments;Persist Security Info=False;User ID=sa;Password=Password123!;MultipleActiveResultSets=False;Encrypt=True;Trust Server Certificate=True;Connection Timeout=30;")
                );

                services.AddTransient<IAjBellCashStatementReader, AjBellCashStatementReader>();
                services.AddTransient<CashStatementItemLoader>();

                services.AddTransient<IAjBellStockTransactionReader, AjBellStockTransactionReader>();
                services.AddTransient<StockTransactionLoader>();

                services.AddTransient<IStockPriceReader, StockPriceReader>();
                services.AddTransient<StockPriceLoader>();
                
                services.AddTransient<IKnownValueReader, KnownValueReader>();
                services.AddTransient<KnownValueLoader>();
            })
            .Build();

        EnsureDatabase(host.Services);

         var cashStatementLoader = host.Services.GetRequiredService<CashStatementItemLoader>();
         await cashStatementLoader.Load();
      
         var stockTransactionLoader = host.Services.GetRequiredService<StockTransactionLoader>();
         await stockTransactionLoader.Load();

        var stockPriceLoader = host.Services.GetRequiredService<StockPriceLoader>();
        
        await stockPriceLoader.LoadFile("/home/gsej/repos/share-prices/price-fetcher/legacy-prices/prices-ci.json", "LegacyCI");
        await stockPriceLoader.LoadFile("/home/gsej/repos/share-prices/price-fetcher/legacy-prices/prices-pi.json", "LegacyPI");

        await stockPriceLoader.LoadFiles("/home/gsej/repos/share-prices/price-fetcher/prices/prices-pi", "PricesPI");
        await stockPriceLoader.LoadFiles("/home/gsej/repos/share-prices/price-fetcher/prices/prices-ci", "PricesCI");
        
        await stockPriceLoader.LoadFile("/home/gsej/repos/share-prices/historical/gold.json", "Download", "GOLD");

      //  var knownBalancesLoader = host.Services.GetRequiredService<KnownValueLoader>();
      //  await knownBalancesLoader.LoadFile("/home/gsej/repos/youinvest-csv-files/known-values/known-values.json");
    }

    /// <summary>
    /// Creates database (using migrations)
    /// Loads some static data (e.g. stocks)
    /// </summary>
    /// <param name="services"></param>
    private static void EnsureDatabase(IServiceProvider services)
    {
        var context = services.GetRequiredService<InvestmentsDbContext>();
        context.Database.EnsureDeleted();
        context.Database.Migrate();

        var accounts = StaticData.Accounts();
        context.Accounts.AddRange(accounts);

        var stocks = StaticData.Stocks();
        context.Stocks.AddRange(stocks);

        context.SaveChanges();
    }
}