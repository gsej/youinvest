using System.Text.Json;

namespace AjBell;

public class AjBellStockTransactionReader : IAjBellStockTransactionReader
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public IEnumerable<AjBellStockTransaction> Read()
    {
        var files = new[]
        {
            "/home/gsej/repos/youinvest-csv-files/gsej-sipp/transactions.json",
            "/home/gsej/repos/youinvest-csv-files/shej-sipp/transactions.json",
            "/home/gsej/repos/youinvest-csv-files/gsej-isa/transactions.json",
            "/home/gsej/repos/youinvest-csv-files/shej-isa/transactions.json",
            "/home/gsej/repos/youinvest-csv-files/gold/transactions.json"
        };

        var allItems = new List<AjBellStockTransaction>();
        
        foreach (var fileName in files)
        {
            var jsonString = File.ReadAllText(fileName);
            var items = JsonSerializer.Deserialize<IList<AjBellStockTransaction>>(jsonString, _options);

            if (items != null)
            {
                allItems.AddRange(items);
            }
        }

        return allItems;
    }
}