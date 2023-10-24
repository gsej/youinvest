using database.Entities;

namespace loader.StockTransactionEnrichers;

public interface IStockTransactionEnricher
{
    void Enrich(StockTransaction stockTransaction);
}