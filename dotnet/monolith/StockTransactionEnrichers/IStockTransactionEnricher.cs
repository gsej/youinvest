using database.Entities;
using monolith.Entities;

namespace monolith.StockTransactionEnrichers;

public interface IStockTransactionEnricher
{
    void Enrich(StockTransaction stockTransaction);
}