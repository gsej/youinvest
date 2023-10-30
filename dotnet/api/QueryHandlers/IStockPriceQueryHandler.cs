namespace api.QueryHandlers;

public interface IStockPriceQueryHandler
{
    Task<StockPriceResult> Handle(StockPriceRequest request);
}