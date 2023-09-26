using HotelManagement.Infrastructure.Models;

namespace HotelManagement.Infrastructure.Repositories;
public interface IHotelRepository
{
    Task<IEnumerable<Hotel>> SearchHotelsByNameAsync(string searchTerm);
}