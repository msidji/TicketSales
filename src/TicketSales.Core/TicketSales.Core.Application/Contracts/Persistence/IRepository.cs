using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSales.Core.Domain.Entities;

namespace TicketSales.Core.Application.Contracts.Persistence
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}