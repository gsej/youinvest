namespace api.QueryHandlers;

public interface ISummaryQueryHandler
{
    Task<SummaryResult> Handle(SummaryRequest request);
}