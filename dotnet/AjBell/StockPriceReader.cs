using System.Text.Json;

namespace AjBell;

public class StockPriceReader : IStockPriceReader
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public IEnumerable<StockPrice> Read()
    {
        var files = new[]
        {
            "/home/gsej/repos/share-prices/price-fetcher/legacy-prices/prices-from-csv.json",
            "/home/gsej/repos/share-prices/price-fetcher/legacy-prices/prices-ci.json",
            "/home/gsej/repos/share-prices/price-fetcher/legacy-prices/prices-pi.json",
        };

        var allItems = new List<StockPrice>();
        
        foreach (var fileName in files)
        {
            var jsonString = File.ReadAllText(fileName);
            var items = JsonSerializer.Deserialize<IList<StockPrice>>(jsonString, _options);

            if (items != null)
            {
                allItems.AddRange(items);
            }
        }

        return allItems;
    }
}