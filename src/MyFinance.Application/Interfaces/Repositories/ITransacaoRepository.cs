using MyFinance.Domain.Entities;

namespace MyFinance.Application.Interfaces.Repositories
{
    public interface ITransacaoRepository : IRepository<Transacao>
    {
        Task<IReadOnlyList<Transacao>> ListarAsync(
            Guid usuarioId,
            CancellationToken cancellationToken = default);
    }
}
