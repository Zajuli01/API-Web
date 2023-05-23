using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;

namespace API_Web.Repositories;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    private readonly BookingManagementDBContext _context;
    public RoleRepository(BookingManagementDBContext context): base(context)    
    {
        _context = context;
    }
}