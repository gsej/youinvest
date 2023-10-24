namespace AjBell
{
    public record AjBellStockTransaction(
        string Account,
        string Date,
        string Transaction,
        string Description,
        decimal Quantity,
        // ReSharper disable once InconsistentNaming
        decimal Amount_Gbp,
        string Reference
    );
}