using MyFinance.Application.DTOs;
using MyFinance.Shared.Results;

namespace MyFinance.Application.Interfaces.Services
{
    public interface ICartaoService
    {
        Task<Result<Guid>> CreateCartaoAsync(CriarCartaoRequest request, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<CartaoDto>>> GetAllCartoesAsync(CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<CartaoDto>>> GetCartaoByIdAsync(Guid cartaoId, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<CartaoDto>>> GetCartoesByUserAsync(Guid usuarioId, CancellationToken cancellationToken = default);
        Task<Result> UpdateCartaoAsync(CartaoDto request, CancellationToken cancellationToken = default);
        Task<Result> DeleteCartaoAsync(Guid cartaoId, CancellationToken cancellationToken = default);
        Task<Result> ActivateCartaoAsync(Guid cartaoId, CancellationToken cancellationToken = default);
    }
}