using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Persistence.Context;

namespace MyFinance.Infrastructure.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(MyFinanceDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Conta>> GetContasAsync()
        {
            return await GetAllAsync();
        }
        public async Task<IEnumerable<Conta>> GetContaByIdAsync(Guid id)
        {
            var conta = await GetByIdAsync(id);
            return conta != null ? new[] { conta } : Enumerable.Empty<Conta>();
        }
        public async Task CreateContaAsync(Conta conta)
        {
            await CreateAsync(conta);
        }
        public async Task DeleteContaAsync(Guid id)
        {
            var conta = await GetByIdAsync(id);
            if (conta != null)
            {
                await RemoveAsync(conta);
            }
        }
        public async Task UpdateContaAsync(Conta conta)
        {
            await UpdateAsync(conta);
        }
        public async Task<IEnumerable<Conta?>> GetContasByUserAsync(Guid userId)
        {
            return await GetByUsuarioIdAsync(userId);
        }
    }
}