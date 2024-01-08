namespace api.QueryHandlers.History;

public interface IHistoryQueryHandler
{
    Task<HistoryResult> Handle(HistoryRequest request);
}