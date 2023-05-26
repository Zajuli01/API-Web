using API_Web.Contracts;
using API_Web.Model;
using API_Web.Others;
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
                return NotFound(new ResponseVM<IEnumerable<RoomBookedTodayVM>>
                {
                    Code = 400,
                    Status = "Not Found",
                    Message = "Ruangan Tidak Ditemukan",
                    Data = room
                }
                    );
            }

            return Ok(new ResponseVM<IEnumerable<RoomBookedTodayVM>>
            {

                Code = 200,
                Status = "OK",
                Message = "Success",
                Data = room
            }
                );
        }
        catch
        {
            return NotFound(new ResponseVM<RoomBookedTodayVM>
            {
                Code = 500,
                Status = "Failed",
                Message = "Runtime error acces Server",
            }
              );
        }
    }


    [HttpGet("CurrentlyUsedRooms")]
    public IActionResult GetCurrentlyUsedRooms()
    {
        var respons = new ResponseVM<IEnumerable<RoomUsedVM>>();
        try
        {
            var room = _roomRepository.GetCurrentlyUsedRooms();
            if (room.Count() < 1)
            {
                return NotFound(respons.NotFound(room));
            }
            var succes = respons.Success(room);
            return Ok(succes);
        }
        catch (Exception ex)
        {

            return BadRequest(respons.Error(ex.Message));
        }
    }





    [HttpGet("GetCurrentlyUsedRoomsByDateTime")]
    public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
    {
        var respons = new ResponseVM<IEnumerable<MasterRoomVM>>();
        try
        {
            var room = _roomRepository.GetByDate(dateTime);
            if (room is null)
            {
                return NotFound(respons.NotFound(room));
            }

            return Ok(respons.Success(room));
        }
        catch (Exception ex)
        {
            return BadRequest(respons.Error(ex.Message));
        }
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
