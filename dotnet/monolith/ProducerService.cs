using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using database.Entities;
using monolith.CashStatementItemEnrichers;
using Incoming;
using monolith.Entities;

namespace monolith;

public class ProducerService //: IHostedService
{
    // private readonly IHostApplicationLifetime _lifetime;
    // private readonly CashStatementSender _cashStatementSender;
    // private readonly StockTransactionSender _stockTransactionSender;
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public void Process()
    {
        var cashStatementReader = new AjBellCashStatementReader();
        var ajBellCashStatementItems = cashStatementReader.Read().ToList();

        var cashStatementItemTypeEnricher = new CashStatementItemTypeEnricher();
        var cashStatementItems = new List<CashStatementItem>(ajBellCashStatementItems.Count);

        foreach (var ajBellCashStatementItem in ajBellCashStatementItems)
        {
            var cashStatementItem = new CashStatementItem
            {
                Account = ajBellCashStatementItem.Account,
                Date = ajBellCashStatementItem.Date,
                Description = ajBellCashStatementItem.Description,
                PaymentAmountGbp = ajBellCashStatementItem.Payment_Amount_Gbp,
                ReceiptAmountGbp = ajBellCashStatementItem.Receipt_Amount_Gbp
            };

            cashStatementItemTypeEnricher.Enrich(cashStatementItem);
            cashStatementItems.Add(cashStatementItem);
            
            Print(cashStatementItem);
        }
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("stopping the service");
        return Task.CompletedTask;
    }

    private void Print(CashStatementItem item)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(item, options);
        Console.WriteLine(json);

    }
}