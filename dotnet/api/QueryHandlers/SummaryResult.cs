namespace api.QueryHandlers;

public record SummaryResult(IList<Holding> Holdings, decimal CashBalance);