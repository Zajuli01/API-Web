﻿using API_Web.Model;

namespace API_Web.Contracts;

public interface IRoomRepository
{
    Room Create(Room room);
    bool Update(Room room);
    bool Delete(Guid guid);
    IEnumerable<Room> GetAll();
    Room? GetByGuid(Guid guid);
}