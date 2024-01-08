namespace api.QueryHandlers.Summary;

public record struct SummaryRequest(string[] AccountCodes, string Date);