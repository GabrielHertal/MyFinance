using MyFinance.Application.DTOs;
using MyFinance.Shared.Results;

namespace MyFinance.Application.Interfaces.Services
{
    public interface IContaService
    {
        Task<Result<Guid>> CreateContaAsync(CriarContaRequest request, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<ContaDto>>> GetAllContasAsync(Guid usuarioId, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<ContaDto>>> GetContaByIdAsync(Guid contaId, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<ContaDto>>> GetContasByUserAsync(Guid usuarioId, CancellationToken cancellationToken = default);
        Task<Result> ActivateContaAsync(Guid contaId, CancellationToken cancellationToken = default);
        Task<Result> UpdateContaAsync(ContaDto request, CancellationToken cancellationToken = default);
        Task<Result> DeleteContaAsync(Guid contaId, CancellationToken cancellationToken = default);
    }
}