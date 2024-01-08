namespace api.QueryHandlers.Summary;

public record SummaryResult(IList<Holding> Holdings, decimal CashBalance);