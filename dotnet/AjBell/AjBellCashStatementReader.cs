using System.Text.Json;

namespace Incoming;

public class AjBellCashStatementReader
{
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
    
    public IEnumerable<AjBellCashStatementItem> Read()
    {
        var cashStatementFiles = new[]
        {
            "/home/gsej/repos/youinvest-csv-files/gsej-sipp/cashstatement_items.json",
            "/home/gsej/repos/youinvest-csv-files/shej-sipp/cashstatement_items.json",
            "/home/gsej/repos/youinvest-csv-files/gsej-isa/cashstatement_items.json"
        };

        var allItems = new List<AjBellCashStatementItem>();
       
        foreach (var fileName in cashStatementFiles)
        {
            var jsonString = File.ReadAllText(fileName);
            var items = JsonSerializer.Deserialize<IList<AjBellCashStatementItem>>(jsonString, _options);

            if (items != null)
            {
                allItems.AddRange(items);
            }
        }

        return allItems;
    }
}