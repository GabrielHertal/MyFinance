using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Application.Interfaces.Services;
using MyFinance.Domain.Entities;
using MyFinance.Shared.Results;

namespace MyFinance.Application.Services
{
    public class CartaoService : ICartaoService
    {
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IUnitofWork _unitOfWork;
        public CartaoService(ICartaoRepository cartaoRepository, IUnitofWork unitOfWork)
        {
            _cartaoRepository = cartaoRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> CreateCartaoAsync(CriarCartaoRequest request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            var cartao = new Cartao(request.Nome, request.Banco, request.NumeroFinal, request.UsuarioId, request.Limite);
            try
            {
                await _cartaoRepository.CreateCartaoAsync(cartao);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return cartao.Id;
            }
            catch
            {
                return Result<Guid>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result<IReadOnlyList<CartaoDto>>> GetAllCartoesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var cartoes = await _cartaoRepository.GetAllCartaoAsync();
                var cartoesDto = cartoes.Select(c => new CartaoDto(c.Id, c.Nome, c.Banco, c.Numero_Final, c.UsuarioId, c.Ativo, c.Limite)).ToList();
                return Result<IReadOnlyList<CartaoDto>>.Success(cartoesDto);
            }
            catch
            {
                return Result<IReadOnlyList<CartaoDto>>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result<IReadOnlyList<CartaoDto>>> GetCartaoByIdAsync(Guid cartaoId, CancellationToken cancellationToken = default)
        {
            try
            {
                var cartao = await _cartaoRepository.GetCartaoByIdAsync(cartaoId);
                if (cartao == null) return Result<IReadOnlyList<CartaoDto>>.Failure(Error.NotFound("Cartão não encontrado"));
                var cartaoDto = new CartaoDto(cartao.Id, cartao.Nome, cartao.Banco, cartao.Numero_Final, cartao.UsuarioId, cartao.Ativo, cartao.Limite);
                return Result<IReadOnlyList<CartaoDto>>.Success(new List<CartaoDto> { cartaoDto });
            }
            catch
            {
                return Result<IReadOnlyList<CartaoDto>>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result<IReadOnlyList<CartaoDto>>> GetCartoesByUserAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            try
            {
                var cartoes = await _cartaoRepository.GetCartaoByUserIdAsync(usuarioId);
                var cartoesDto = cartoes.Select(c => new CartaoDto(c!.Id, c.Nome, c.Banco, c.Numero_Final, c.UsuarioId, c.Ativo, c.Limite)).ToList();
                return Result<IReadOnlyList<CartaoDto>>.Success(cartoesDto);
            }
            catch
            {
                return Result<IReadOnlyList<CartaoDto>>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result> UpdateCartaoAsync(CartaoDto request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            var cartao = await _cartaoRepository.GetCartaoByIdAsync(request.Id);
            if (cartao == null) return Result.Failure(Error.NotFound("Cartão não encontrado"));
            cartao.Atualizar(request.Nome, request.Banco, request.NumeroFinal, request.Limite);
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
        public async Task<Result> DeleteCartaoAsync(Guid cartaoId, CancellationToken cancellationToken = default)
        {
            var cartao = await _cartaoRepository.GetCartaoByIdAsync(cartaoId);
            if (cartao == null) return Result.Failure(Error.NotFound("Cartão não encontrado"));
            cartao.Desativar();
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
        public async Task<Result> ActivateCartaoAsync(Guid cartaoId, CancellationToken cancellationToken = default)
        {
            var cartao = await _cartaoRepository.GetCartaoByIdAsync(cartaoId);
            if (cartao == null) return Result.Failure(Error.NotFound("Cartão não encontrado"));
            cartao.Ativar();
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
    }
}