using API_Web.Model;
using API_Web.ViewModels.Bookings;

namespace API_Web.Contracts;

public interface IBookingRepository : IGeneralRepository<Booking>
{
    //Booking Create(Booking booking);
    //bool Update(Booking booking);
    //bool Delete(Guid guid);
    //IEnumerable<Booking> GetAll();
    //Booking? GetByGuid(Guid guid);

    //Booking? GetByDate(DateTime dateTime);
    IEnumerable<BookingDurationVM> GetBookingDuration();
    IEnumerable<BookingDetailVM> GetAllBookingDetail();
    BookingDetailVM GetBookingDetailByGuid(Guid guid);
    //K3
}
