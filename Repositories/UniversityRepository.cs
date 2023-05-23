using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;

namespace API_Web.Repositories;

public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
{
    private readonly BookingManagementDBContext _context;
    public UniversityRepository(BookingManagementDBContext context) : base(context)
    {
        _context = context;
    }

}
