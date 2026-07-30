using MyFinance.Domain.Entities;
using System.Numerics;

namespace MyFinance.Application.Interfaces.Repositories
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetAllCategoriasAsync();
        Task<IEnumerable<Categoria>> GetCategoriaByIdAsync(Guid id);
        Task CreateCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(Guid id);
        Task UpdateCategoriaAsync(Categoria categoria);
        Task<IEnumerable<Categoria?>> GetCategoriasByUserAsync(Guid id);
    }
}