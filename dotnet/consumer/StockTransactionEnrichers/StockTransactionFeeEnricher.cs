using consumer.Entities;

namespace consumer.StockTransactionEnrichers;

public class StockTransactionFeeEnricher : IStockTransactionEnricher
{
    public void Enrich(StockTransaction stockTransaction)
    {
        if (stockTransaction.TransactionType == StockTransactionTypes.Purchase)
        {
            stockTransaction.Fee = 9.95m;
        }
        else if (stockTransaction.TransactionType == StockTransactionTypes.Sale)
        {  stockTransaction.Fee = 9.95m;
        }
        else if (stockTransaction.TransactionType == StockTransactionTypes.RegularInvestmentPurchase)
        {
            stockTransaction.Fee = 1.50m;
        }
    }
}