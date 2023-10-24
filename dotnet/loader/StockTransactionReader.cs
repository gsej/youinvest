using System.Text.Json;
using AjBell;

namespace loader;

public class StockTransactionReader
{
    public IEnumerable<AjBellStockTransaction> Read()
    {
        var files = new[]
        {
            "/home/gsej/repos/youinvest-csv-files/gsej-sipp/transactions.json",
            "/home/gsej/repos/youinvest-csv-files/shej-sipp/transactions.json",
            "/home/gsej/repos/youinvest-csv-files/gsej-isa/transactions.json"
        };

        var allItems = new List<AjBellStockTransaction>();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        foreach (var fileName in files)
        {
            var jsonString = File.ReadAllText(fileName);
            var items = JsonSerializer.Deserialize<IList<AjBellStockTransaction>>(jsonString, options);

            allItems.AddRange(items);
        }

        return allItems;
    }
}