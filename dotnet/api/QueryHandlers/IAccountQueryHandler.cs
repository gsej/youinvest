namespace api.QueryHandlers;

public interface IAccountQueryHandler
{
    Task<IList<Account>> Handle(AccountRequest _);
}