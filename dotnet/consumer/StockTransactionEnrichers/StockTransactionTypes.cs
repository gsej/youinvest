namespace consumer.StockTransactionEnrichers;

public static class StockTransactionTypes
{
    public const string Purchase = "Purchase";
    public const string RegularInvestmentPurchase = "RegularInvestmentPurchase";
    public const string Sale = "Sale";
    public const string TransferIn = "TransferIn";
    public const string Receipt = "Receipt"; // stock coming in from corporate action or something
    public const string Removal = "Receipt"; // stock going out from corporate action or something
}