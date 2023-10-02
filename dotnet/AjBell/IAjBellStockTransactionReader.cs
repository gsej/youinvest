namespace AjBell;

public interface IAjBellStockTransactionReader
{
    IEnumerable<AjBellStockTransaction> Read();
}