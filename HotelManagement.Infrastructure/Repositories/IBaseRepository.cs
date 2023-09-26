using HotelManagement.Infrastructure.Models;

namespace HotelManagement.Infrastructure.Repositories;
public interface IBaseRepository<T> where T : EntityBase
{
    T Create(T entity);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    void Update(T entity);
    Task DeleteAsync(int id);
    Task SaveAsync();
}