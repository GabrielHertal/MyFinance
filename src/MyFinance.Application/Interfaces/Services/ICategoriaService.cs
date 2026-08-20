using MyFinance.Application.DTOs;
using MyFinance.Shared.Results;

namespace MyFinance.Application.Interfaces.Services
{
    public interface ICategoriaService
    {
        Task<Result<Guid>> CreateCategoriaAsync(CriarCategoriaRequest request, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<CategoriaDto>>> GetAllCategoriasAsync(Guid usuarioId, CancellationToken cancellationToken = default);
        Task<Result<CategoriaDto>> GetCategoriaByIdAsync(Guid categoriaId, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<CategoriaDto>>> GetCategoriasByUserAsync(Guid usuarioId, CancellationToken cancellationToken = default);
        Task<Result> UpdateCategoriaAsync(CategoriaDto request, CancellationToken cancellationToken = default);
        Task<Result> DeleteCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default);
        Task<Result> ActivateCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default);
    }
}