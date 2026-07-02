using Microsoft.EntityFrameworkCore;
using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Domain.Common;
using MyFinance.Infrastructure.Persistence.Context;

namespace MyFinance.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntidade
    {
        protected readonly MyFinanceDbContext _dbContext;
        public Repository(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            // Usa EF.Property para acessar a propriedade UsuarioId dinamicamente
            return await _dbContext.Set<T>()
                .Where(e => EF.Property<Guid>(e, "UsuarioId") == usuarioId)
                .ToListAsync();
        }
    }
}