using API_Web.Model;
using API_Web.ViewModels.Accounts;

namespace API_Web.Contracts;

public interface IAccountRepository : IGeneralRepository<Account>
{
    Account Create(Account account);
    bool Update(Account account);
    bool Delete(Guid guid);
    IEnumerable<Account> GetAll();
    Account? GetByGuid(Guid guid);

    int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);

    int UpdateOTP(Guid? employeeId);

    int Register(RegisterVM registerVM);

    LoginVM Login(LoginVM loginVM);

}
