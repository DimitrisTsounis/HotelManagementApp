using AutoMapper;
using FluentValidation;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository bookingRepository;
    private readonly IMapper mapper;
    private readonly IValidator<Booking> validator;

    public BookingsController(IBookingRepository bookingRepository, IMapper mapper, IValidator<Booking> validator)
    {
        this.bookingRepository = bookingRepository;
        this.mapper = mapper;
        this.validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings()
    {
        IEnumerable<Booking> bookings = await bookingRepository.GetAllAsync();

        if (!bookings.Any())
            return NoContent();

        var dto = bookings.Select(mapper.Map<Booking_OutputWebDTO>);
        return Ok(dto);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetBookingById([FromRoute] int id)
    {
        Booking booking = await bookingRepository.GetByIdAsync(id);

        if (booking is null)
            return NotFound();

        return Ok(mapper.Map<Booking_OutputWebDTO>(booking));
    }

    [HttpGet("hotel")]
    public async Task<IActionResult> GetBookingsOfSpecifiedHotel([FromQuery] int hotelId)
    {
        IEnumerable<Booking> bookings = await bookingRepository.GetAllBookingsOfSpecifiedHotel(hotelId);

        if (bookings is null)
            return NoContent();
        
        var dto = bookings.Select(mapper.Map<Booking_OutputWebDTO>);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] Booking_ManipulationWebDTO bookingInsertDTO)
    {
        Booking bookingToBeInserted = mapper.Map(bookingInsertDTO, new Booking());

        await validator.ValidateAndThrowAsync(bookingToBeInserted);
        bookingRepository.Create(bookingToBeInserted);
        await bookingRepository.SaveAsync();

        Booking_OutputWebDTO outputBooking = mapper.Map<Booking_OutputWebDTO>(bookingInsertDTO);
        return Created(string.Empty, outputBooking);
    }

    [HttpPatch("id/{id}")]
    public async Task<IActionResult> UpdateBooking([FromRoute] int id, [FromBody] Booking_ManipulationWebDTO bookinglDTO)
    {
        Booking existingBooking = await bookingRepository.GetByIdAsync(id);

        if (existingBooking is null)
            return NotFound($"No booking was found for the requested Id: {id}");

        mapper.Map(bookinglDTO, existingBooking);
        await validator.ValidateAndThrowAsync(existingBooking);

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
