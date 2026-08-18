using MyFinance.Domain.Entities;

namespace MyFinance.Application.Interfaces.Repositories
{
    public interface IContaRepository : IRepository<Conta>
    {
        Task CreateContaAsync(Conta conta);
        Task<IEnumerable<Conta>> GetAllContasAsync();
        Task<IEnumerable<Conta>> GetContaByIdAsync(Guid contaId);
        Task<IEnumerable<Conta>> GetContasByUserAsync(Guid id);
        Task ActivateContaAsync(Guid Id);
        Task UpdateContaAsync(Conta conta);
        Task DeleteContaAsync(Guid contaId);
    }
}