using System.Text.Json;
using AjBell;
using database;
using database.Entities;
using monolith.CashStatementItemEnrichers;

namespace monolith;

public class CashStatementItemLoader
{
    private readonly IAjBellCashStatementReader _ajBellCashStatementReader;
    private readonly InvestmentsDbContext _context;

     private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public CashStatementItemLoader(IAjBellCashStatementReader ajBellAjBellCashStatementReader, InvestmentsDbContext context)
    {
        _ajBellCashStatementReader = ajBellAjBellCashStatementReader;
        _context = context;
    }

    public void Load()
    {
        var ajBellCashStatementItems = _ajBellCashStatementReader.Read().ToList();
        var cashStatementItemTypeEnricher = new CashStatementItemTypeEnricher();
        
        foreach (var ajBellCashStatementItem in ajBellCashStatementItems)
        {
            var cashStatementItem = new CashStatementItem
            {
                Account = ajBellCashStatementItem.Account,
                Date = ajBellCashStatementItem.Date,
                Description = ajBellCashStatementItem.Description,
                PaymentAmountGbp = ajBellCashStatementItem.Payment_Amount_Gbp,
                ReceiptAmountGbp = ajBellCashStatementItem.Receipt_Amount_Gbp
            };

            cashStatementItemTypeEnricher.Enrich(cashStatementItem);

            _context.CashStatementItems.Add(cashStatementItem);
            _context.SaveChanges();
            
        }
    }
}