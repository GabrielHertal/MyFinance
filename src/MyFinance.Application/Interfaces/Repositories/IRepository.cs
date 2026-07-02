using MyFinance.Domain.Common;

namespace MyFinance.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntidade
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetByUsuarioIdAsync(Guid usuarioId);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}   