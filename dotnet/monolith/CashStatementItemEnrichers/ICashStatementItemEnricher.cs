

using monolith.Entities;

namespace monolith.CashStatementItemEnrichers;

public interface ICashStatementItemEnricher
{
    void Enrich(CashStatementItem cashStatementItem);
}