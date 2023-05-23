using API_Web.Model;

namespace API_Web.Contracts;

public interface IAccountRoleRepository : IGeneralRepository<AccountRole>
{
    AccountRole Create(AccountRole accountRole);
    bool Update(AccountRole accountRole);
    bool Delete(Guid guid);
    IEnumerable<AccountRole> GetAll();
    AccountRole? GetByGuid(Guid guid);
}
