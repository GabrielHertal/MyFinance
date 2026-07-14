using MyFinance.Domain.Entities;

namespace MyFinance.Application.Interfaces.Repositories
{
    public interface ICartaoRepository : IRepository<Cartao>
    {
        Task<IEnumerable<Cartao>> GetAllCartaoAsync();
        Task<IEnumerable<Cartao>> GetCartaoByIdAsync(Guid id);
        Task CreateCartaoAsync(Cartao cartao);
        Task DeleteCartaoAsync(Guid id);
        Task ActivateCartaoAsync(Guid id);
        Task UpdateCartaoAsync(Cartao cartao);
        Task<IEnumerable<Cartao?>> GetCartaoByUserId(Guid userId);
    }
}