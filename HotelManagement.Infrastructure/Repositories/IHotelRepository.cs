using HotelManagement.Infrastructure.Models;

namespace HotelManagement.Infrastructure.Repositories;
public interface IHotelRepository : IBaseRepository<Hotel>
{
    Task<IEnumerable<Hotel>> SearchHotelsByNameAsync(string searchTerm);
}