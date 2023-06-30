using consumer.Entities;

namespace consumer.StockTransactionEnrichers;

public interface IStockTransactionEnricher
{
    void Enrich(StockTransaction stockTransaction);
}