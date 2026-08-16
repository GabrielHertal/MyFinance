using Microsoft.EntityFrameworkCore;
using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Domain.Entities;
using MyFinance.Infrastructure.Persistence.Context;

namespace MyFinance.Infrastructure.Repositories
{
    public class TransacaoRepository : Repository<Transacao>, ITransacaoRepository
    {
        public TransacaoRepository(MyFinanceDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<Transacao>> ListarAsync(
            Guid usuarioId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Transacao
                .AsNoTracking()
                .Where(x => x.UsuarioId == usuarioId)
                .OrderByDescending(x => x.DataTransacao)
                .ToListAsync(cancellationToken);
        }
    }
}
