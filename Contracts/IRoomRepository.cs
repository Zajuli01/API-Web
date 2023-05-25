using API_Web.Model;
using API_Web.ViewModels.Rooms;

namespace API_Web.Contracts;

public interface IRoomRepository : IGeneralRepository<Room>
{
    Room Create(Room room);
    bool Update(Room room);
    bool Delete(Guid guid);
    IEnumerable<Room> GetAll();
    Room? GetByGuid(Guid guid);

    //IEnumerable<Room> GetDateTime(DateTime dateTime);
    IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
    IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();
    IEnumerable<RoomBookedTodayVM> GetRoomByDate();

}


