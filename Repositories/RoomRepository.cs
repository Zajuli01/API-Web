using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;

namespace API_Web.Repositories;

public class RoomRepository : GeneralRepository<Room>, IRoomRepository
{
    private readonly BookingManagementDBContext _context;
    private readonly IBookingRepository _bookingRepository;
    public RoomRepository(BookingManagementDBContext context) : base(context)
    {
        _context = context;
    }
    public IEnumerable<Booking> GetRoomsByDateTime(DateTime dateTime)
    {
        var booking = _bookingRepository.GetAll();
        var filteredBookings = booking.Where(booking => booking.StartDate <= dateTime && booking.EndDate >= dateTime);
        return filteredBookings;
    }
}