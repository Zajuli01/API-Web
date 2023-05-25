using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;
using API_Web.ViewModels.Bookings;
using API_Web.ViewModels.Employees;
using API_Web.ViewModels.Rooms;

namespace API_Web.Repositories;

public class RoomRepository : GeneralRepository<Room>, IRoomRepository
{
    private readonly BookingManagementDBContext _context;

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoomRepository _roomRepository;



    private readonly IBookingRepository _bookingRepository;
    public RoomRepository(BookingManagementDBContext context, IBookingRepository booking, IEmployeeRepository employeeRepository) : base(context)
    {
        _context = context;
        _bookingRepository = booking;
        _employeeRepository = employeeRepository;
    }


    public IEnumerable<RoomBookedTodayVM> GetRoomByDate()
    {
        try
        {
            //get all data from booking and rooms
            var booking = _bookingRepository.GetAll();
            var rooms = GetAll();

            var startToday = DateTime.Today;
            var endToday = DateTime.Today.AddHours(23).AddMinutes(59);

            var roomUse = rooms.Join(booking, Room => Room.Guid, booking => booking.RoomGuid, (Room, booking) =>
            new { Room, booking })
                    .Select(joinResult => new {
                        joinResult.Room.Name,
                        joinResult.Room.Floor,
                        joinResult.Room.Capacity,
                        joinResult.booking.StartDate,
                        joinResult.booking.EndDate
                    }
             );
            var roomUseTodays = new List<RoomBookedTodayVM>();
            foreach (var room in roomUse)
            {
                if ((room.StartDate > startToday && room.EndDate > endToday) || (room.StartDate < startToday && room.EndDate < startToday))
                {
                    var roomDay = new RoomBookedTodayVM
                    {
                        RoomName = room.Name,
                        Floor = room.Floor,
                        Capacity = room.Capacity,
                    };
                    roomUseTodays.Add(roomDay);
                }
            };
            return roomUseTodays;
        }
        catch
        {
            return null;

        }
    }


    public IEnumerable<RoomBookedTodayVM> GetAvailableRoom()
    {
        try
        {
            //get all data from booking and rooms
            var booking = _bookingRepository.GetAll();
            var rooms = GetAll();

            var startToday = DateTime.Today;
            var endToday = DateTime.Today.AddHours(23).AddMinutes(59);

            var roomUse = rooms.Join(booking, Room => Room.Guid, booking => booking.RoomGuid, (Room, booking) => new { Room, booking })
                    .Select(joinResult => new {
                        joinResult.Room.Name,
                        joinResult.Room.Floor,
                        joinResult.Room.Capacity,
                        joinResult.booking.StartDate,
                        joinResult.booking.EndDate
                    }
             );

            var roomUseTodays = new List<RoomBookedTodayVM>();


            foreach (var room in roomUse)
            {
                if ((room.StartDate < startToday && room.EndDate < startToday) || (room.StartDate > startToday && room.EndDate > endToday))
                {
                    var roomDay = new RoomBookedTodayVM
                    {
                        RoomName = room.Name,
                        Floor = room.Floor,
                        Capacity = room.Capacity
                    };
                    roomUseTodays.Add(roomDay);
                }
            }
            return roomUseTodays;
        }

        catch
        {
            return null;

        }
    }


    public IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime)
    {
        var rooms = GetAll();
        var bookings = _context.Bookings.ToList();
        var employees = _context.Employees.ToList();

        var usedRooms = new List<MasterRoomVM>();

        foreach (var room in rooms)
        {
            var booking = bookings.FirstOrDefault(b => b.RoomGuid == room?.Guid && b.StartDate <= dateTime && b.EndDate >= dateTime);
            if (booking != null)
            {
                var employee = employees.FirstOrDefault(e => e.Guid == booking.EmployeeGuid);

                var result = new MasterRoomVM
                {
                    BookedBy = employee.FirstName + " " + employee?.LastName,
                    Status = booking.Status.ToString(),
                    RoomName = room.Name,
                    Floor = room.Floor,
                    Capacity = room.Capacity,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,

                };

                usedRooms.Add(result);
            }
        }

        return usedRooms;
    }

    public IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms()
    {
        var rooms = GetAll();
        var bookings = _context.Bookings.ToList();
        var employees = _context.Employees.ToList();

        var usedRooms = new List<RoomUsedVM>();

        var currentTime = DateTime.Now;

        foreach (var room in rooms)
        {
            var booking = bookings.FirstOrDefault(b => b.RoomGuid == room?.Guid && b.StartDate <= currentTime && b.EndDate >= currentTime);
            if (booking != null)
            {
                var employee = employees.FirstOrDefault(e => e.Guid == booking.EmployeeGuid);

                var result = new RoomUsedVM
                {
                    RoomName = room.Name,
                    Status = booking.Status.ToString(),
                    Floor = room.Floor,
                    BookedBy = employee.FirstName + " " + employee?.LastName,
                };

                usedRooms.Add(result);
            }
        }
        return usedRooms;
    }
}