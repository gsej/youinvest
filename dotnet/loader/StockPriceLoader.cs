using System.Globalization;
using AjBell;
using database;
using Microsoft.EntityFrameworkCore;
using StockPrice = database.Entities.StockPrice;

namespace loader;

public class StockPriceLoader
{
    private readonly InvestmentsDbContext _context;
    private readonly IStockPriceReader _reader;

    public StockPriceLoader(InvestmentsDbContext context, IStockPriceReader reader)
    {
        _context = context;
        _reader = reader;
    }

    public async Task LoadFile(string fileName, string source, string defaultStockSymbol = null)
    {
        // This is to load a file of stock prices, and attribute those prices to a source.
        var stocks = _context
            .Stocks
            .Include(stock => stock.Aliases)
            .Include(stock => stock.AlternativeSymbols)
            .ToList();

        var stockPrices = _reader.Read(fileName).ToList();

        foreach (var stockPriceDto in stockPrices)
        {
            decimal price;

            var priceParsable = decimal.TryParse(stockPriceDto.Price, null, out price);

            if (!priceParsable)
            {
                continue;
            }

            var stockSymbol = stockPriceDto.StockSymbol ?? defaultStockSymbol;

            var matchingStock = stocks.SingleOrDefault(s =>
                s.StockSymbol.Equals(stockSymbol, StringComparison.InvariantCultureIgnoreCase) ||
                s.AlternativeSymbols.Any(a =>
                    a.Alternative.Equals(stockSymbol, StringComparison.InvariantCultureIgnoreCase)));

            var symbol = matchingStock != null ? matchingStock.StockSymbol : stockSymbol;

             // deduplication
            var existing = await _context.StockPrices.SingleOrDefaultAsync(s =>
                s.Date == stockPriceDto.Date && s.StockSymbol == symbol);

            if (existing != null)
            {
                if (existing.Price != price)
                {
                    Console.WriteLine($"discrepancy with prices for {symbol} on {stockPriceDto.Date}");
                    throw new InvalidOperationException();
                }
            }
            if (existing == null)
            {
                var stockPrice = new StockPrice(
                    stockSymbol: symbol,
                    date: stockPriceDto.Date,
                    price: price,
                    currency: "GBP", 
                    source: source);

                _context.StockPrices.Add(stockPrice);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task LoadFiles(string directoryName, string source, string defaultStockSymbol = null)
    {
        var fileNames = Directory.GetFiles(directoryName, "*.json");

        foreach (var fileName in fileNames)
        {
            await LoadFile(fileName, source, defaultStockSymbol);
        }
    }

    public async Task Load()
    {
        var stocks = _context
            .Stocks
            .Include(stock => stock.Aliases)
            .Include(stock => stock.AlternativeSymbols)
            .ToList();

        var stockPrices = _reader.Read().ToList();

        foreach (var stockPriceDto in stockPrices)
        {
            decimal price;

            var priceParsable = decimal.TryParse(stockPriceDto.Price, null, out price);

            if (!priceParsable)
            {
                continue;
            }

            if (stockPriceDto == null)
            {
                Console.WriteLine("kjshkj");
            }

            if (stockPriceDto.StockSymbol == null)
            {
                Console.WriteLine("kjshkj");
            }

            if (stockPriceDto.StockSymbol.Equals("GB00BNNGP668.SG"))
            {
                continue;
            }

            var matchingStock = stocks.SingleOrDefault(s =>
                s.StockSymbol.Equals(stockPriceDto.StockSymbol, StringComparison.InvariantCultureIgnoreCase) ||
                s.AlternativeSymbols.Any(a =>
                    a.Alternative.Equals(stockPriceDto.StockSymbol, StringComparison.InvariantCultureIgnoreCase)));

            var symbol = matchingStock != null ? matchingStock.StockSymbol : stockPriceDto.StockSymbol;

            // StockPrice existing = null; // disable deduplication
            var existing = await _context.StockPrices.SingleOrDefaultAsync(s =>
                s.Date == stockPriceDto.Date && s.StockSymbol == symbol);

            // if (existing != null)
            // {
            //     if (existing.Price != stockPriceDto.Price)
            //     {
            //         throw new InvalidOperationException();
            //     }
            // }
            if (existing == null)
            {
                var stockPrice = new StockPrice(
                    stockSymbol: symbol,
                    date: stockPriceDto.Date,
                    price: price,
                    currency: "GBP", source: "unknown");

                _context.StockPrices.Add(stockPrice);
                await _context.SaveChangesAsync();

                // TODO: check for different price
            }
        }
    }
}