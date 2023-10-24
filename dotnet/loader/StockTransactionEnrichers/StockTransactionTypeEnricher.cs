using System.Text.Json;
using common;
using database.Entities;

namespace loader.StockTransactionEnrichers;

public class StockTransactionTypeEnricher : IStockTransactionEnricher
{
    public void Enrich(StockTransaction stockTransaction)
    {
        if (stockTransaction.Transaction.Equals("Purchase"))
        {
            stockTransaction.TransactionType = StockTransactionTypes.Purchase;
        }
        else if (stockTransaction.Transaction.Equals("Sale"))
        {
            stockTransaction.TransactionType = StockTransactionTypes.Sale;
        }
        else if (stockTransaction.Transaction.Equals("Transfer In"))
        {
            stockTransaction.TransactionType = StockTransactionTypes.TransferIn;
        }
        else if (stockTransaction.Transaction.Contains("Removal"))
        {
            stockTransaction.TransactionType = StockTransactionTypes.Removal;
        } else if (stockTransaction.Transaction.Contains("Receipt"))
        {
            stockTransaction.TransactionType = StockTransactionTypes.Receipt;
        }
        else
        {
            stockTransaction.TransactionType = string.Empty;
        }
        //
        // else
        // {
        //     var json = JsonSerializer.Serialize(cashStatementItem, new JsonSerializerOptions { WriteIndented = true});
        //     throw new Exception($"couldn't identify type for description {json}");
        // }
    }
}