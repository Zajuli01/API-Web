using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;

namespace API_Web.Repositories;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    private readonly BookingManagementDBContext _context;
    public AccountRepository(BookingManagementDBContext context): base(context)
    {
        _context = context;
    }
}