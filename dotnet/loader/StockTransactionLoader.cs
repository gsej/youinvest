using AjBell;
using database;
using database.Entities;
using loader.StockTransactionEnrichers;
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

    public async Task Load()
    {
        var stocks = await _context
            .Stocks
            .Include(stock => stock.Aliases)
            .Include(stock => stock.AlternativeSymbols)
            .ToListAsync();
        
        var ajBellStockTransactions = _reader.Read().ToList();
        var stockTransactionTypeEnricher = new StockTransactionTypeEnricher();

        foreach (var ajBellStockTransaction in ajBellStockTransactions)
        {
            var matchingStock = stocks.SingleOrDefault(s =>
                s.Description.Equals(ajBellStockTransaction.Description, StringComparison.InvariantCultureIgnoreCase) ||
                s.Aliases.Any(alias =>
                    alias.Description.Equals(ajBellStockTransaction.Description, StringComparison.InvariantCultureIgnoreCase)));

            var stockTransaction = new StockTransaction(
                accountCode: ajBellStockTransaction.Account,
                date: ajBellStockTransaction.Date,
                transaction: ajBellStockTransaction.Transaction,
                description: ajBellStockTransaction.Description,
                quantity: ajBellStockTransaction.Quantity,
                amountGbp: ajBellStockTransaction.Amount_Gbp,
                reference: ajBellStockTransaction.Reference,
                fee: 999, //todo: enrich
                stampDuty: 11111, // todo: enrich,
                stock: matchingStock
            );

            stockTransactionTypeEnricher.Enrich(stockTransaction);

            _context.StockTransactions.Add(stockTransaction);
            _context.SaveChanges();

        }
    }
}