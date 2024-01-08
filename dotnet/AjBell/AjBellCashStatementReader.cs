using System.Text.Json;
namespace AjBell;

public class AjBellCashStatementReader : IAjBellCashStatementReader
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public IEnumerable<AjBellCashStatementItem> Read()
    {
        var files = new[]
        {
            "/home/gsej/repos/youinvest-csv-files/gsej-sipp/cashstatement_items.json",
            "/home/gsej/repos/youinvest-csv-files/shej-sipp/cashstatement_items.json",
            "/home/gsej/repos/youinvest-csv-files/gsej-isa/cashstatement_items.json",
            "/home/gsej/repos/youinvest-csv-files/shej-isa/cashstatement_items.json",
            "/home/gsej/repos/youinvest-csv-files/amej-sipp/cashstatement_items.json",
            "/home/gsej/repos/youinvest-csv-files/tkej-sipp/cashstatement_items.json",
        };

        var allItems = new List<AjBellCashStatementItem>();
       
        foreach (var fileName in files)
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