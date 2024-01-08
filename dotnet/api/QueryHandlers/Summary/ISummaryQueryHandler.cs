namespace api.QueryHandlers.Summary;

public interface ISummaryQueryHandler
{
    Task<SummaryResult> Handle(SummaryRequest request);
}