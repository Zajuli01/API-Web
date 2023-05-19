using API_Web.Model;

namespace API_Web.Contracts;

public interface IAccountRepository
{
    Account Create(Account account);
    bool Update(Account account);
    bool Delete(Guid guid);
    IEnumerable<Account> GetAll();
    Account? GetByGuid(Guid guid);
}
