using HotelManagement.Infrastructure.Models;

namespace HotelManagement.Infrastructure.Repositories;
public interface IBookingRepository : IBaseRepository<Booking>
{
    Task<IEnumerable<Booking>> GetAllBookingsOfSpecifiedHotel(int hotelId);
}
