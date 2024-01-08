using database;
using Microsoft.EntityFrameworkCore;

namespace api.QueryHandlers.History;

public class HistoryQueryHandler : IHistoryQueryHandler
{
    private readonly InvestmentsDbContext _context;

    public HistoryQueryHandler(InvestmentsDbContext context)
    {
        _context = context;
    }
    
    public async Task<HistoryResult> Handle(HistoryRequest request)
    {
        var knownValues = await _context.KnownValues
            .Where(kb => request.AccountCode == kb.AccountCode)
            .OrderBy(kb => kb.Date)
            .AsNoTracking()
            .Select(kb => new KnownValue(kb.Date, kb.AccountCode, kb.TotalValue))
            
            .ToListAsync();

        return new HistoryResult(knownValues);
    }
}