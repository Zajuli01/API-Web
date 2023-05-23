using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;

namespace API_Web.Repositories;

public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    private readonly BookingManagementDBContext _context;
    public BookingRepository(BookingManagementDBContext context): base(context)
    {
        _context = context;
    }
}