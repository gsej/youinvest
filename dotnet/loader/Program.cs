using AjBell;
using database;
using database.Entities;
using database.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace loader;

/// <summary>
/// loads various data files and adds to the database.
/// </summary>
class Program
{
    public static void Main(string[] args)
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
            })
            .Build();

        EnsureDatabase(host.Services);

        var cashStatementLoader = host.Services.GetRequiredService<CashStatementItemLoader>();
        cashStatementLoader.Load();

        var stockTransactionLoader = host.Services.GetRequiredService<StockTransactionLoader>();
        stockTransactionLoader.Load();

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

        var accounts = new List<Account>
        {
            new() { AccountCode = "GSEJ-SIPP" },
            new() { AccountCode = "GSEJ-ISA" },
            new() { AccountCode = "SHEJ-SIPP" },
            new() { AccountCode = "SHEJ-ISA" },
        };

        context.Accounts.AddRange(accounts);

        var stocks = new List<Stock>
        {
            new()
            {
                StockSymbol = "AZN.L",
                Description = "AstraZeneca PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "BP.L",
                Description = "BP PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "BLND.L",
                Description = "British Land Co PLC"
            },
            new()
            {
                StockSymbol = "BVIC.L",
                Description = "Britvic PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "CPI.L",
                Description = "Capita PLC",
                Aliases = new List<StockAlias>
                {
                    new()
                    {
                        Description = "CAPITA PLC ORD GBP0.02066666(NP-24/05/"
                    }
                },
                StockType = StockTypes.Share
            },
            //new Stock { Description = "CAPITA PLC ORD GBP0.02066666(NP-24/05/" },
            new()
            {
                StockSymbol = "CML.L",
                Description = "CML Microsystems PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "XWND.L",
                Description = "DB X-TRACKERS MSCI WLD TRN IDX UCITS ETF1",
                StockType = StockTypes.Etf,
                Notes = "aka db x-trackers MSCI World Information Technology TRN Index UCITS ETF, Acquired by DBK.GE"
            },
            // new Stock { Description = "DIRECT LINE INS GR ORD GBP0.10" },
            new()
            {
                StockSymbol = "DLG.L",
                Description = "Direct Line Insurance Group PLC",
                Aliases = new List<StockAlias>
                {
                    new ()
                    {
                        Description = "DIRECT LINE INS GR ORD GBP0.10"
                    }
                },
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "ECOR.L",
                Description = "Ecora Resources PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "EIG.L",
                Description = "EI GROUP PLC ORD GBP0.025",
                StockType = StockTypes.Share,
                Notes = "formerly Enterprise Inns"
            },
            new()
            {
                StockSymbol = "SONG.L",
                Description = "Hipgnosis Songs Ord",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "T26A",
                Description = "HM TREASURY GILT 0.375% (22/10/26)",
                StockType = StockTypes.Gilt
            },
            new()
            {
                StockSymbol = "HSV.L",
                Description = "HOMESERVE ORD GBP0.025",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "IBST.L",
                Description = "Ibstock PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "IDB.L",
                Description = "International Distributions Services PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "SLXX.L",
                Description = "iShares Core £ Corp Bond ETF GBP Dist",
                StockType = StockTypes.Etf
            }, 
            new()
            {
                StockSymbol = "ISF.L",
                Description = "iShares Core FTSE 100 ETF GBP Dist",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "SWDA.L",
                Isin = "IE00B4L5Y983",
                Description = "iShares Core MSCI World ETF USD Acc GBP",
                StockType = StockTypes.Etf
            },
             new()
             {
                 StockSymbol = "IGLT.L",
                 Description = "iShares Core UK Gilts ETF GBP Dist",
                 StockType = StockTypes.Etf
             },
            new()
            {
                StockSymbol = "MIDD.L",
                Description = "iShares FTSE 250 ETF GBP Dist",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "IUKP.L",
                Description = "iShares UK Property ETF GBP Dist",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "GILS.L",
                Description = "Lyxor Core UK Govt Bd (DR) ETF D GBP",
                StockType = StockTypes.Etf
            },
            // new Stock { Description = "MICRO FOCUS INT. PROVISIONAL CAP RET SHS" },
            new()
            {
                StockSymbol = "MICROFOCUS", // not the correct ticker
                Description = "MICRO FOCUS INTL ORD GBP0.10",
                Aliases = new List<StockAlias>
                {
                    new()
                    {
                        Description = "MICRO FOCUS INT. PROVISIONAL CAP RET SHS"
                    }
                },
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "MCG.L",
                Description = "Mobico Group PLC",
                Notes = "Formerly National Express",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "MRW.L",
                Description = "MORRISON(W)SUPRMKT ORD GBP0.10",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "NEX.L",
                Description = "National Express Group PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "NG.L",
                Description = "National Grid PLC",
                Aliases = new List<StockAlias>
                {
                    new()
                    {
                        Description = "NATIONAL GRID ORD GBP0.113953"
                    }
                },
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "RMG.L",
                Description = "Royal Mail PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "SMT.L",
                Description = "Scottish Mortgage Ord",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "TSCO.L",
                Description = "TESCO ORD GBP0.05",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "TET.L",
                Description = "Treatt PLC",
                StockType = StockTypes.Share
            },
            new ()
            {
                StockSymbol = "T25",
                Description = "UK(GOVT OF) 2% SNR 07/09/2025 GBP1000",
                StockType = StockTypes.Gilt
            },
            new()
            {
                StockSymbol = "VECP.L",
                Description = "Vanguard EUR Corp Bd UCITS ETF GBP",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "VMID.L",
                Description = "Vanguard FTSE 250 UCITS ETF",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "VWRP.L",
                Description = "Vanguard FTSE All-World ETF USD Acc GBP",
                Aliases = new List<StockAlias>
                {
                    new()
                    {
                        Description = "Vanguard FTSE All-World UCITS ETF" // is this correct?
                    }
                },
                StockType = StockTypes.Etf
            },
            // new () {
            // StockSymbol = "VWRP.L",
            // Description = "Vanguard FTSE All-World UCITS ETF" ,
            // StockType = StockTypes.Etf
            // },
            new()
            {
                StockSymbol = "VWRL.L",
                Description = "Vanguard FTSE All-World UCITS ETF GBP",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "VAPX.L",
                Description = "Vanguard FTSE Dev AsiaPac exJpn ETF $Dis GBP",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "VEVE.L",
                Description = "Vanguard FTSE Dev World ETF $Dis GBP",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "VFEM.L",
                Description = "Vanguard FTSE Emerg Markets ETF $Dis GBP",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "VJPN.L",
                Description = "Vanguard FTSE Japan ETF $Dis GBP",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "VUCP.L",
                Description = "Vanguard USD Corp Bd UCITS ETF GBP",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "JDW.L",
                Description = "Wetherspoon (J D) PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "WTB.L",
                Description = "Whitbread PLC",
                StockType = StockTypes.Share
            },
            new()
            {
                StockSymbol = "SGBX.L",
                Description = "WisdomTree Physical Swiss Gold ETC GBP",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "XUKX.L",
                Description = "Xtrackers FTSE 100 Income ETF 1D",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "XASX.L",
                Description = "Xtrackers MSCI UK ESG ETF 1D £",
                StockType = StockTypes.Etf
            },
            new()
            {
                StockSymbol = "XMWD.L",
                Description = "Xtrackers MSCI World Swap ETF 1C USD",
                Aliases = new List<StockAlias>
                {
                  new ()
                  {
                      Description = "XTRACKERS MSCI WORLD SWAP UCITS ETF 1" // is this the same thing??
                  }  
                },
                StockType = StockTypes.Etf
            }
            // new()
            // {
            //     StockSymbol = "",
            //     Description = "XTRACKERS MSCI WORLD SWAP UCITS ETF 1"
            // },
        };

        context.Stocks.AddRange(stocks);

        context.SaveChanges();
    }
}