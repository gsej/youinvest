namespace AjBell;

public record AjBellCashStatementItem(
    string Account,
    string Date,
    string Description,
    // ReSharper disable once InconsistentNaming
    decimal Receipt_Amount_Gbp,
    // ReSharper disable once InconsistentNaming
    decimal Payment_Amount_Gbp);