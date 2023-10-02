namespace AjBell;

public interface IAjBellCashStatementReader
{
    IEnumerable<AjBellCashStatementItem> Read();
}