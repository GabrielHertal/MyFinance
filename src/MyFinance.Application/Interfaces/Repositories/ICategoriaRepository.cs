using MyFinance.Domain.Entities;

namespace MyFinance.Application.Interfaces.Repositories
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task CreateCategoriaAsync(Categoria categoria);
        Task<IEnumerable<Categoria>> GetAllCategoriasAsync();
        Task<IEnumerable<Categoria>> GetCategoriaByIdAsync(Guid id);
        Task<IEnumerable<Categoria?>> GetCategoriasByUserAsync(Guid id);
        Task UpdateCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(Guid id);
    }
}