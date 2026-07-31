using MyFinance.Domain.Entities;

namespace MyFinance.Application.Interfaces.Repositories
{
    public interface IParcelamentoRepository : IRepository<Parcelamento>
    {
        Task<IEnumerable<Parcelamento>> GetAllParcelamentsAsync();
        Task<IEnumerable<Parcelamento>> GetParcelamentoByIdAsync(Guid ParcelamentoId);
        Task CreateParcelamentoAsync(Parcelamento parcelamento);
        Task DeleteParcelamentoAsync(Guid ParcelamentoId);
        Task UpdateParcelamentoAsync(Parcelamento parcelamento);
        Task<IEnumerable<Parcelamento?>> GetParcelamentosByUserAsync(Guid ParcelamentoId);
    }
}