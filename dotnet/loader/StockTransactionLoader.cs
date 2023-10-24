using AjBell;
using database;
using database.Entities;
using Microsoft.EntityFrameworkCore;

namespace loader;

public class StockTransactionLoader
{
    private readonly IAjBellStockTransactionReader _reader;
    private readonly InvestmentsDbContext _context;

    public StockTransactionLoader(IAjBellStockTransactionReader reader,
        InvestmentsDbContext context)
    {
        _reader = reader;
        _context = context;
    }

    public void Load()
    {
        var stocks = _context
            .Stocks
            .Include(stock => stock.Aliases)
            .ToList();
        
        var ajBellStockTransactions = _reader.Read().ToList();
        //   var stockTransactionTypeEnricher = new StockTransactionTypeEnricher();

        foreach (var ajBellStockTransaction in ajBellStockTransactions)
        {
            var stockTransaction = new StockTransaction()
            {
                AccountCode = ajBellStockTransaction.Account,
                Date = ajBellStockTransaction.Date,
                Transaction = ajBellStockTransaction.Transaction,
                Description = ajBellStockTransaction.Description,
                Quantity = ajBellStockTransaction.Quantity,
                AmountGbp = ajBellStockTransaction.Amount_Gbp,
                Reference = ajBellStockTransaction.Reference,
                TransactionType = "???",
                Fee = 999,
                StampDuty = 11111
            };

            var matchingStock = stocks.SingleOrDefault(s =>
                s.Description.Equals(stockTransaction.Description, StringComparison.InvariantCultureIgnoreCase) ||
                s.Aliases.Any(alias =>
                    alias.Description.Equals(stockTransaction.Description, StringComparison.InvariantCultureIgnoreCase)));

            if (matchingStock != null)
            {
                // Move to enricher
                stockTransaction.Stock = matchingStock;
            }

            //       stockTransactionTypeEnricher.Enrich(stockTransaction);

            _context.StockTransactions.Add(stockTransaction);
            _context.SaveChanges();

        }
    }
}