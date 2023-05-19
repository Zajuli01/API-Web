using API_Web.Model;

namespace API_Web.Contracts;

public interface IBookingRepository
{
    Booking Create(Booking booking);
    bool Update(Booking booking);
    bool Delete(Guid guid);
    IEnumerable<Booking> GetAll();
    Booking? GetByGuid(Guid guid);
}
