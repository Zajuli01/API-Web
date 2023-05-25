using API_Web.Contracts;
using API_Web.Model;
using API_Web.Repositories;
using API_Web.ViewModels.Bookings;
using API_Web.ViewModels.Educations;
using API_Web.ViewModels.Employees;
using API_Web.ViewModels.Rooms;
using API_Web.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Room, RoomVM> _mapper;
    private readonly IMapper<Booking, BookingVM> _bookingVMMapper;
    private readonly IMapper<Employee, EmployeeVM> _employeeVMMapper;
    public RoomController(IRoomRepository roomRepository,
        IEmployeeRepository employeeRepository,
        IMapper<Employee, EmployeeVM> employeeVMMapper, IBookingRepository bookingRepository, IMapper<Room, RoomVM> mapper, IMapper<Booking, BookingVM> bookingVMMapper)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _bookingVMMapper = bookingVMMapper;
        _employeeRepository = employeeRepository;
        _employeeVMMapper = employeeVMMapper;
    }


    [HttpGet("RoomAvailable")]
    public IActionResult GetRoomByDate()
    {
        try
        {
            var room = _roomRepository.GetRoomByDate();
            if (room is null)
            {
                return Ok("tidak ada data");
            }

            return Ok(room);
        }
        catch
        {
            return Ok("ada error");
        }
    }


    [HttpGet("CurrentlyUsedRooms")]
    public IActionResult GetCurrentlyUsedRooms()
    {
        var room = _roomRepository.GetCurrentlyUsedRooms();
        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet("CurrentlyUsedRoomsByDate")]
    public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
    {
        var room = _roomRepository.GetByDate(dateTime);
        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    //[HttpGet("CurrentlyUsedRooms")]
    //public IActionResult GetDateTimeRooms()
    //{
    //    DateTime now = DateTime.Now;
    //    var booking = _bookingRepository.GetByDate(now);
    //    if (booking is null)
    //    {
    //        return NotFound();
    //    }
    //    var room = _roomRepository.GetByDate(now);
    //    if (room is null)
    //    {
    //        return NotFound();
    //    }
    //    var emp = _employeeRepository.GetByDate(now);
    //    if (emp is null)
    //    {
    //        return NotFound();
    //    }


    //    var data = new
    //    {
    //        BookedBy = emp.FirstName + " " + emp.LastName,
    //        Status = booking.Status.ToString(),
    //        RoomName = room.Name,
    //        Floor = room.Floor.ToString(),
    //        Capacity = room.Capacity,
    //        StartDate = booking.StartDate,
    //        EndDate = booking.EndDate
    //    };



    //    return Ok(data);
    //}






    [HttpGet("RoomsByDateTime")]
    public IActionResult GetRoomsByDateTime(DateTime dateTime)
    {

        var room = _roomRepository.GetAll();
        var booking = _bookingRepository.GetAll();
        var emp = _employeeRepository.GetAll();
        var filteredRooms = booking.Where(booking => booking.StartDate <= dateTime && booking.EndDate >= dateTime).ToList();

        var result = filteredRooms.Select(room => new
        {

            BookedBy = room.Employee.FirstName + " " + room.Employee.LastName,
            Status = room.Status.ToString(),
            RoomName = room.Room.Name,
            Floor = room.Room.Floor,
            Capacity = room.Room.Capacity,
            StartDate = room.StartDate,
            EndDate = room.EndDate
        });

        return Ok(result);
    }



    [HttpGet]
    public IActionResult GetAll()
    {
        var room = _roomRepository.GetAll();
        if (!room.Any())
        {
            return NotFound();
        }
        var resultConverted = room.Select(_mapper.Map).ToList();

        return Ok(resultConverted);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _roomRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(room);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(RoomVM roomVM)
    {
        var roomConverted = _mapper.Map(roomVM);
        var result = _roomRepository.Create(roomConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(RoomVM roomVM)
    {
        var roomConverted = _mapper.Map(roomVM);
        var isUpdated = _roomRepository.Update(roomConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roomRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
