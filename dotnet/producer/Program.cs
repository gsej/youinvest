using System.Text.Json;
using common;

namespace producer;

static class Program
{
    /// <summary>
    /// Reads the json files and sends the items to the relevant 
    /// service bus queue
    /// </summary>
    static async Task Main(string[] args)
    {
        await SendItems<TransactionSender, StockTransaction>(
            "/home/gsej/repos/youinvest-csv-files/gsej-sipp/transactions.json",
            "/home/gsej/repos/youinvest-csv-files/gsej-isa/transactions.json");

        await SendItems<CashStatementSender, CashStatementItem>(   
            "/home/gsej/repos/youinvest-csv-files/gsej-sipp/cashstatement_items.json",
            "/home/gsej/repos/youinvest-csv-files/gsej-isa/cashstatement_items.json");
    }
    
    private static async Task SendItems<TSender, TItem>(params string[] sourceFiles) where TSender : QueueSender<TItem>, new()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };   
        
        var sender = new TSender();
        
        foreach (var fileName in sourceFiles)
        {
            var jsonString = File.ReadAllText(fileName);
            var items = JsonSerializer.Deserialize<IList<TItem>>(jsonString, options);
            foreach (var item in items)
            {
                await sender.Send(item);
            }
        }
    }
}