using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;

namespace API_Web.Repositories;

public class EducationRepository : GeneralRepository<Education>, IEducationRepository
{
    private readonly BookingManagementDBContext _context;
    public EducationRepository(BookingManagementDBContext context): base(context)
    {
        _context = context;
    }

}