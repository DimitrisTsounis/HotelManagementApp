using HotelManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
{
    private readonly HotelManagementDbContext _context;

    public BaseRepository(HotelManagementDbContext _context)
    {
        this._context = _context;
    }

    public async Task<T> CreateAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await _context.Set<T>().AddAsync(entity);

        return entity;
    }

    public async Task<T> GetByIdAsync(int id) =>
        await _context.Set<T>().FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => 
        await _context.Set<T>().ToListAsync();

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id) =>    
        _context.Set<T>().Remove(await GetByIdAsync(id));

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}
