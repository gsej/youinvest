namespace api.QueryHandlers.Summary;

public record Holding(string StockSymbol, string StockDescription, decimal Quantity, StockPriceResult? StockPrice, decimal? Value);