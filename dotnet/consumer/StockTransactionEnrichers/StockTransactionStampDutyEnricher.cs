using consumer.Database;
using consumer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace consumer.StockTransactionEnrichers;

public class StockTransactionStampDutyEnricher : IStockTransactionEnricher
{
    private readonly ConsumerDbContext _consumerDbContext;
    private readonly IList<Stock> _stocks;

    public StockTransactionStampDutyEnricher(ILogger<StockTransactionStampDutyEnricher> logger, ConsumerDbContext consumerDbContext)
    {
        _consumerDbContext = consumerDbContext;

        logger.LogInformation("loading stocks");
        _stocks = consumerDbContext.Stocks.ToList();
    }
    public void Enrich(StockTransaction stockTransaction)
    {
        if (stockTransaction.TransactionType is StockTransactionTypes.Purchase or StockTransactionTypes.RegularInvestmentPurchase)
        {
            // calculate stamp duty.....

            var stock = _stocks.SingleOrDefault(s => s.Description == stockTransaction.Description);

            if (stock == null)
            {
                throw new InvalidOperationException($"stock with description '{stockTransaction.Description}' was not found");
            }
            else if (stock.SubjectToStampDuty)
            {

                var amountLessFees = stockTransaction.AmountGbp - stockTransaction.Fee;

                var factor = 1 - (1 / 1.005m);
                var stamp = amountLessFees * factor;
                stockTransaction.StampDuty = stamp;
            }
            else
            {
                stockTransaction.StampDuty = 0;
            }
        }
        else
        {
            stockTransaction.StampDuty = 0;
        }
    }
}