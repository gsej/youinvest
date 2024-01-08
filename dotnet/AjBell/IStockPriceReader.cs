namespace AjBell;

public interface IStockPriceReader
{
    IEnumerable<StockPrice> Read();
    IEnumerable<StockPrice> Read(string fileName);
}