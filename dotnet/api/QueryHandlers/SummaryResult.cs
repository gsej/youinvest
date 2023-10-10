namespace api.QueryHandlers;

public class SummaryResult
{
    public IList<Holding> Holdings { get; set; }
    public decimal CashBalance { get; set; }
}