using common;
using database;
using Microsoft.EntityFrameworkCore;

namespace api.QueryHandlers;

public record struct StockPriceRequest(string StockSymbol, string Date);

public class StockPriceQueryQueryHandler : IStockPriceQueryHandler
{
    private readonly InvestmentsDbContext _context;

    public StockPriceQueryQueryHandler(InvestmentsDbContext context)
    {
        _context = context;
    }
    
    // Gets the price of a stock on a given day
    public async Task<StockPriceResult> Handle(StockPriceRequest request)
    {
        return new StockPriceResult();
        // var cashBalance = await _context.CashStatementItems
        //     .Where(c =>
        //         request.AccountCodes.Contains(c.AccountCode) &&
        //         request.Date.CompareTo(c.Date) >= 0 &&
        //         c.CashStatementItemType != CashStatementItemTypes.Balance
        //     )
        //     .AsNoTracking()
        //     .SumAsync(c => c.ReceiptAmountGbp + c.PaymentAmountGbp);
        //
        // var holdings = new List<Holding>();
        //
        // var stockTransactions = _context.StockTransactions
        //     .Include(stockTransaction => stockTransaction.Stock)
        //     .Where(s =>
        //         request.AccountCodes.Contains(s.AccountCode) &&
        //         request.Date.CompareTo(s.Date) >= 0)
        //     .GroupBy(s => s.Stock)
        //     .ToList();
        //
        // foreach (var group in stockTransactions)
        // {
        //     var stock = group.Key;
        //
        //     var stocksAdded = group.Where(st =>
        //             st.TransactionType is StockTransactionTypes.Purchase or StockTransactionTypes.TransferIn or StockTransactionTypes.Receipt)
        //         .Sum(st => st.Quantity);
        //
        //     var stocksRemoved = group.Where(st =>
        //             st.TransactionType is StockTransactionTypes.Sale or StockTransactionTypes.Removal)
        //         .Sum(st => st.Quantity);
        //
        //     var totalHeld = stocksAdded - stocksRemoved;
        //
        //     if (totalHeld != 0)
        //     {
        //         holdings.Add(new Holding(stock.StockSymbol, stock.Description, totalHeld));
        //     }
        // }
        //
        // return new SummaryResult { Holdings = holdings, CashBalance = cashBalance };
    }
}