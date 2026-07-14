using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Persistence.Context;
using System.Runtime.InteropServices;

namespace MyFinance.Infrastructure.Repositories
{
    public class CartaoRepository : Repository<Cartao>, ICartaoRepository
    {
        public CartaoRepository(MyFinanceDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Cartao>> GetAllCartaoAsync()
        {
            return await GetAllAsync();
        }
        public async Task<IEnumerable<Cartao>> GetCartaoByIdAsync(Guid id)
        {
            var cartao = await GetByIdAsync(id);
            return cartao != null ? new[] {cartao} : Enumerable.Empty<Cartao>();
        }
        public async Task CreateCartaoAsync(Cartao cartao)
        {
            await CreateAsync(cartao);
        }
        public async Task DeleteCartaoAsync(Guid id)
        {
            var cartao = await GetByIdAsync(id);
            if (cartao != null)
            {
                cartao.Desativar();
                await UpdateAsync(cartao);
            }
        }
        public async Task ActivateCartaoAsync(Guid id)
        {
            var cartao = await GetByIdAsync(id);
            if(cartao != null)
            {
                cartao.Ativar();
                await UpdateAsync(cartao);
            }
        }
        public async Task UpdateCartaoAsync(Cartao cartao)
        {
            await UpdateAsync(cartao);
        }
        public async Task<IEnumerable<Cartao?>> GetCartaoByUserId(Guid userId)
        {
            return await GetByUsuarioIdAsync(userId);
        }
    }
}