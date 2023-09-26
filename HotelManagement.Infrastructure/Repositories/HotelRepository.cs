using HotelManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories;

public class HotelRepository : BaseRepository<Hotel>, IHotelRepository
{
    private readonly HotelManagementDbContext _context;

    public HotelRepository(HotelManagementDbContext context) : base(context)
    {
        _context = context;
    } 

    public async Task<IEnumerable<Hotel>> SearchHotelsByNameAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await _context.Hotels.ToListAsync();

        List<Hotel> hotels = await _context
            .Hotels
            .Where(h => h.Name.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();

        return hotels;
    }
}