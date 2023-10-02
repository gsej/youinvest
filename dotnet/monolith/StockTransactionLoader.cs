using System.Text.Json;
using AjBell;
using database;
using database.Entities;
using monolith.Entities;

namespace monolith;

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
        var ajBellStockTransactions = _reader.Read().ToList();
        //   var stockTransactionTypeEnricher = new StockTransactionTypeEnricher();

        foreach (var ajBellStockTransaction in ajBellStockTransactions)
        {
            var stockTransaction = new StockTransaction()
            {
                Account = ajBellStockTransaction.Account,
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

            //       stockTransactionTypeEnricher.Enrich(stockTransaction);

                 _context.StockTransactions.Add(stockTransaction);
            _context.SaveChanges();

        }
    }
}