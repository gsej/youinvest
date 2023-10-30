using AjBell;
using database;
using database.Entities;
using loader.CashStatementItemEnrichers;

namespace loader;

public class CashStatementItemLoader
{
    private readonly IAjBellCashStatementReader _ajBellCashStatementReader;
    private readonly InvestmentsDbContext _context;

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
            var cashStatementItem = new CashStatementItem(
                accountCode: ajBellCashStatementItem.Account,
                date: ajBellCashStatementItem.Date, 
                description: ajBellCashStatementItem.Description,
                paymentAmountGbp: ajBellCashStatementItem.Payment_Amount_Gbp,
                receiptAmountGbp: ajBellCashStatementItem.Receipt_Amount_Gbp);

            cashStatementItemTypeEnricher.Enrich(cashStatementItem);

            _context.CashStatementItems.Add(cashStatementItem);
            _context.SaveChanges();
            
        }
    }
}