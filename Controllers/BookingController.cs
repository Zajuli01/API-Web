using API_Web.Contracts;
using API_Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    public BookingController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var booking = _bookingRepository.GetAll();
        if (!booking.Any())
        {
            return NotFound();
        }

        return Ok(booking);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var booking = _bookingRepository.GetByGuid(guid);
        if (booking is null)
        {
            return NotFound();
        }

        return Ok(booking);
    }

    [HttpPost]
    public IActionResult Create(Booking booking)
    {
        var result = _bookingRepository.Create(booking);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(Booking booking)
    {
        var isUpdated = _bookingRepository.Update(booking);
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
