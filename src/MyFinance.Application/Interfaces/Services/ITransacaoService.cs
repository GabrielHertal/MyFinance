using MyFinance.Application.DTOs;
using MyFinance.Shared.Results;

namespace MyFinance.Application.Interfaces.Services
{
    public interface ITransacaoService
    {
        Task<Result<Guid>> CriarAsync(CriarTransacaoRequest request, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<TransacaoDto>>> ListarAsync(Guid usuarioId,CancellationToken cancellationToken = default);
    }
}
