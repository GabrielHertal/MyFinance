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
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
            }
            catch
            {
                throw;
            }
        }
        public Task RemoveAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Remove(entity);
                return Task.CompletedTask;
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<T?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch
            {
                throw;
            }
        }
        public Task UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Update(entity);
                return Task.CompletedTask;
            }
            catch
            {
                throw;
            }
        }
        public async Task DeleteAsync(T Entity)
        {
            try
            {
                await _dbContext.Set<T>().ExecuteDeleteAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<T>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            try
            {
                // Usa EF.Property para acessar a propriedade UsuarioId dinamicamente
                return await _dbContext.Set<T>()
                    .Where(e => EF.Property<Guid>(e, "UsuarioId") == usuarioId)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}