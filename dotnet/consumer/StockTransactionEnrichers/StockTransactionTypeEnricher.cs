using common;
using StockTransaction = consumer.Entities.StockTransaction;

namespace consumer.StockTransactionEnrichers;

public class StockTransactionTypeEnricher : IStockTransactionEnricher
{
    public void Enrich(StockTransaction stockTransaction)
    {
        if (stockTransaction.Transaction.Equals("Purchase"))
        {
            if (RegularInvestmentDayCalculator.IsRegularInvestmentDay(stockTransaction.Date))
            {
                stockTransaction.TransactionType = StockTransactionTypes.RegularInvestmentPurchase;
            }
            else
            {
                stockTransaction.TransactionType = StockTransactionTypes.Purchase;
            }
        }
        else if (stockTransaction.Transaction.Equals("Sale"))
        {
            stockTransaction.TransactionType = StockTransactionTypes.Sale;
        }
        else if (stockTransaction.Transaction.Equals("Transfer In"))
        {
            stockTransaction.TransactionType = StockTransactionTypes.TransferIn;
        }
        else if (stockTransaction.Transaction.StartsWith("Removal"))
        {
            stockTransaction.TransactionType = StockTransactionTypes.Removal;
        }
        else if (stockTransaction.Transaction.StartsWith("Receipt"))
        {
            stockTransaction.TransactionType = StockTransactionTypes.Receipt;
        }
    }
}