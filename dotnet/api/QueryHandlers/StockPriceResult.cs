namespace api.QueryHandlers;

public record StockPriceResult(decimal Price, string Currency, int AgeInDays);