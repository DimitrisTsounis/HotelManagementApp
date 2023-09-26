using HotelManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories;
public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    private readonly HotelManagementDbContext _context;

    public BookingRepository(HotelManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Booking>> GetAllBookingsOfSpecifiedHotel(int hotelId)
    {
        List<Booking> bookings = await _context
            .Bookings
            .Where(b => b.HotelId == hotelId)
            .ToListAsync();

        return bookings;
    }
}
