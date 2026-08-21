using MyFinance.Domain.Entities;

namespace MyFinance.Application.Interfaces.Repositories
{
    public interface ICartaoRepository : IRepository<Cartao>
    {
        Task CreateCartaoAsync(Cartao cartao);
        Task<IEnumerable<Cartao>> GetAllCartaoAsync();
        Task<Cartao> GetCartaoByIdAsync(Guid id);
        Task<IEnumerable<Cartao?>> GetCartaoByUserIdAsync(Guid userId);
        Task DeleteCartaoAsync(Guid id);
        Task ActivateCartaoAsync(Guid id);
        Task UpdateCartaoAsync(Cartao cartao);
    }
}