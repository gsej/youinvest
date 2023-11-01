namespace api.QueryHandlers;

public record struct StockPriceRequest(string StockSymbol, string Date);