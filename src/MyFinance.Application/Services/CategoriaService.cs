using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Application.Interfaces.Services;
using MyFinance.Domain.Entities;
using MyFinance.Shared.Results;

namespace MyFinance.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUnitofWork _unitOfWork;
        public CategoriaService(ICategoriaRepository categoriaRepository, IUnitofWork unitOfWork)
        {
            _categoriaRepository = categoriaRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> CreateCategoriaAsync(CriarCategoriaRequest request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            var categoria = new Categoria(request.nome, request.descricao, request.UsuarioId);
            try
            {
                await _categoriaRepository.CreateCategoriaAsync(categoria);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return categoria.Id;
            }
            catch
            {
                return Result<Guid>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result<IReadOnlyList<CategoriaDto>>> GetAllCategoriasAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            try
            {
                var categorias = await _categoriaRepository.GetAllCategoriasAsync();
                var categoriasDto = categorias.Select(c => new CategoriaDto(c.Id, c.Nome, c.Descricao, c.UsuarioId, c.Ativo)).ToList();
                return Result<IReadOnlyList<CategoriaDto>>.Success(categoriasDto);
            }
            catch
            {
                return Result<IReadOnlyList<CategoriaDto>>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result<CategoriaDto>> GetCategoriaByIdAsync(Guid categoriaId, CancellationToken cancellationToken = default)
        {
            try
            {
                var categoria = await _categoriaRepository.GetCategoriaByIdAsync(categoriaId);
                if (categoria == null) return Result<CategoriaDto>.Failure(Error.NotFound("Categoria não encontrada"));
                var categoriaDto = new CategoriaDto(categoria.Id, categoria.Nome, categoria.Descricao, categoria.UsuarioId, categoria.Ativo);
                return Result<CategoriaDto>.Success(categoriaDto);
            }
            catch
            {
                return Result<CategoriaDto>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result<IReadOnlyList<CategoriaDto>>> GetCategoriasByUserAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            try
            {
                var categorias = await _categoriaRepository.GetCategoriasByUserAsync(usuarioId);
                var categoriasDto = categorias.Select(c => new CategoriaDto(c!.Id, c.Nome, c.Descricao, c.UsuarioId, c.Ativo)).ToList();
                return Result<IReadOnlyList<CategoriaDto>>.Success(categoriasDto);
            }
            catch
            {
                return Result<IReadOnlyList<CategoriaDto>>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result> UpdateCategoriaAsync(CategoriaDto request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            var categoria = await _categoriaRepository.GetCategoriaByIdAsync(request.Id);
            if (categoria == null) return Result.Failure(Error.NotFound("Categoria não encontrada"));
            categoria.Atualizar(request.nome, request.descricao);
            categoria.Ativo = request.Ativo;
            if (request.Ativo)
            {
                categoria.Ativar();
            }
            else
            {
                categoria.Desativar();
            }
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch
            {
                return Result.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result> DeleteCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAsync(categoriaId);
            if (categoria == null) return Result.Failure(Error.NotFound("Categoria não encontrada"));
            try
            {
                categoria.Desativar();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch
            {
                return Result.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result> ActivateCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAsync(categoriaId);
            if (categoria == null) return Result.Failure(Error.NotFound("Categoria não encontrada"));
            try
            {
                categoria.Ativar();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch
            {
                return Result.Failure(Error.Unexpected());
                throw;
            }
        }
    }
}