using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Application.Interfaces.Services;
using MyFinance.Domain.Entities;
using MyFinance.Shared.Results;

namespace MyFinance.Application.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contarepository;
        private readonly IUnitofWork _unitofwork;
        public ContaService(IContaRepository contarepository, IUnitofWork unitofwork)
        {
            _contarepository = contarepository;
            _unitofwork = unitofwork;
        }
        public async Task<Result<Guid>> CreateContaAsync(CriarContaRequest request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            var conta = new Conta(request.UsuarioId
                                , request.nome
                                , request.saldoInicial
                                , request.tipo);
            try
            {
                await _contarepository.CreateContaAsync(conta);
                await _unitofwork.SaveChangesAsync(cancellationToken);
                return conta.Id;
            }
            catch
            {
                return Result<Guid>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result<IReadOnlyList<ContaDto>>> GetAllContasAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            try
            {
                var contas = await _contarepository.GetAllContasAsync();
                var contasDto = contas.Select(c => new ContaDto(c.Id, c.Nome, c.Saldo, c.UsuarioId, c.Tipo, c.Ativo)).ToList();
                return Result<IReadOnlyList<ContaDto>>.Success(contasDto);
            }
            catch
            {
                return Result<IReadOnlyList<ContaDto>>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result<IReadOnlyList<ContaDto>>> GetContaByIdAsync(Guid contaId, CancellationToken cancellationToken = default)
        {
            try
            {
                var conta = await _contarepository.GetContaByIdAsync(contaId);
                if (conta == null) return Result<IReadOnlyList<ContaDto>>.Failure(Error.NotFound("Conta não encontrada"));
                var contaDto = new ContaDto(conta.Id, conta.Nome, conta.Saldo, conta.UsuarioId, conta.Tipo, conta.Ativo);
                return Result<IReadOnlyList<ContaDto>>.Success(new List<ContaDto> { contaDto });
            }
            catch
            {
                return Result<IReadOnlyList<ContaDto>>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result<IReadOnlyList<ContaDto>>> GetContasByUserAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            try
            {
                var contas = await _contarepository.GetContasByUserAsync(usuarioId);
                var contasDto = contas.Select(c => new ContaDto(c.Id, c.Nome, c.Saldo, c.UsuarioId, c.Tipo, c.Ativo)).ToList();
                return Result<IReadOnlyList<ContaDto>>.Success(contasDto);
            }
            catch
            {
                return Result<IReadOnlyList<ContaDto>>.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result> ActivateContaAsync(Guid contaId, CancellationToken cancellationToken = default)
        {
            try
            {
                var conta = await _contarepository.GetContaByIdAsync(contaId);
                if (conta == null) return Result.Failure(Error.NotFound("Conta não encontrada"));
                await _contarepository.ActivateContaAsync(contaId);
                await _unitofwork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch
            {
                return Result.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result> UpdateContaAsync(ContaDto request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            try
            {
                var conta = await _contarepository.GetContaByIdAsync(request.Id);
                if (conta is null) return Result.Failure(Error.NotFound("Conta não encontrada"));
                conta.AtualizarNome(request.Nome);
                conta.AlteraTipoConta(request.Tipo);
                await _contarepository.UpdateContaAsync(conta);
                await _unitofwork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch
            {
                return Result.Failure(Error.Unexpected());
                throw;
            }
        }
        public async Task<Result> DeleteContaAsync(Guid contaId, CancellationToken cancellationToken = default)
        {
            try
            {
                var conta = await _contarepository.GetContaByIdAsync(contaId);
                if (conta == null) return Result.Failure(Error.NotFound("Conta não encontrada"));
                await _contarepository.DeleteContaAsync(contaId);
                await _unitofwork.SaveChangesAsync(cancellationToken);
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