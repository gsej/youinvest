namespace common;

public static class StockTransactionTypes
{
    public const string Purchase = "Purchase";
    public const string Sale = "Sale";
    public const string ReceiptOfRightsEntitlement = "Receipt of Rights entitlement";

    public const string ReceiptOfRightsFollowingAConsolidation = "Receipt of stock following a consolidation";
    public const string ReceiptOfStockFollowingACorporateAction = "Receipt of stock following a corporate action";
    public const string ReceiptOfStockFollowingATakeCover = "Receipt of stock following a takeover";

    public const string RemovalOfRightsWarrantsFollowingACorporateAction =
        "Removal of Rights/Warrants following a corporate action";

    public const string RemovalOfStockFollowingAConsolidation = "Removal of stock following a consolidation";
    public const string RemovalOfStockFollowingACorporateAction = "Removal of stock following a corporate action";
    public const string TransferIn = "Transfer In";
}