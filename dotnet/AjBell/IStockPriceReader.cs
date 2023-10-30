namespace AjBell;

public interface IStockPriceReader
{
    IEnumerable<StockPrice> Read();
}