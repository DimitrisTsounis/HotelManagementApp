using HotelManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories;

public class HotelRepository : BaseRepository<Hotel>
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

        return await _context
            .Hotels
            .Where(h => h.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
    }
}