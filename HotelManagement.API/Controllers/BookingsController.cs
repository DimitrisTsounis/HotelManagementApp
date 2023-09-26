using AutoMapper;
using HotelManagement.API.WebDTOs;
using HotelManagement.Infrastructure.Models;
using HotelManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository bookingRepository;
    private readonly IMapper mapper;

    public BookingsController(IBookingRepository bookingRepository, IMapper mapper)
    {
        this.bookingRepository = bookingRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings()
    {
        IEnumerable<Booking> bookings = await bookingRepository.GetAllAsync();

        return Ok(bookings);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetBookingById([FromRoute] int id)
    {
        Booking booking = await bookingRepository.GetByIdAsync(id);

        if (booking is null)
            return NotFound();

        return Ok(booking);
    }

    [HttpGet("hotel")]
    public async Task<IActionResult> GetBookingsOfSpecifiedHotel([FromQuery] int hotelId)
    {
        IEnumerable<Booking> booking = await bookingRepository.GetAllBookingsOfSpecifiedHotel(hotelId);

        if (booking is null)
            return NotFound();

        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
    {
        bookingRepository.Create(booking);
        await bookingRepository.SaveAsync();

        return Created(string.Empty, booking);
    }

    [HttpPatch("id/{id}")]
    public async Task<IActionResult> UpdateBooking([FromRoute] int id, [FromBody] BookingManipulation_WebDTO bookinglDTO)
    {
        Booking existingBooking = await bookingRepository.GetByIdAsync(id);

        if (existingBooking is null)
            return NotFound($"No booking was found for the requested Id: {id}");

        mapper.Map(bookinglDTO, existingBooking);
        bookingRepository.Update(existingBooking);
        await bookingRepository.SaveAsync();

        return Ok();
    }

    [HttpDelete("id/{id}")]
    public async Task<IActionResult> DeleteBooking([FromRoute] int id)
    {
        Booking existingBooking = await bookingRepository.GetByIdAsync(id);

        if (existingBooking is null)
            return NotFound($"No booking was found for the requested Id: {id}");

        await bookingRepository.DeleteAsync(id);
        await bookingRepository.SaveAsync();

        return Ok();
    }

}
