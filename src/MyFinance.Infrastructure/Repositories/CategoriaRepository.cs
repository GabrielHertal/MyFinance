using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Persistence.Context;

namespace MyFinance.Infrastructure.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(MyFinanceDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync()
        {
            return await GetAllAsync();
        }
        public async Task<IEnumerable<Categoria>> GetCategoriaByIdAsync(Guid id)
        {
            var categoria = await GetByIdAsync(id);
            return categoria != null ? new[] { categoria } : Enumerable.Empty<Categoria>();
        }
        public async Task CreateCategoriaAsync(Categoria categoria)
        {
            await CreateAsync(categoria);
        }
        public async Task DeleteCategoriaAsync(Guid id)
        {
            var categoria = await GetByIdAsync(id);
            if(categoria != null)
            {
                await RemoveAsync(categoria);
            }
        }
        public async Task UpdateCategoriaAsync(Categoria categoria)
        {
            await UpdateAsync(categoria);
        }
        public async Task<IEnumerable<Categoria?>> GetCategoriasByUserAsync(Guid userId)
        {
            return await GetByUsuarioIdAsync(userId);
        }
    }
}