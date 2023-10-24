using database.Entities;

namespace loader.CashStatementItemEnrichers;

public interface ICashStatementItemEnricher
{
    void Enrich(CashStatementItem cashStatementItem);
}