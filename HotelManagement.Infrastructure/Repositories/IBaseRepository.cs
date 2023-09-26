using HotelManagement.Infrastructure.Models;

namespace HotelManagement.Infrastructure.Repositories;
public interface IBaseRepository<T> where T : EntityBase
{
    Task<T> CreateAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
