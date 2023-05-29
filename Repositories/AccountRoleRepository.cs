using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;

namespace API_Web.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(BookingManagementDBContext context): base(context)
    {
    }
}