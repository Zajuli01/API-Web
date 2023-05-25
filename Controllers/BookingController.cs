using API_Web.Contracts;
using API_Web.Model;
using API_Web.Utility;
using API_Web.ViewModels.Bookings;
using API_Web.ViewModels.Employees;
using API_Web.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Booking, BookingVM> _mapper;
    private readonly IMapper<Room, RoomVM> _roomVMMapper;
    private readonly IMapper<Employee, EmployeeVM> _employeeVMMapper;
    public BookingController(IBookingRepository bookingRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository, IMapper<Booking, BookingVM> mapper, IMapper<Room, RoomVM> roomVMMapper, IMapper<Employee, EmployeeVM> employeeVMMapper)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _roomVMMapper = roomVMMapper;
        _employeeVMMapper = employeeVMMapper;
    }


    [HttpGet("BookingDetail")]
    public IActionResult GetAllBookingDetail()
    {
        try
        {
            var bookingDetails = _bookingRepository.GetAllBookingDetail();

            return Ok(bookingDetails);

        }
        catch
        {
            return Ok("error");
        }
    }

    [HttpGet("BookingDetail/{guid}")]
    public IActionResult GetDetailByGuid(Guid guid)
    {
        try
        {
            var booking = _bookingRepository.GetBookingDetailByGuid(guid);
            if (booking is null)
            {

                return NotFound();
            }

            return Ok(booking);
        }
        catch
        {
            return Ok("error");
        }
    }

    [HttpGet("bookinglength")]
    public IActionResult GetDuration()
    {
        var bookingLengths = _bookingRepository.GetBookingDuration();
        if (!bookingLengths.Any())
        {
            return NotFound();
        }

        return Ok(bookingLengths);
    }




    [HttpGet]
    public IActionResult GetAll()
    {
        var booking = _bookingRepository.GetAll();
        if (!booking.Any())
        {
            return NotFound();
        }
        var result = booking.Select(_mapper.Map).ToList();

        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var booking = _bookingRepository.GetByGuid(guid);
        if (booking is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(booking);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(BookingVM bookingVM)
    {
        var resultConverted = _mapper.Map(bookingVM);
        var result = _bookingRepository.Create(resultConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(BookingVM bookingVM)
    {
        var resultConverted = _mapper.Map(bookingVM);
        var isUpdated = _bookingRepository.Update(resultConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _bookingRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
