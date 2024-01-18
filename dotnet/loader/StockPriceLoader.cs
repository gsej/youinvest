using System.Globalization;
using AjBell;
using database;
using database.Entities;
using Microsoft.EntityFrameworkCore;
using StockPrice = database.Entities.StockPrice;

namespace loader;

public class StockPriceLoader
{
    private readonly InvestmentsDbContext _context;
    private readonly IStockPriceReader _reader;

    private readonly Lazy<List<Stock>> _stocks;

    public StockPriceLoader(InvestmentsDbContext context, IStockPriceReader reader)
    {
        _context = context;
        _reader = reader;

        _stocks = new(() =>
            _context
                .Stocks
                .Include(stock => stock.Aliases)
                .Include(stock => stock.AlternativeSymbols)
                .ToList());
    }

    public async Task LoadFile(string fileName, string source, string defaultStockSymbol = null)
    {
        await LoadFileInternal(fileName, source, defaultStockSymbol);
    }

    public async Task LoadFiles(string directoryName, string source, string defaultStockSymbol = null)
    {
        var fileNames = Directory.GetFiles(directoryName, "*.json");

        foreach (var fileName in fileNames)
        {
            await LoadFile(fileName, source, defaultStockSymbol);
        }
    }

    private async Task LoadFileInternal(string fileName, string source, string defaultStockSymbol = null)
    {
        var stockPrices = _reader.Read(fileName).ToList();

        foreach (var stockPriceDto in stockPrices)
        {
            var priceParsable = decimal.TryParse(stockPriceDto.Price, null, out var price);

            if (!priceParsable)
            {
                continue;
            }

            var stockSymbol = stockPriceDto.StockSymbol ?? defaultStockSymbol;

            var matchingStock = _stocks.Value.SingleOrDefault(s =>
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
                var currency = string.IsNullOrEmpty(stockPriceDto.Currency)
                    ? matchingStock?.DefaultCurrency
                    : stockPriceDto.Currency;
                
                var stockPrice = new StockPrice(
                    stockSymbol: symbol,
                    date: stockPriceDto.Date,
                    price: price,
                    currency: currency, 
                    source: source);

                _context.StockPrices.Add(stockPrice);
                await _context.SaveChangesAsync();
            }
        }
    }
}
