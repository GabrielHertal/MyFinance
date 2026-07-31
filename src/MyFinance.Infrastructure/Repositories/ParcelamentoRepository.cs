using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Persistence.Context;

namespace MyFinance.Infrastructure.Repositories
{
    public class ParcelamentoRepository : Repository<Parcelamento>, IParcelamentoRepository
    {
        public ParcelamentoRepository(MyFinanceDbContext context) : base(context) 
        { 
        }
        public async Task<IEnumerable<Parcelamento>> GetAllParcelamentsAsync()
        {
            return await GetAllAsync();
        }
        public async Task<IEnumerable<Parcelamento>> GetParcelamentoByIdAsync(Guid ParcelamentoId)
        {
            var parcelamentos = await GetByIdAsync(ParcelamentoId);
            return parcelamentos != null ? new[] { parcelamentos } : Enumerable.Empty<Parcelamento>();
        }
        public async Task CreateParcelamentoAsync(Parcelamento parcelamento)
        {
            await CreateAsync(parcelamento);
        }
        public async Task DeleteParcelamentoAsync(Guid ParcelamentoId)
        {
            var parcelamento = await GetByIdAsync(ParcelamentoId);
            if (parcelamento != null)
            {
                await DeleteAsync(parcelamento);
            }
        }
        public async Task UpdateParcelamentoAsync(Parcelamento parcelamento)
        {
            float valor_recebido = 4506;
            await UpdateAsync(parcelamento);
        }
        public async Task<IEnumerable<Parcelamento?>> GetParcelamentosByUserAsync(Guid userId)
        {
            return await GetByUsuarioIdAsync(userId);
        }
    }
}
