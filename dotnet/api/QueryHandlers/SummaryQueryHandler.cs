using common;
using database;
using database.ValueTypes;
using Microsoft.EntityFrameworkCore;

namespace api.QueryHandlers;

public record struct SummaryRequest(string[] AccountCodes, string Date);

public record Holding(string StockSymbol, decimal Quantity);

public class SummaryResult
{
    public IList<Holding> Holdings { get; set; }
    public decimal CashBalance { get; set; }
}

public interface ISummaryQueryHandler
{
    Task<SummaryResult> Handle(SummaryRequest request);
}
public class SummaryQueryHandler : ISummaryQueryHandler
{
    private readonly InvestmentsDbContext _context;

    public SummaryQueryHandler(InvestmentsDbContext context)
    {
        _context = context;
    }
    
    // Summarizes the position of an account or set of accounts on a given day
    public async Task<SummaryResult> Handle(SummaryRequest request)
    {
        var cashBalance = await _context.CashStatementItems
            .Where(c =>
                request.AccountCodes.Contains(c.AccountCode) &&
                request.Date.CompareTo(c.Date) >= 0 &&
                c.CashStatementItemType != CashStatementItemTypes.Balance
            )
            .AsNoTracking()
            .SumAsync(c => c.ReceiptAmountGbp + c.PaymentAmountGbp);

        var holdings = new List<Holding>();

        var stockTransactions = _context.StockTransactions
            .Where(s =>
                request.AccountCodes.Contains(s.AccountCode) &&
                request.Date.CompareTo(s.Date) >= 0)
            .GroupBy(s => s.StockSymbol)
            .ToList();

        foreach (var group in stockTransactions)
        {
            var stockSymbol = group.Key;

            var stocksAdded = group.Where(st =>
                    st.Transaction == StockTransactionTypes.Purchase)
                .Sum(st => st.Quantity);

            var stocksRemoved = group.Where(st =>
                    st.Transaction == StockTransactionTypes.Sale)
                .Sum(st => st.Quantity);
            
            holdings.Add(new Holding(stockSymbol, stocksAdded - stocksRemoved));
            
        }
        
        return new SummaryResult { Holdings = holdings, CashBalance = cashBalance };
    }
}