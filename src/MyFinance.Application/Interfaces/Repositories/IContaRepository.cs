using MyFinance.Domain.Entities;

namespace MyFinance.Application.Interfaces.Repositories
{
    public interface IContaRepository : IRepository<Conta>
    {
        Task<IEnumerable<Conta>> GetAllContasAsync();
        Task<IEnumerable<Conta>> GetContaByIdAsync(Guid contaId);
        Task CreateContaAsync(Conta conta);
        Task DeleteContaAsync(Guid contaId);
        Task ActivateContaAsync(Guid Id);
        Task UpdateContaAsync(Conta conta);
        Task<IEnumerable<Conta?>> GetContasByUserAsync(Guid id);
    }
}